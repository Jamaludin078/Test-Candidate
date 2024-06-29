using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http.Headers;

namespace Web.Data.Extensions.WebApi
{
	public static class HttpClientExtension
	{
		private static string GetStringToken(string json, string jsonToken)
		{
			try
			{
				if (!string.IsNullOrWhiteSpace(jsonToken) && !string.IsNullOrWhiteSpace(json))
				{
					json = json.Trim();
					if (json.StartsWith("[")) //array
					{
						var arr = JArray.Parse(json);
						json = arr.SelectToken(jsonToken, false)?.ToString();
					}
					else
					{
						var obj = JObject.Parse(json);
						//json = obj.SelectToken(jsonToken, false)?.ToString();
						json = obj.GetValue(jsonToken, StringComparison.OrdinalIgnoreCase)?.ToString();
					}
				}

				return json;
			}
			catch//(Exception ex)
			{
				return json;
			}
		}
		private static async Task<T> CreateResponse<T>(HttpResponseMessage response, string jsonToken = null)
		{
			string originalJson = "";
			var json = await response.Content.ReadAsStringAsync();
			json = Convert.ToString(json).Trim();
			originalJson = json;

			if (!response.IsSuccessStatusCode)
			{
				var valid = false;
				if (json.StartsWith("{"))
				{
					var model = JsonConvert.DeserializeObject<ErrorViewModel>(json);
					var ex = new ApiException(model.Message)
					{
						StatusCode = model.Data != null ? ((HttpStatusCode)model.Data.StatusCode) : response.StatusCode,
						Status = model.Data?.Status ?? response.ReasonPhrase,
						ErrorViewModel = model
					};
					throw ex;
				}

				var exception = new ApiException(response.ReasonPhrase)
				{
					StatusCode = response.StatusCode,
					Status = response.StatusCode.ToString(),
					Source = json
				};

				response.Dispose();
				throw exception;
			}

			if (string.IsNullOrWhiteSpace(json) || json.Equals("{}") || json.Equals("[]"))
			{
				throw new NullReferenceException("Empty json response");
			}

			if (!json.StartsWith("{") && !json.StartsWith("["))
			{
				throw new NullReferenceException("Invalid json format");
			}

			if (!string.IsNullOrWhiteSpace(jsonToken))
			{
				json = GetStringToken(json, jsonToken);
			}

			if (typeof(T) == typeof(string) || typeof(T).IsPrimitive)
			{
				var t = (T)Convert.ChangeType(json, typeof(T));
				return t;
			}

			var result = JsonConvert.DeserializeObject<T>(json);
			return result;

		}

		public static async Task<T> PostAsync<T>(this HttpClient client, string url, object parameter = null, string jsonToken = null)
		{
			if (parameter == null)
				return await client.PostAsync<T>(url, null, jsonToken);

			if (parameter.GetType() == typeof(string))
			{
				var jsonRequest = Convert.ToString(parameter);
				if (!string.IsNullOrWhiteSpace(jsonRequest))
				{
					using (var content = CreateStringContent(jsonRequest))
					{
						return await client.PostAsync<T>(url, content, jsonToken);
					}
				}
				else
				{
					return await client.PostAsync<T>(url, (object)null, jsonToken);
				}
			}
			else
			{
				using (var content = CreateobjectContent(parameter))
				{
					return await client.PostAsync<T>(url, content, jsonToken);
				}
			}
		}
		//public static async Task<T> PostAsync<T>(this HttpClient client, string url, object parameter = null, string jsonToken = null)
		//{
		//	if (parameter == null)
		//		return await client.PostAsync<T>(url, null, jsonToken);

		//	if (parameter.GetType() == typeof(string))
		//	{
		//		var jsonRequest = Convert.ToString(parameter);
		//		if (!string.IsNullOrWhiteSpace(jsonRequest))
		//		{
		//			using (var content = CreateStringContent(jsonRequest))
		//			{
		//				return await client.PostAsync<T>(url, content, jsonToken);
		//			}
		//		}
		//		else
		//		{
		//			return await client.PostAsync<T>(url, (object)null, jsonToken);
		//		}
		//	}
		//	else
		//	{
		//		using (var content = CreateobjectContent(parameter))
		//		{
		//			return await client.PostAsync<T>(url, content, jsonToken);
		//		}
		//	}
		//}
		public static async Task<T> PostAsync<T>(this HttpClient client, string url, HttpContent content, string jsonToken = null)
		{
			var r = await client.PostAsync(url, content);
			return await CreateResponse<T>(r, jsonToken);

		}
		public static async Task<T> PutAsync<T>(this HttpClient client, string url, HttpContent content, string jsonToken = null)
		{
			var r = await client.PutAsync(url, content);
			return await CreateResponse<T>(r, jsonToken);

		}
		public static async Task<T> PutAsync<T>(this HttpClient client, string url, object parameter, string jsonToken = null)
		{
			if (parameter == null)
				return await client.PutAsync<T>(url, null, jsonToken);

			if (parameter.GetType() == typeof(string))
			{
				var jsonRequest = Convert.ToString(parameter);
				if (!string.IsNullOrWhiteSpace(jsonRequest))
				{
					using (var content = CreateStringContent(jsonRequest))
					{
						return await client.PutAsync<T>(url, content, jsonToken);
					}
				}
				else
				{
					return await client.PutAsync<T>(url, null, jsonToken);
				}
			}
			else
			{
				using (var content = CreateobjectContent(parameter))
				{
					return await client.PutAsync<T>(url, content, jsonToken);
				}
			}

		}
		public static async Task<T> DeleteAsync<T>(this HttpClient client, string url, string jsonToken = null)
		{
			var r = await client.DeleteAsync(url);
			return await CreateResponse<T>(r, jsonToken);

		}
		#region Object Data
		public static async Task<T> GetAsync<T>(this HttpClient client, string url, string jsonToken = null)
		{
			var r = await client.GetAsync(url);
			return await CreateResponse<T>(r, jsonToken);

		}
		#endregion


		private static FormUrlEncodedContent CreateobjectContent(object parameter)
		{
			if (parameter == null)
				return null;

			var json = JsonConvert.SerializeObject(parameter);
			var dict = JsonConvert.DeserializeObject<Dictionary<string, string>>(json);

			return new FormUrlEncodedContent(dict);
		}
		private static StringContent CreateStringContent(string request)
		{
			if (request == null)
				return null;

			var content = new StringContent(request, Encoding.UTF8, "application/json");
			content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
			//content.Headers.ContentType.CharSet = string.Empty;
			return content;
		}

	}

	public class ApiException : Exception
	{
		public ApiException(string message)
		: base(message)
		{

		}
		public HttpStatusCode StatusCode { get; set; }
		public string Status { get; set; }
		public ErrorViewModel ErrorViewModel { get; set; }
	}

	[Serializable]
	public class ErrorViewModel
	{
		public bool Success { get; set; }
		public string Message { get; set; }
		public DataError Data { get; set; }

		[Serializable]
		public class DataError
		{
			public string Header { get; set; }
			public string Status { get; set; }
			public int StatusCode { get; set; }
			public string Description { get; set; }
			public string ExceptionType { get; set; }
			public string StackTrace { get; set; }
			public string Environment { get; set; }

		}
	}

}
