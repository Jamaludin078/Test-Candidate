using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web.Data.Models.Database
{
	public class PengadaanDetail
	{
		[Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		[DisplayName("Id")]
		public int id { get; set; }

		[MaxLength(20), StringLength(20)]
		[DisplayName("Pengadaan No")]
		public string? pengadaan_no { get; set; }

		[MaxLength(50), StringLength(50)]
		[DisplayName("Nama Item")]
		public string? nama_item { get; set; }

		[MaxLength(50), StringLength(50)]
		[DisplayName("Spesifikasi Item")]
		public string? spesifikasi_item { get; set; }

		[DisplayName("Jumlah")]
		public int? jumlah { get; set; }

	}
}
