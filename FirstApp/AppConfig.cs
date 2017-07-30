using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace FirstApp {
    public class AppConfig {
        public static IConfigurationRoot Config = new ConfigurationBuilder()
        .SetBasePath(Directory.GetCurrentDirectory())
        .AddJsonFile("appsettings.json")
		.AddJsonFile("appsettings.secret.json")
		.Build();

		public static string DatabaseConnection {
			get {
				return Config.GetConnectionString("DefaultConnection");
			}
		}

		public static string RecaptchaSiteKey {
			get {
				return Config.GetSection("Recaptcha")["SiteKey"];
			}
		}

		public static string RecaptchaSecretKey {
			get {
				return Config.GetSection("Recaptcha")["SecretKey"];
			}
		}
    }
}
