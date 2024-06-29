using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Web.Data.Extensions.WebApi;
using Web.Data.Models.Database;
using Web.Data.Services;

namespace WebApi.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class PengadaanController : ControllerBase
	{
		IPengadaanService service;
		public PengadaanController(IPengadaanService service)
		{
			this.service = service;
		}

		[HttpGet]
		public async Task<IActionResult> Get()
		{
			var result = await service.GetAsync();
			return Ok(new ApiResponse<List<PengadaanHeader>>(result));
		}

		[HttpGet("{id}")]
		public async Task<IActionResult> Get(string id)
		{
			var result = await service.GetAsync(id);
			return Ok(new ApiResponse<PengadaanHeader>(result));
		}

		[HttpGet("view/{id}")]
		public async Task<IActionResult> View(string id)
		{
			if (id.Contains("%2F"))
				id = id.Replace("%2F", "/");

			var result = await service.ViewAsync(id);
			return Ok(new ApiResponse<PengadaanHeader>(result));
		}

		[HttpPost]
		public async Task<IActionResult> Post([FromBody] PengadaanHeader model)
		{
			var result = await service.AddAsync(model);
			return Ok(new ApiResponse<int>(result));
		}

		[HttpPut]
		public async Task<IActionResult> Put(PengadaanHeader model)
		{
			var result = await service.EditAsync(model);
			return Ok(new ApiResponse<int>(result));
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> Delete(string id)
		{
			var result = await service.DeleteAsync(id);
			return Ok(new ApiResponse<int>(result));
		}
	}
}
