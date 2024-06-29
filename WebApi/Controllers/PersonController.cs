using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Resources;
using Web.Data.Extensions.WebApi;
using Web.Data.Models.Database;
using Web.Data.Services;

namespace WebApi.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class PersonController : ControllerBase
	{
		IPersonService service;
		public PersonController(IPersonService service)
		{
			this.service = service;
		}

		[HttpGet]
		public async Task<IActionResult> Get()
		{
			var result = await service.GetAsync();
			return Ok(new ApiResponse<List<Person>>(result));
		}

		[HttpGet("{id}")]
		public async Task<IActionResult> Get(int id)
		{
			var result = await service.GetAsync(id);
			return Ok(new ApiResponse<Person>(result));
		}

		[HttpPost]
		public async Task<IActionResult> Post(Person model)
		{
			var result = await service.AddAsync(model);
			return Ok(new ApiResponse<int>(result));
		}

		[HttpPut]
		public async Task<IActionResult> Put(Person model)
		{
			var result = await service.EditAsync(model);
			return Ok(new ApiResponse<int>(result));
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> Delete(int id)
		{
			var result = await service.DeleteAsync(id);
			return Ok(new ApiResponse<int>(result));
		}
	}
}
