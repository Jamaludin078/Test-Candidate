using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.Data.Models.Database;

namespace Web.Data.ViewModels
{
	public class PermintaanViewModel
	{
		public string permintaan_no { get; set; }

		public int? permintaan_kategori { get; set; }
		[NotMapped]
		public List<PermintaanDetail> dt { get; set; }
	}
}
