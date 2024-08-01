using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domin.Entity
{
	public class TBViewFAQDescription
	{
        public int IdFAQDescreption { get; set; }
        public int IdFAQ { get; set; }
        public string FAQ { get; set; }
        public string Descreption { get; set; }
        public DateTime DateTimeEntry     { get; set; }
        public string DateEntry { get; set; }
        public bool CurrentState { get; set; }
        public bool Active { get; set; }
    }
}
