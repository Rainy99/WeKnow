using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WeKnow.Models {
	public class UserInfoModel {
		public string UserName { get; set; }
		public string ReallyUserName { get; set; }
		public string city { get; set; }
		public string password { get; set; }
		public string confirmPassword { get; set; }
		public int gender { get; set; }
		public DateTime birthday { get; set; }
		public string KeiKnow { get; set; }
		public string email { get; set; }
		public int QQname { get; set; }
	}
}