using Web.Data.Extensions;

namespace WebApp.Models
{
	public class ApiUrl
	{
		public static string BaseUrl
		{
			get
			{
				var o = DataConfigurationManager.Configuration;
				var u = o.ApiUrl;

				if (u.EndsWith("/"))
					return u.Substring(0, u.Length - 1);
				else
					return u;
			}
		}

		//public static string AuthUrl => $"{BaseUrl}/api/authenticate";

		public static string PersonUrl => $"{BaseUrl}/api/person";//http://localhost:5149/api/person
		public static string KategoriUrl => $"{BaseUrl}/api/kategori";//http://localhost:5149/api/person
		public static string PengadaanUrl => $"{BaseUrl}/api/pengadaan";//http://localhost:5149/api/person
		public static string PermintaanUrl => $"{BaseUrl}/api/permintaan";//http://localhost:5149/api/person
	}
}
