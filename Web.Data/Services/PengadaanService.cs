using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.Data.Models.Database;

namespace Web.Data.Services
{
	public interface IPengadaanService
	{
		Task<List<PengadaanHeader>> GetAsync();
		Task<PengadaanHeader> GetAsync(string id);
		Task<int> AddAsync(PengadaanHeader TEntity);
		Task<int> EditAsync(PengadaanHeader TEntity);
		Task<int> DeleteAsync(string id);
		Task<PengadaanHeader> ViewAsync(string id);
	}
	public class PengadaanService : IPengadaanService
	{
		readonly DataContext context;

		public PengadaanService(DataContext context) => this.context = context;

		public async Task<List<PengadaanHeader>> GetAsync()
		{
			var data = await (from a in context.PengadaanHeader
							  join b in context.Kategori on a.pengadaan_kategori equals b.kategori_id
							  select new PengadaanHeader
							  {
								  pengadaan_no = a.pengadaan_no,
								  nama_kategori = b.nama_kategori,
								  pengadaan_kategori = a.pengadaan_kategori,
								  pengadaan_name = a.pengadaan_name,
								  tanggal_buat = a.tanggal_buat,
								  tanggal_butuh = a.tanggal_butuh,
								  tanggal_update = a.tanggal_update
							  }).AsNoTracking().ToListAsync();

			return data;
		}


		public async Task<PengadaanHeader> GetAsync(string id)
		{
			return await context.PengadaanHeader.AsNoTracking().Where(x => x.pengadaan_no == id).FirstOrDefaultAsync();
		}

		public async Task<int> AddAsync(PengadaanHeader TEntity)
		{
			int old_no = 0;
			int current_year = DateTime.Now.Year;
			var pengadaan_no = "";

			using (var trans = context.Database.BeginTransaction())
			{
				try
				{
					var pengadaan = await context.PengadaanHeader.OrderByDescending(o => o.tanggal_buat).ThenByDescending(t => t.pengadaan_no).FirstOrDefaultAsync();

					if (pengadaan != null)
					{
						var aa = pengadaan.pengadaan_no.Substring(7) == current_year.ToString();
						if (pengadaan.pengadaan_no.Substring(7) == current_year.ToString())
						{
							old_no = int.Parse(pengadaan.pengadaan_no.Substring(2, 4)) + 1;
							pengadaan_no = "P." + old_no.ToString().PadLeft(4, '0') + "/" + current_year;
						}
					}
					else
					{
						old_no = 1;
						pengadaan_no = "P." + old_no.ToString().PadLeft(4, '0') + "/" + current_year;
					}

					TEntity.pengadaan_no = pengadaan_no;
					TEntity.tanggal_buat = DateTime.Now;

					context.Add(TEntity);
					await context.SaveChangesAsync();
					//await context.SaveChangesAsync();

					foreach (var item in TEntity.PengadaanDetails)
					{
						var detail = new PengadaanDetail
						{
							pengadaan_no = pengadaan_no,
							nama_item = item.nama_item,
							spesifikasi_item = item.spesifikasi_item,
							jumlah = item.jumlah
						};

						context.Add(detail);
						await context.SaveChangesAsync();

					}

					trans.Commit();
					return 1;
				}
				catch (Exception ex)
				{
					trans.Rollback();
					return 0;
				}
			}
		}
		public async Task<int> EditAsync(PengadaanHeader TEntity)
		{
			context.Update(TEntity);

			return await context.SaveChangesAsync();
		}

		public async Task<int> DeleteAsync(string id)
		{
			var model = new PengadaanHeader { pengadaan_no = id };
			return await DeleteAsync(model);
		}

		public async Task<int> DeleteAsync(PengadaanHeader TEntity)
		{
			context.Remove(TEntity);
			return await context.SaveChangesAsync();
		}

		public async Task<PengadaanHeader> ViewAsync(string id)
		{
			var data = await (from a in context.PengadaanHeader
							  join b in context.Kategori on a.pengadaan_kategori equals b.kategori_id
							  where a.pengadaan_no == id
							  select new PengadaanHeader
							  {
								  pengadaan_no = id,
								  pengadaan_name = a.pengadaan_name,
								  tanggal_butuh = a.tanggal_butuh,
								  pengadaan_kategori = a.pengadaan_kategori,
								  nama_kategori = b.nama_kategori,
								  tanggal_buat = a.tanggal_buat,
								  tanggal_update = a.tanggal_update,
								  PengadaanDetails = context.PengadaanDetail.Where(x => x.pengadaan_no == id).AsNoTracking().ToList(),
							  }).AsNoTracking().FirstOrDefaultAsync();

			return data;
		}
	}
}
