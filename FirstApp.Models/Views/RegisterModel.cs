using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace FirstApp.Models.Views {
	public class RegisterModel {
		public string Username { get; set; }
		public string Password { get; set; }
		public string RePassword { get; set; }
	}
}
