using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Web.Data.Extensions
{
	public class DataConfigurationManager
	{
		public static string GetConnectionString()
		{
			var model = GetDataConfiguration();
			if (!string.IsNullOrWhiteSpace(model.DbPort))
				model.DbServer = $"{model.DbServer},{model.DbPort}";

			return $"Server={model.DbServer};User Id={model.DbUserId};Password={model.DbPassword};Database={model.DbName}";
		}

		public static DataConfiguration GetDataConfiguration()
		{
			string json = File.ReadAllText("config.json");
			var m = JsonConvert.DeserializeObject<DataConfiguration>(json);

			return m;
		}

		public static DataConfiguration Configuration
		{
			get
			{
				string json = File.ReadAllText("config.json");
				var m = JsonConvert.DeserializeObject<DataConfiguration>(json);
				return m;
			}
		}

		public class DataConfiguration
		{
			[Required]
			[Display(Name = "User ID")]
			public string DbUserId { get; set; }

			[Required]
			[Display(Name = "Password")]
			public string DbPassword { get; set; }

			[Required]
			[Display(Name = "Database Name")]
			public string DbName { get; set; }

			[Required]
			[Display(Name = "Server")]
			public string DbServer { get; set; }

			[Required]
			[Display(Name = "Port")]
			public string DbPort { get; set; }

			[Required]
			[Display(Name = "Application ID")]
			public string AppId { get; set; }

			[Required]
			[Display(Name = "Api Url")]
			public string ApiUrl { get; set; }

			[Required]
			[MaxLength(32)]
			[MinLength(32)]
			[Display(Name = "Encryption Key")]
			public string EncryptionKey { get; set; }

			[Required]
			[Display(Name = "Setting User Name")]
			public string SettingUserName { get; set; }

			[Required]
			[Display(Name = "Setting Password")]
			public string SettingPassword { get; set; }
		}
	}
}
