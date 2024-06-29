using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Web.Data.Extensions.WebApi;
using Web.Data.Models.Database;
using Web.Data.Services;
using Web.Data.ViewModels;

namespace WebApi.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class PermintaanController : ControllerBase
	{
		IPermintaanService service;
		public PermintaanController(IPermintaanService service)
		{
			this.service = service;
		}

		[HttpGet]
		public async Task<IActionResult> Get()
		{
			var result = await service.GetAsync();
			return Ok(new ApiResponse<List<Permintaan>>(result));
		}

		[HttpGet("{id}")]
		public async Task<IActionResult> Get(string id)
		{
			var result = await service.GetAsync(id);
			return Ok(new ApiResponse<Permintaan>(result));
		}

		[HttpPost]
		public async Task<IActionResult> Post([FromBody] Permintaan model)
		{
			var result = await service.AddAsync(model);
			return Ok(new ApiResponse<int>(result));
		}

		[HttpPut]
		public async Task<IActionResult> Put(Permintaan model)
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
