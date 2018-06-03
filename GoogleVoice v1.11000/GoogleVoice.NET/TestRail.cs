using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoogleVoice.NET {
	class TestRail {
		private System.Net.WebClient client = new System.Net.WebClient();
		public enum TestResult { Passed, Blocked, Retest, Failed };
		public bool PostResults(string testName, string result) {
			
			return false;
		}

	}
}
