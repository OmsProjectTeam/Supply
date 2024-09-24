using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domin.Entity
{
    public class TBViewNewsLetterGroup
    {
        public int IdNewsletterGroup { get; set; }
        public string GroupName { get; set; }
        public string IdUser { get; set; }
        public string ConstEmail { get; set; }
        public string CurrentEmail { get; set; }
        public string UserName { get; set; }
        public string ImageUser { get; set; }
        public string PhoneNumber { get; set; }
        public string DataEntry { get; set; }
        public DateTime DateTimeEntry { get; set; }

        public bool CurrentState { get; set; }
        public bool Active { get; set; }
    }
}
