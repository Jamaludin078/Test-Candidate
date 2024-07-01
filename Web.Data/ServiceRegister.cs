using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.Data.Services;

namespace Web.Data
{
	public static class ServiceRegister
	{
		public static void RegisterDbService(this IServiceCollection services, IConfiguration configuration)
		{
			services.AddScoped<IKategoriService, KategoriService>();
			services.AddScoped<IPengadaanService, PengadaanService>();
		}
	}
}
