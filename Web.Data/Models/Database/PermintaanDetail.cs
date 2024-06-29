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
	public class PermintaanDetail
	{
		[Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		[DisplayName("Id")]
		public int id { get; set; }

		[MaxLength(20), StringLength(20)]
		[DisplayName("Permintaan No")]
		public string? permintaan_no { get; set; }

		[MaxLength(50), StringLength(50)]
		[DisplayName("Item Name")]
		public string? item_name { get; set; }

	}
}
