using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.Data.Models.Database;

namespace Web.Data.Services
{
	public interface IKategoriService
	{
		Task<List<Kategori>> GetAsync();
		Task<Kategori> GetAsync(int id);
		Task<int> AddAsync(Kategori TEntity);
		Task<int> EditAsync(Kategori TEntity);
		Task<int> DeleteAsync(int id);
	}
	public class KategoriService : IKategoriService
	{
		readonly DataContext context;

		public KategoriService(DataContext context) => this.context = context;

		public async Task<List<Kategori>> GetAsync()
			=> await context.Kategori.AsNoTracking().ToListAsync();

		public async Task<Kategori> GetAsync(int id)
			=> await context.Kategori.AsNoTracking().Where(x => x.kategori_id == id).FirstOrDefaultAsync();
		public async Task<int> AddAsync(Kategori TEntity)
		{
			context.Add(TEntity);
			return await context.SaveChangesAsync();
		}

		public async Task<int> EditAsync(Kategori TEntity)
		{
			context.Update(TEntity);

			return await context.SaveChangesAsync();
		}

		public async Task<int> DeleteAsync(int id)
		{
			var model = new Kategori { kategori_id = id };
			return await DeleteAsync(model);
		}

		public async Task<int> DeleteAsync(Kategori TEntity)
		{
			context.Remove(TEntity);
			return await context.SaveChangesAsync();
		}
	}
}
