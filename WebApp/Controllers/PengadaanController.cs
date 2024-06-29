using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Web.Data.Extensions.WebApi;
using Web.Data.Models.Database;
using WebApp.Models;

namespace WebApp.Controllers
{
	public class PengadaanController : Controller
	{
		private readonly HttpClientApi client;
		public PengadaanController(HttpClientApi client) => this.client = client;

		public async Task<ActionResult> Index()
		{
			var data = await client.GetData<List<PengadaanHeader>>(ApiUrl.PengadaanUrl);
			return View(data);
		}

		public async Task<ActionResult> Add()
		{
			var kategori = await client.GetData<List<Kategori>>(ApiUrl.KategoriUrl);
			var valueList = kategori.Select(x => new SelectListItem("Text", "Value") { Text = x.nama_kategori, Value = x.kategori_id.ToString() });
			ViewBag.KategoriList = valueList;

			return View();
		}

		[HttpPost]
		public async Task<JsonResult> Add([FromBody] PengadaanHeader TEntity)
		{
			ApiResponse<int> i = null;

			i = await client.PostResponse<int>(ApiUrl.PengadaanUrl, TEntity);

			return Json(i);
		}

		public async Task<JsonResult> Delete(int id)
		{
			var i = await client.DeleteResponse<int>($"{ApiUrl.PengadaanUrl}/{id}");

			return Json(i);
		}

		public async Task<ActionResult> View(string id)
		{
			var kategori = await client.GetData<List<Kategori>>(ApiUrl.KategoriUrl);
			var valueList = kategori.Select(x => new SelectListItem("Text", "Value") { Text = x.nama_kategori, Value = x.kategori_id.ToString() });
			ViewBag.KategoriList = valueList;

			var m = await client.GetData<PengadaanHeader>($"{ApiUrl.PengadaanUrl}/view/{id}");

			return View(m);
		}
	}
}
