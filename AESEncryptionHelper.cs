namespace Helper
{
  using System;
	using System.IO;
	using System.Security.Cryptography;
	using System.Text;
	using System.Web;

	public class AESEncryptionHelper
	{
		private static readonly byte[] Key = { 111, 222, 33, 11, 24, 26, 85, 45, 114, 184, 27, 162, 37, 112, 222, 209, 241, 24, 175, 144, 173, 53, 196, 29, 24, 26, 17, 218, 131, 236, 53, 209 };
		private static readonly byte[] Vector = { 155, 68, 222, 111, 23, 3, 113, 119, 231, 121, 221, 112, 79, 32, 114, 156 };

		private readonly ICryptoTransform _encryptor;
		private readonly ICryptoTransform _decryptor;

		public AESEncryptionHelper()
		{
			var rm = new RijndaelManaged();
			_encryptor = rm.CreateEncryptor(Key, Vector);
			_decryptor = rm.CreateDecryptor(Key, Vector);
		}

		private string Encrypt(string unencrypted)
		{
			return Convert.ToBase64String(Encrypt(Encoding.Unicode.GetBytes(unencrypted)));
		}

		private string Decrypt(string encrypted)
		{
			return Encoding.Unicode.GetString(Decrypt(Convert.FromBase64String(encrypted.Replace(" ", "+"))));
		}


		public string EncryptToUrl(string unencrypted)
		{
			return HttpUtility.UrlEncode(Encrypt(unencrypted));
		}

		public string DecryptFromUrl(string encrypted)
		{
			return Decrypt(HttpUtility.UrlDecode(encrypted));
		}

		public byte[] Encrypt(byte[] buffer)
		{
			var encryptStream = new MemoryStream();
			using (var cs = new CryptoStream(encryptStream, _encryptor, CryptoStreamMode.Write))
			{
				cs.Write(buffer, 0, buffer.Length);
			}
			return encryptStream.ToArray();
		}

		public byte[] Decrypt(byte[] buffer)
		{
			var decryptStream = new MemoryStream();
			using (var cs = new CryptoStream(decryptStream, _decryptor, CryptoStreamMode.Write))
			{
				cs.Write(buffer, 0, buffer.Length);
			}
			return decryptStream.ToArray();
		}

	}


}
