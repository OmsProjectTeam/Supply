using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domin.Entity
{
	public class TBFAQDescreption
	{
		[Key]
        public int IdFAQDescreption { get; set; }
		public int IdFAQ { get; set; }
		[Required(ErrorMessageResourceType = typeof(Resource.ResourceData), ErrorMessageResourceName = "VlDescreption")]
		[MaxLength(1000, ErrorMessageResourceType = typeof(Resource.ResourceData), ErrorMessageResourceName = "MaxLength1000")]
		[MinLength(3, ErrorMessageResourceType = typeof(Resource.ResourceData), ErrorMessageResourceName = "MinLength3")]
		public string Descreption { get; set; }

		public DateTime DateTimeEntry { get; set; }
		public string DateEntry { get; set; }
		public bool CurrentState { get; set; }
	}

}
