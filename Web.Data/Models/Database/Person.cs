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
	public class Person
	{
		[Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		[DisplayName("Id")]
		public int Id { get; set; }

		[MaxLength(50), StringLength(50)]
		[DisplayName("First Name")]
		public string FirstName { get; set; }

		[MaxLength(50), StringLength(50)]
		[DisplayName("Last Name")]
		public string LastName { get; set; }

		[DisplayName("Age")]
		public int? Age { get; set; }
	}
}
