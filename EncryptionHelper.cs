// Needs the Base65 Encoding Class (In gists)

namespace Utils
{
  using System.Text;
	using System.Web;

	public sealed class EncryptionHelper
	{
		private static string Encrypt(string unencrypted)
		{
			var parsed = Encoding.Unicode.GetBytes(unencrypted);
			return Base65Encoding.Encode(parsed);
		}

		private static string Decrypt(string encrypted)
		{
			return Encoding.Unicode.GetString(Base65Encoding.Decode(encrypted));
		}


		public static string EncryptToUrl(string unencrypted)
		{
			return HttpUtility.UrlEncode(Encrypt(unencrypted));
		}

		public static string DecryptFromUrl(string encrypted)
		{
			return Decrypt(HttpUtility.UrlDecode(encrypted));
		}

	}
}
