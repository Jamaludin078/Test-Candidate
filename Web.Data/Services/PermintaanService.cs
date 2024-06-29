using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.Data.Models.Database;

namespace Web.Data.Services
{
	public interface IPermintaanService
	{
		Task<List<Permintaan>> GetAsync();
		Task<Permintaan> GetAsync(string id);
		Task<int> AddAsync(Permintaan TEntity);
		Task<int> EditAsync(Permintaan TEntity);
		Task<int> DeleteAsync(string id);
	}
	public class PermintaanService : IPermintaanService
	{
		readonly DataContext context;

		public PermintaanService(DataContext context) => this.context = context;

		public async Task<List<Permintaan>> GetAsync()
			=> await context.Permintaan.AsNoTracking().ToListAsync();

		public async Task<Permintaan> GetAsync(string id)
			=> await context.Permintaan.AsNoTracking().Where(x => x.permintaan_no == id).FirstOrDefaultAsync();
		public async Task<int> AddAsync(Permintaan TEntity)
		{
			context.Add(TEntity);
			//await context.SaveChangesAsync();

			foreach (var item in TEntity.PermintaanDetails)
			{
				var detail = new PermintaanDetail
				{
					permintaan_no = TEntity.permintaan_no,
					item_name = item.item_name,
				};

				context.Add(detail);
				context.SaveChanges();

			}

			return await context.SaveChangesAsync();
		}
		public async Task<int> EditAsync(Permintaan TEntity)
		{
			context.Update(TEntity);

			return await context.SaveChangesAsync();
		}

		public async Task<int> DeleteAsync(string id)
		{
			var model = new Permintaan { permintaan_no = id };
			return await DeleteAsync(model);
		}

		public async Task<int> DeleteAsync(Permintaan TEntity)
		{
			context.Remove(TEntity);
			return await context.SaveChangesAsync();
		}
	}
}
