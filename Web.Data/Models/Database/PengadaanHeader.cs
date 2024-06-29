using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace Web.Data.Models.Database
{
	public class PengadaanHeader
	{
		[Key, Required]
		[MaxLength(20), StringLength(20)]
		[DisplayName("Pengadaan No")]
		public string? pengadaan_no { get; set; }

		[MaxLength(50), StringLength(50)]
		[DisplayName("Pengadaan Name")]
		public string? pengadaan_name { get; set; }

		[DisplayName("Pengadaan Kategori")]
		public int? pengadaan_kategori { get; set; }

		[DisplayName("Tanggal Butuh")]
		public DateTime? tanggal_butuh { get; set; }
		[DisplayName("Tanggal Buat")]
		public DateTime? tanggal_buat { get; set; }

		[DisplayName("Tanggal Update")]
		public DateTime? tanggal_update { get; set; }

		[NotMapped]
		[DisplayName("Nama Kategori")]
		public string? nama_kategori { get; set; }

		[NotMapped]
		public List<PengadaanDetail> PengadaanDetails { get; set; }

	}
}
