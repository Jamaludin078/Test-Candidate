using Microsoft.AspNetCore.Mvc;
using Web.Data.Extensions.WebApi;
using Web.Data.Models.Database;
using Web.Data.ViewModels;
using WebApp.Models;

namespace WebApp.Controllers
{
	public class PermintaanController : Controller
	{
		private readonly HttpClientApi client;
		public PermintaanController(HttpClientApi client) => this.client = client;

		public async Task<ActionResult> Index()
		{
			var data = await client.GetData<List<Permintaan>>(ApiUrl.PermintaanUrl);
			return View(data);
		}

		public ActionResult Add()
		{
			return View();
		}

		[HttpPost]
		public async Task<JsonResult> Add([FromBody] Permintaan TEntity)
		{
			ApiResponse<int> i = null;
			//if (TEntity.permintaan_no != "")
			//	i = await client.PutResponse<int>(ApiUrl.PermintaanUrl, TEntity);
			//else
			i = await client.PostResponse<int>(ApiUrl.PermintaanUrl, TEntity);

			return Json(i);
		}

		public async Task<JsonResult> Delete(int id)
		{
			var i = await client.DeleteResponse<int>($"{ApiUrl.PermintaanUrl}/{id}");
			return Json(i);
		}
	}
}
