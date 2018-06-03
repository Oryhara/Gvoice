using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Windows.Forms;
using Google.Voice;
using System.Reflection;
using Google.Voice;
using System.Configuration;
using CommandLine;
using HtmlAgilityPack;
using System.Threading;
using System.Xml;
using System.IO;
using Gurock.TestRail;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using GoogleVoice.NET.Entities;

namespace GoogleVoice.NET
{
	class Program {
		
		public static Google.Voice.GoogleVoice GoogleVoice { get; set; }
		private static StringCollection serializedEvents = Settings.Default.GoogleVoiceEvents;
		private static GoogleVoiceEvents voiceEvents;
		public static GoogleVoiceEvents VoiceEvents
		{
			get
			{
				if (voiceEvents == null) {
					if (serializedEvents == null) {
						serializedEvents = new StringCollection();
					}

					voiceEvents = new GoogleVoiceEvents(serializedEvents);
				}

				return voiceEvents;
			}
			set
			{
				voiceEvents = value;
				Settings.Default.GoogleVoiceEvents = value.Serialize();
				Settings.Default.Save();
			}
		}
		public static FormLogin LoginForm { get; set; }
		public static FormControls ControlsForm { get; set; }
		private static volatile bool haltEvents;
		public static bool HaltEvents
		{
			get { return Program.haltEvents; }
			set { Program.haltEvents = value; }
		}

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main(string[] args) {
			GoogleVoice = new Google.Voice.GoogleVoice();

			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);


			if (args.Length == 0) {
				LoginForm = new FormLogin();
				Application.Run(LoginForm);
			} else {
				CLIRUnner cli = new CLIRUnner();
				var result = Parser.Default.ParseArguments<Options,testRailOptions>(args)
					.WithParsed<testRailOptions>(options => cli.runTestRail(options))
					.WithParsed<Options>(options => cli.runCLIMode(options))
					.WithNotParsed(errors => System.Console.WriteLine(errors.ToString()));

			}
		}

		public static void SaveVoiceEvents() {
			Settings.Default.Save();
		}
		[Verb("testRail", HelpText = "verb to run this program from TestRail", Hidden = true)]
		class testRailOptions {
			[Option('r', HelpText = "Test Run Number for TestRail", SetName = "testRun", Required = true)]
			public string testRun { get; set; }

			[Option('d', HelpText = "Destination Number to send SMS to", Default = "", Required = false)]
			public string recipient { get; set; }

		}
		class Options {

			[Option('d', HelpText = "Destination Number to send SMS to", Default = "", Required = false)]
			public string recipient { get; set; }

			[Option('m', HelpText = "SMS Message to send to destination", Required = true, Separator = ':')]
			public IEnumerable<string> smsmessage { get; set; }

			[Option('u', HelpText = "email to log in to Google Voice", SetName = "email", Default = "", Required = false)]
			public string username { get; set; }

			[Option('p', HelpText = "password to log in to Google Voice", SetName = "pw", Default = "", Required = false)]
			public string password { get; set; }

			[Option('c', HelpText = "Config file name", SetName = "config", Default = "", Required = false)]
			public string config { get; set; }

			// Omitting long name, default --verbose
			[Option(
			  HelpText = "Prints all messages to standard output.")]
			public bool Verbose { get; set; }

		}

		private class CLIRUnner {
			enum Results { INVALID, Passed, Blocked, Untested, Retest, Failed };
			APIClient TRClient;

			private bool loadCustomConfiguration(string filename) {
				try {
					XmlDataDocument xmlDoc = new XmlDataDocument();
					XmlNodeList xmlNodes;

					FileStream fs = new FileStream(filename, FileMode.Open, FileAccess.Read);
					xmlDoc.Load(fs);
					xmlNodes = xmlDoc.GetElementsByTagName("appSettings")[0].ChildNodes;
					foreach (XmlNode node in xmlNodes) {
						Properties.Settings.Default[node.Name] = node.InnerText;
					}
					Properties.Settings.Default.Save();
				} catch (Exception e) {
					return false;
				}
				return true;
			}
			public int runTestRail(testRailOptions arguments) {
				TRClient = new APIClient(Properties.Settings.Default.TRURL);
				TRClient.User = Properties.Settings.Default.TRusername;
				TRClient.Password = Properties.Settings.Default.TRAPIkey;

				bool loginSuccessful = CLILogin(Properties.Settings.Default.email, Properties.Settings.Default.password);
				if (!loginSuccessful) {
					System.Console.WriteLine("Login unsuccessful.  Exiting.");
					return 0;
				}

				string destination;
				if (string.IsNullOrEmpty(arguments.recipient) && string.IsNullOrEmpty(Properties.Settings.Default.destination)) {
					//no one to send SMS to
					System.Console.WriteLine("Recipient not specified.  Add to config or include in command line.");
					return 0;
				} else if (string.IsNullOrEmpty(arguments.recipient)) {
					destination = Properties.Settings.Default.destination;
				} else {
					destination = arguments.recipient;
				}
				string smsResponse = "";
				List<TRTest> test_run = getTests(arguments.testRun);
				foreach (TRTest test in test_run) {
					string sms = getSMSFromTest(test);
					smsResponse = CLISendSMS(sms, destination);
					if (string.IsNullOrEmpty(smsResponse)) {
						smsResponse = "no response for: ";
						smsResponse += sms.ToString();
					}

					if (EvaluateResponse(getExpectedResponseFromTest(test),smsResponse)) {
						uploadTestRailResult(test.case_id, (int)Results.Passed, smsResponse);
					}else {
						uploadTestRailResult(test.case_id, (int)Results.Failed, smsResponse);
					}
					smsResponse = "";
				} 
				return 1;
			}
			public string getSMSFromTest(TRTest test) {
				return "";
			}
			public string getExpectedResponseFromTest(TRTest test) {
				return "";
			}
			public int runCLIMode(Options arguments) {
				

				//load config options
				if (!string.IsNullOrEmpty(arguments.config)) {
					if (loadCustomConfiguration(arguments.config)) {

					}
				}//no custom config defined.  using defaults
				 //login
				bool loginSuccessful;
				if (string.IsNullOrEmpty(arguments.username) | string.IsNullOrEmpty(arguments.password)) {
					//username/password not specified on command line, use configured value.
					loginSuccessful = CLILogin(Properties.Settings.Default.email, Properties.Settings.Default.password);
				} else {
					loginSuccessful = CLILogin(arguments.username, arguments.password);
				}
				if (!loginSuccessful) {
					System.Console.WriteLine("Login unsuccessful.  Exiting.");
					return 0;
				}
				string smsResponse;
				//send sms
				string destination;
				if (string.IsNullOrEmpty(arguments.recipient) && string.IsNullOrEmpty(Properties.Settings.Default.destination)) {
					//no one to send SMS to
					System.Console.WriteLine("Recipient not specified.  Add to config or include in command line.");
					return 0;
				} else if (string.IsNullOrEmpty(arguments.recipient)) {
					destination = Properties.Settings.Default.destination;
				} else {
					destination = arguments.recipient;
				}
				foreach (string sms in arguments.smsmessage) {
					smsResponse = CLISendSMS(sms, destination);
					if (string.IsNullOrEmpty(smsResponse)) {
						smsResponse = "no response for: ";
						smsResponse += sms.ToString();
					}

					smsResponse = "";
				}
				return 1;
				//print output
			}


			private bool CLILogin(string username, string password) {
				var loginResult = Program.GoogleVoice.Login(username, password);
				if (!loginResult.RequiresRelogin) {
					return true;
				}

				if (loginResult.Exception == null) {
					System.Console.WriteLine("The username or password you entered is incorrect.");
				} else {
					System.Console.WriteLine(loginResult.Exception);
				}
				return false;
			}

			private string CLISendSMS(string message, string destination) {
				Program.GoogleVoice.SMS(destination.ToString(), message);
				string response = "";
				for (int i = 0; i < 18 && string.IsNullOrEmpty(response); i++) {
					//wait 10 seconds
					Thread.Sleep(10000);
					//check for messages
					response = checkForMessage();
				}

				return response.Replace('\n', ' ');
			}

			public string checkForMessage() {
				HtmlNodeCollection messages = Program.GoogleVoice.GetLatestMessage(Properties.Settings.Default.rnr_se, 0, "https://www.google.com/voice/b/0/inbox/");
				string conversation = "";
				if (messages != null) {

					foreach (HtmlNode message in messages) {
						//should be a pair of messages
						string messageText = "";
						string messageAuthor = "";
						messageAuthor += message.ChildNodes[1].InnerText;
						messageText += message.ChildNodes[3].InnerText;
						if (messageAuthor.Contains("Me:")) {
							conversation = messageAuthor + messageText;
						} else {
							if (messageText.Contains("((more)")) {
								conversation += messageText;
							}
							conversation += messageAuthor + messageText;
						}
					}
				}
				return conversation;
			}

			public bool EvaluateResponse(string expectedResponse, string actualResponse) {
				return false;
			}

			public List<TRTest> getTests(string runID) {
				string uri = "get_tests/" + runID;
				JArray run_tests = (JArray)TRClient.SendGet(uri);
				List<TRTest> result = new List<TRTest>();
				foreach (JObject test in run_tests) {
					result.Add(test.ToObject<TRTest>());
				}
				return result;
			}

			public void uploadTestRailResult(int caseID, int result, string comment) {
				var requestData = new Newtonsoft.Json.Linq.JObject();
				string uri = "add_result/" + caseID.ToString();
				requestData.Add("status_id", result);
				requestData.Add("comment", comment);
				TRClient.SendPost(uri, requestData);
			}
		}
	}
}
