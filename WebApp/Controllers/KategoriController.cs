using Microsoft.AspNetCore.Mvc;
using Web.Data.Extensions.WebApi;
using Web.Data.Models.Database;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class KategoriController : Controller
    {
        private readonly HttpClientApi client;
        public KategoriController(HttpClientApi client) => this.client = client;

        public async Task<ActionResult> Index()
        {
            var data = await client.GetData<List<Kategori>>(ApiUrl.KategoriUrl);
            return View(data);
        }

        public ActionResult Add()
        {
            return View();
        }

        public async Task<ActionResult> AddEdit(int id = 0)
        {
            var m = await client.GetData<Kategori>($"{ApiUrl.KategoriUrl}/{id}");
            if (m == null)
                return View();
            else
                return View(m);
        }

        [HttpPost]
        public async Task<JsonResult> AddEdit(Kategori TEntity)
        {
            ApiResponse<int> i = null;
            if (TEntity.kategori_id > 0)
                i = await client.PutResponse<int>(ApiUrl.KategoriUrl, TEntity);
            else
                i = await client.PostResponse<int>(ApiUrl.KategoriUrl, TEntity);

            return Json(i);
        }

        public async Task<JsonResult> Delete(int id)
        {
            var i = await client.DeleteResponse<int>($"{ApiUrl.KategoriUrl}/{id}");
            return Json(i);
        }

        public async Task<JsonResult> DeletePerson(int id)
        {
            var i = await client.DeleteResponse<int>($"{ApiUrl.KategoriUrl}/{id}");
            return Json(i);
        }
    }
}
