using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web.Data.Extensions.WebApi
{
	public class ApiResponse<T>
	{
		public ApiResponse()
		{
			Message = "OK";
			Success = true;
		}

		public ApiResponse(T data)
		{
			if (data == null)
			{
				Message = "Fail";
				Success = false;
			}
			else
			{
				var type = data.GetType();
				if (typeof(IEnumerable).IsAssignableFrom(type))
				{
					var obj = data as IEnumerable<object>;
					var dyn = data as System.Dynamic.ExpandoObject;
					var count = obj == null ? dyn.Count() : obj.Count();

					if (count == 0)
					{
						Data = data;
						Message = $"Fail. Data count 0 from {type.UnderlyingSystemType.FullName}";
						Success = false;
						return;
					}
				}

				Data = data;
				Message = "OK";
				Success = true;
				SetJson(data);
			}
		}

		public ApiResponse(T data, bool success)
		{
			Data = data;
			Message = success ? "OK" : "Fail";
			Success = success;
			SetJson(data);
		}

		public ApiResponse(T data, bool success, string message)
		{
			Data = data;
			Message = message;
			Success = success;
			SetJson(data);
		}


		public ApiResponse(T data, bool success, string message, string json)
		{
			Data = data;
			Message = message;
			Success = success;
			Json = json;
		}

		[JsonProperty("success")]
		public bool Success { get; set; }

		[JsonProperty("message")]
		public string Message { get; set; }

		[JsonProperty("data")]
		public T Data { get; set; }

		[JsonProperty("json")]
		public string Json { get; set; }

		private void SetJson(T data)
		{
			if (data == null)
				return;

			var type = data.GetType();
			if (typeof(IEnumerable).IsAssignableFrom(type))
			{
				if ((data as IEnumerable<object>).Count() == 0)
				{
					Json = "[]";
					return;
				}
			}

			var setting = new JsonSerializerSettings
			{
				Formatting = Formatting.Indented,
				ContractResolver = new CamelCasePropertyNamesContractResolver()
			};

			if (data is string || type.IsPrimitive)
				Json = $@"{{""value"": ""{Convert.ToString(data)}""}}";
			else
				Json = JsonConvert.SerializeObject(data, setting);
		}
	}
}
