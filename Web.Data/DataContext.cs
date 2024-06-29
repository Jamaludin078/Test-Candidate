using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.Data.Extensions;
using Web.Data.Models.Database;

namespace Web.Data
{
	public class DataContext : DbContext
	{
		readonly IConfiguration configuration;
		public DataContext(DbContextOptions<DataContext> options) : base(options)
		{
		}

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			optionsBuilder.UseSqlServer(DataConfigurationManager.GetConnectionString(), options =>
			{
				options.CommandTimeout((int)TimeSpan.FromMinutes(60).TotalSeconds);
			});

			//optionsBuilder.EnableSensitiveDataLogging();

			base.OnConfiguring(optionsBuilder);
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);
		}

		public DbSet<Person> Person { get; set; }
		public DbSet<Kategori> Kategori { get; set; }
		public DbSet<PengadaanHeader> PengadaanHeader { get; set; }
		public DbSet<PengadaanDetail> PengadaanDetail { get; set; }
		public DbSet<Permintaan> Permintaan { get; set; }
		public DbSet<PermintaanDetail> PermintaanDetail { get; set; }
	}
}
