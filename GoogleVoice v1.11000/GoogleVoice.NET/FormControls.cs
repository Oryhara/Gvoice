using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using Google.Voice;
using Google.Voice.Extensions;
using Google.Voice.Entities;
using System.Text.RegularExpressions;
using HtmlAgilityPack;
using System.Timers;
using System.Threading;

namespace GoogleVoice.NET
{
    public partial class FormControls : Form {
        private bool doResetEvents = true;
        private AutoCompleteStringCollection contactsAutoComplete = new AutoCompleteStringCollection();
        private bool forceClose = false;
        private GoogleVoiceEventArgs CurrentEvent;
        bool creating = false;

        private Dictionary<string, string> expectedSMSResponses = new Dictionary<string, string>()
        {
            { ".help", "Commands:\n.help\n.info\n.packagelist\n.status" },
            { ".info","Model: VT400\nSN: 010030016664340\n.info [help|cell|gps|fw|vcore|ign|obd|battery|wifi]" }

        };
        private Dictionary<string, string> actualSMSResponses = new Dictionary<string, string>();

        private string CurrendEventID
        {
            get
            {
                return CurrentEvent.ID;
            }
            set
            {
                CurrentEvent.ID = value;
            }
        }

        public FormControls() {
            InitializeComponent();
        }

        private void FormControls_FormClosing(object sender, FormClosingEventArgs e) {
            if (!forceClose) {
                e.Cancel = true;
                WindowState = FormWindowState.Minimized;
            } else {
                Program.LoginForm.Show();
            }
        }

        private void lblSignOut_Click(object sender, EventArgs e) {
            Program.HaltEvents = true;
            Program.GoogleVoice.Logout();
            forceClose = true;
            Close();
        }

        private void lblHide_Click(object sender, EventArgs e) {
            WindowState = FormWindowState.Minimized;
        }

        private void lblLock_Click(object sender, EventArgs e) {
            Hide();
            Program.LoginForm.Show();
        }

        //private void btnCall_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        if (chkRemember.Checked)
        //        {
        //            Settings.Default.DefaultPhoneName = (listPhones.SelectedItem as Variable).Name;
        //            Settings.Default.Save();
        //        }

        //        string to = "0";

        //        if (btnContactsCall.Checked)
        //        {
        //            //to = (listContactsCall.SelectedItem as Contact).phoneNumber;
        //            Match match = Regex.Match(listContactsCall.SelectedItem.ToString(), @"\d{11}");
        //            if (match.Success)
        //            {
        //                to = match.Value;
        //            }
        //        }
        //        else
        //        {
        //            to = LoadAutoCompleteString(txtCall.Text).phoneNumber;
        //        }
        //        string from = "0";
        //        Match match2 = Regex.Match((listPhones.SelectedItem as ForwardingPhone).phoneNumber, @"\d{11}");
        //        if (match2.Success)
        //        {
        //            from = match2.Value;
        //        }
        //        Program.GoogleVoice.Call(from, to);
        //        btnCancelCall.Enabled = true;
        //        timerCancelCall.Enabled = true;
        //        btnCall.Enabled = false;
        //        try
        //        {
        //            var lstItem = new ListViewItem();
        //            string call = "Call to " + to + " at " + DateTime.Now;
        //            lstItem.Text = call;
        //            StreamWriter writer = File.AppendText("History.txt");
        //            writer.WriteLine(call);
        //            writer.Close();
        //            lstHistory.Items.Add(lstItem);
        //        }
        //        catch (Exception)
        //        {
        //            throw;
        //        }
        //    }
        //    catch
        //    {
        //        MessageBox.Show(
        //            "You supplied an invalid number or contact to call. " + 
        //            Environment.NewLine + 
        //            "Only enter numbers; Example: 8008675309");
        //    }
        //}

        private Contact LoadAutoCompleteString(string p) {
            if (IsNumeric(p)) {
                return new Contact() {
                    name = "Unknown",
                    phoneNumber = Convert.ToInt64(p).ToString()
                };
            }

            foreach (var pair in Program.GoogleVoice.Account.contacts) {
                foreach (PhoneNumber c in pair.Value.numbers) {
                    if (p.ToUpper() == c.ToString().ToUpper()) {
                        return pair.Value;
                    }
                }
            }

            string number = "";

            foreach (char c in p.ToCharArray()) {
                if (IsNumeric(c.ToString()))
                    number += c.ToString();
            }

            if (number.Length >= 10) {
                return new Contact() {
                    name = "Unknown",
                    phoneNumber = Convert.ToInt64(number).ToString()
                };
            }

            return new Contact() {
                name = "Unknown",
                phoneNumber = Convert.ToInt64(0).ToString()
            };
        }

        private bool IsNumeric(string s) {
            try {
                decimal n = Convert.ToDecimal(s);
            } catch {
                return false;
            }
            return true;
        }


        private void FormControls_Load(object sender, EventArgs e) {
            int i = 0;
            foreach (var pair in Program.GoogleVoice.Account.phones) {
                ForwardingPhone variable = pair.Value;
                if (variable.name == Settings.Default.DefaultPhoneName) {
                }
                i++;
            }

            foreach (var pair in Program.GoogleVoice.Account.contacts) {
                var contact = pair.Value;
                try {
                    //foreach (PhoneNumber contactPhone in contact.numbers)
                    //{
                    string number = contact.name + " " + contact.phoneNumber;
                    contactsAutoComplete.Add(number);

                    //listContactsText.Items.Add(number);
                    contactsSelectionDropdown.Items.Add(number);
                    //}
                } catch (Exception) {
                }
            }

            //txtCall.AutoCompleteCustomSource = contactsAutoComplete;yea
           // txtTextNumber.AutoCompleteCustomSource = contactsAutoComplete;
            StreamReader reader = new StreamReader("History.txt");
            string text = "";
            while (!reader.EndOfStream) {
                text = reader.ReadLine();
                var lstItem = new ListViewItem();
                lstItem.Text = text;

            };
            reader.Close();
            //GetFolderResult inbox = Program.GoogleVoice.History(0);
            //string s = Program.GoogleVoice.Dump();
            //foreach( Google.Voice.Entities.Message m in inbox.messages.Values) {
            //    string smsMessage = m.displayStartDateTime +" "+ m.displayNumber +" "+ m.messageText;
            //    lstHistory.Items.Add(smsMessage);
            //}

            ResetEvents();
            GoogleVoiceEventWatcher.Run(Program.GoogleVoice);
        }

        private void timerCancelCall_Tick(object sender, EventArgs e) {
            timerCancelCall.Enabled = false;
        }

        public long ToLong(string number) {
            try {
                return Convert.ToInt64(number);
            } catch {
                return 0;
            }
        }



        private void ResetEvents() {
            if (doResetEvents == true) {
                doResetEvents = false;



                int i = 0;

                doResetEvents = true;
            }
        }



        private void button1_Click(object sender, EventArgs e) {
            try {
                object to = 0;
                string smsMessage = testSMSMessage.Text;
                if (testSMSMessage.SelectedItem == null) {
                    //do nothing
                } else {
                    smsMessage = testSMSMessage.Text;
                }
                smsMessage += textBox1.Text;
                if (contactsLookupButton.Checked) {
                    //to = (listContactsText.SelectedItem as Contact).phoneNumber;
                    Match match = Regex.Match(contactsSelectionDropdown.SelectedItem.ToString(), @"\d{11}");
                    if (match.Success) {
                        to = match.Value;
                    }
                } else {
                    to = LoadAutoCompleteString(recipientNumberBox.Text).phoneNumber;
                }

                Program.GoogleVoice.SMS(to.ToString(), smsMessage);
                try {
                    var lstItem = new ListViewItem();
                    string text = "SMS: " + to.ToString() + " - \"" + smsMessage + "\" at " + DateTime.Now;
                    lstItem.Text = text;
                    sentListView.Items.Add(lstItem);
                    StreamWriter writer = File.AppendText("History.txt");
                    writer.WriteLine(text);
                    writer.Close();
                } catch (Exception) {
                    throw;
                }

                //MessageBox.Show("The message has been sent!");
            } catch {
                MessageBox.Show(
                    "You supplied an invalid number or contact to send a SMS. " +
                    Environment.NewLine +
                    "Only enter numbers; Example: 8008675309");
            }
        }

        private void label3_Click(object sender, EventArgs e) {

        }

        private void label4_Click(object sender, EventArgs e) {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e) {

        }

        private void tabPage5_Click(object sender, EventArgs e) {

        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e) { }
        private void radioButton1_Click(object sender, EventArgs e) {
            contactsLookupButton.Checked = !contactsLookupButton.Checked;
            contactsSelectionDropdown.Visible = contactsLookupButton.Checked;
            if (contactsLookupButton.Checked)
                contactsSelectionDropdown.Focus();
        }


        private void testSMSMessage_SelectedIndexChanged(object sender, EventArgs e) {

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e) {

        }

        private void tabPage3_Click(object sender, EventArgs e) {

        }


        private void button1_Click_2(object sender, EventArgs e) {
            sentListView.Clear();
            recievedSMSTextBox.Clear();
            refreshMessages();
        }
        private void refreshMessages() {
            backgroundWorker1.RunWorkerAsync();
            HtmlNodeCollection messages = Program.GoogleVoice.InboxMessages(Properties.Settings.Default.rnr_se, 0, "https://www.google.com/voice/b/0/inbox/recent/");
            if (messages != null) {
                foreach (HtmlNode message in messages) {
                    string messageText = "";
                    string messageAuthor = "";
                    messageAuthor = message.ChildNodes[1].InnerText;
                    messageText += message.ChildNodes[3].InnerText;
                    if (messageAuthor.Contains("Me:")) {
                        sentListView.Items.Add(messageText);
                    } else {
                        if (messageText.Contains("((more)")) {
                            sentListView.Items.Add("");
                        }
                        recievedSMSTextBox.AppendText(messageText + "\n");
                    }
                }
            }
        }



        private void label2_Click(object sender, EventArgs e) {

        }

        private void label2_Click_1(object sender, EventArgs e) {

        }

        private void textBox2_TextChanged(object sender, EventArgs e) {

        }
        public void getLatestMessage() {
            HtmlNodeCollection messages = Program.GoogleVoice.GetLatestMessage(Properties.Settings.Default.rnr_se, 0, "https://www.google.com/voice/b/0/inbox/");
            if (messages != null) {
                string sent = "";
                string recieved = "";
                foreach (HtmlNode message in messages) {
                    //should be a pair of messages
                    string messageText = "";
                    string messageAuthor = "";
                    messageAuthor = message.ChildNodes[1].InnerText;
                    messageText += message.ChildNodes[3].InnerText;
                    if (messageAuthor.Contains("Me:")) {
                        sent = messageText;
                    } else {
                        if (messageText.Contains("((more)")) {
                            recieved += messageText;
                        }
                        recieved = messageText;
                    }
                }
                if (actualSMSResponses.Keys.Contains(sent)) {

                } else {
                    actualSMSResponses.Add(sent, recieved);
                }
            }
        }
        private void run_Test_Messages(object sender, EventArgs e) {
            
            //first get the contact we're sending to
            object to = 0;
            if (contactsLookupButton.Checked) {
                //to = (listContactsText.SelectedItem as Contact).phoneNumber;
                Match match = Regex.Match(contactsSelectionDropdown.SelectedItem.ToString(), @"\d{11}");
                if (match.Success) {
                    to = match.Value;
                }
            } else {
                to = LoadAutoCompleteString(recipientNumberBox.Text).phoneNumber;
            }
            //send message for every test
            foreach (string testSMS in expectedSMSResponses.Keys) {
                Program.GoogleVoice.SMS(to.ToString(), testSMS);
                string response = expectedSMSResponses[testSMS];
                for(int i = 0; i<5 ; i++) {
                    //wait 60 seconds
                    Thread.Sleep(30000);
                    //check for messages
                    getLatestMessage();
                }
                if (response == "") {
                    //empty string response means a negative test.   should be no response. 

                }else {
                    if (actualSMSResponses[testSMS] == expectedSMSResponses[testSMS]) {
                        //pass.
                        MessageBox.Show(testSMS + " test passed");
                    }else {
                        //fail.
                        MessageBox.Show(testSMS + " test failed");
                    }

                }
            }
            //evaluate responses
            //mark read
        }


        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e) {
            var backgroundWorker = sender as BackgroundWorker;
            for (int i = 1; i <= 100; i++) {
                // Wait 100 milliseconds.
                Thread.Sleep(100);
                // Report progress.
                backgroundWorker.ReportProgress(i);
            }
        }

        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e) {
            // Change the value of the ProgressBar to the BackgroundWorker progress.
            progressBar1.Value = e.ProgressPercentage;
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e) {

        }

        private void tabPage3_Click_1(object sender, EventArgs e) {

        }
    }
}
