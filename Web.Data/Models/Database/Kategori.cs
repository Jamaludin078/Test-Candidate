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
	public class Kategori
	{
		[Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		[DisplayName("Kategori Id")]
		public int kategori_id { get; set; }

		[MaxLength(50), StringLength(50)]
		[DisplayName("Nama Kategori")]
		public string? nama_kategori { get; set; }

	}
}
