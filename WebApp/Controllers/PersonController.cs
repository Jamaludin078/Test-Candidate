using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Web.Data.Extensions.WebApi;
using Web.Data.Models.Database;
using Web.Data.Services;
using WebApp.Models;

namespace WebApp.Controllers
{
	public class PersonController : Controller
	{
		private readonly HttpClientApi client;
		IKategoriService service;
		public PersonController(HttpClientApi client, IKategoriService service)
		{
			this.client = client;
			this.service = service;
		}

		public async Task<ActionResult> Index()
		{
			var data = await client.GetData<List<Person>>(ApiUrl.PersonUrl);
			return View(data);
		}

		public ActionResult Add()
		{
			var kategoriAsetValue = client.GetData<List<Kategori>>(ApiUrl.KategoriUrl);
			var kategoriAsetList = kategoriAsetValue.Result.Select(x => new SelectListItem("Text", "Value") { Text = x.nama_kategori, Value = x.kategori_id.ToString() });
			//ViewBag.KategoriAset = kategoriAsetList;
			ViewBag.ListKategori = kategoriAsetList;

			return View();
		}

		[HttpPost]
		public async Task<JsonResult> Add(Person TEntity)
		{
			ApiResponse<int> i = null;

			if (TEntity.Age == null)
				TEntity.Age = 0;

			i = await client.PostResponse<int>(ApiUrl.PersonUrl, TEntity);

			return Json(i);
		}

		public async Task<ActionResult> Edit(int id)
		{
			var m = await client.GetData<Person>($"{ApiUrl.PersonUrl}/{id}");

			return View(m);
		}

		[HttpPost]
		public async Task<JsonResult> Edit(Person TEntity)
		{
			ApiResponse<int> i = null;
			i = await client.PutResponse<int>(ApiUrl.PersonUrl, TEntity);

			return Json(i);
		}

		public async Task<JsonResult> Delete(int id)
		{
			var i = await client.DeleteResponse<int>($"{ApiUrl.PersonUrl}/{id}");
			return Json(i);
		}

        public async Task<JsonResult> DeletePerson(int id)
        {
            var i = await client.DeleteResponse<int>($"{ApiUrl.PersonUrl}/{id}");
            return Json(i);
        }
    }
}
