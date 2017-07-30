using Rijndael256;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace FirstApp.Services {
	public class EncryptionService {
		public static string GenerateKey() {
			byte[] buffer = new byte[256];
			RandomNumberGenerator.Create().GetBytes(buffer);

			return Convert.ToBase64String(buffer);
		}

		public static string GenerateSalt() {
			return Hash(GenerateKey());
		}

		public static string Hash(string Source) {
			using(SHA256 hasher = SHA256.Create()) {
				byte[] result = hasher.ComputeHash(Encoding.UTF8.GetBytes(Source));
				return Convert.ToBase64String(result);
			}
		}

		public static string Encrypt(string Source, string Password) {
			return RijndaelEtM.Encrypt(Source, Password, KeySize.Aes256);
		}

		public static string Decrypt(string Source, string Password) {
			return RijndaelEtM.Decrypt(Source, Password, KeySize.Aes256);
		}
	}
}
