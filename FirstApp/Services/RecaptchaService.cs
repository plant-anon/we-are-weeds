using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace FirstApp.Services {
	public class RecaptchaService {
		private class RecaptchaResponse {
			[DataMember(Name = "success")]
			public bool Success { get; set; }

			[DataMember(Name = "error-codes")]
			public List<string> ErrorCodes { get; set; } //TODO: Alert to user
		}

		public static bool Validate(string CaptchaResponse, string ClientIp) {
			try {
				var url = string.Format("https://www.google.com/recaptcha/api/siteverify?secret={0}&response={1}&remoteip={2}", AppConfig.RecaptchaSecretKey, CaptchaResponse, ClientIp);

				WebRequest request = WebRequest.Create(url);
				request.ContentType = "application/json; charset=utf-8";

				var task = request.GetResponseAsync();
				task.Wait();

				WebResponse response = task.Result;

				using(Stream responseStream = response.GetResponseStream()) {
					StreamReader reader = new StreamReader(responseStream, Encoding.UTF8);
					RecaptchaResponse obj = JsonConvert.DeserializeObject<RecaptchaResponse>(reader.ReadToEnd());
					return obj.Success;
				}
			} catch(Exception ex) {
				//TODO: Log
				return false;
			}
		}
	}
}
