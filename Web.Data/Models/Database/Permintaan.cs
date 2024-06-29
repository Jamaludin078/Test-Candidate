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
	public class Permintaan
	{
		[Key, Required]
		[MaxLength(20), StringLength(20)]
		[DisplayName("Permintaan No")]
		public string? permintaan_no { get; set; }

		[DisplayName("Permintaan Kategori")]
		public int? permintaan_kategori { get; set; }

		[NotMapped]
		public List<PermintaanDetail> PermintaanDetails { get; set;}
	}
}
