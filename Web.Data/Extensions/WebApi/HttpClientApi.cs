using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Web.Data.Extensions.WebApi
{
	public class HttpClientApi
	{
		public HttpClient client { get; }
		readonly IHttpContextAccessor accessor;

		public HttpClientApi(IHttpClientFactory httpClientFactory, IHttpContextAccessor accessor)
		{
			//this.httpClientFactory = httpClientFactory;
			this.accessor = accessor;
			client = httpClientFactory.CreateClient();
			SetHttpClient();
		}

		private void SetHttpClient()
		{
			client.DefaultRequestHeaders.Clear();
			client.Timeout = TimeSpan.FromSeconds(30);
			client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
			client.DefaultRequestHeaders.AcceptEncoding.Add(new StringWithQualityHeaderValue("utf-8"));
			client.DefaultRequestHeaders.ConnectionClose = true;

			client.DefaultRequestHeaders.Add("x-api-key", "qs3yft9832ywr0hpsuvwpdgqu05uf02v");
			var token = accessor?.HttpContext?.User?.FindFirst(ClaimTypes.Authentication)?.Value;
			if (!string.IsNullOrWhiteSpace(token))
			{
				client.DefaultRequestHeaders.Remove("Authorization");
				client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
			}
		}


		/////
		public async Task<T> GetData<T>(string url)
		{
			var response = await client.GetAsync<T>(url, "data");
			return response;
		}

		//public async Task<ApiResponse<T>> PostResponse<T>(string url, object parameter)
		//{
		//	var json = JsonConvert.SerializeObject(parameter);
		//	var response = await client.PostAsync<ApiResponse<T>>(url, json, null);
		//	return response;
		//}
		public async Task<ApiResponse<T>> PostResponse<T>(string url, object parameter)
		{
			var json = JsonConvert.SerializeObject(parameter);
			var response = await client.PostAsync<ApiResponse<T>>(url, json, null);
			return response;
		}

		public async Task<ApiResponse<T>> PutResponse<T>(string url, object parameter = null)
		{
			var json = JsonConvert.SerializeObject(parameter);
			var response = await client.PutAsync<ApiResponse<T>>(url, json, null);
			return response;
		}

		public async Task<ApiResponse<T>> DeleteResponse<T>(string url)
		{
			var response = await client.DeleteAsync<ApiResponse<T>>(url, null);
			return response;
		}
	}
}
