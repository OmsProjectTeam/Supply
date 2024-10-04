using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domin.Entity
{
    public class TBScrapingHtmlTitle
    {
        [Key]
        public int IdScrapingHtmlTitle { get; set; }
        public string ScrapingHtmlTitle { get; set; }
        public DateTime DateTimeEntry { get; set; }
        public string DataEntry { get; set; }
        public bool CurrentState { get; set; }
        public bool Active { get; set; }
    }
}
