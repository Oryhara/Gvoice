using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace GoogleVoice.NET.Entities {
	public class TRTest {
		public class TRTestStep {
			public string content { get; set; }
			public string expected { get; set; }
		}
		public int testID { get; set; }
		public int assignedto_ID { get; set; }
		public int case_id { get; set; }
		public string custom_Expected { get; set; }
		public string custom_Preconds { get; set; }
		public TRTestStep[] custom_casesteps { get; set; }
		public string estimate { get; set; }
		public string estimate_forecast { get; set; }
		public int priority_id { get; set; }
		public int run_id { get; set; }
		public int status_id { get; set; }
		public string title { get; set; }
		public int type_id { get; set; }
	}
}
