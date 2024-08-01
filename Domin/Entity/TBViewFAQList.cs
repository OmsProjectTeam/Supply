using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domin.Entity
{
	public class TBViewFAQList
	{
        public int IdFAQList { get; set; }
        public int IdFAQ { get; set; }
        public string FAQ { get; set; }
        public string ListFAQ { get; set; }
        public DateTime DateTimeEntry { get; set; }
        public string DateEntry { get; set; }
        public bool CurrentState { get; set; }
		public bool Active { get; set; }
	}
}
