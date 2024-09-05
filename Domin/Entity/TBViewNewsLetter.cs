using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domin.Entity
{
    public class TBViewNewsLetter
    {
        public int IdNewsletter { get; set; }
        public string Title { get; set; }


        public int IdTemplate { get; set; }
        public string TemplateName { get; set; }
        public string Containt { get; set; }


        public int IdNewsletterGroup { get; set; }
        public string GroupName { get; set; }


        public string UserName { get; set; }
        public string ConstEmail { get; set; }
        public string ImageUser { get; set; }
        public string PhoneNumber { get; set; }
        public string CurrentEmail { get; set; }


        public string DataEntry { get; set; }
        public DateTime DateTimeEntry { get; set; }
        public bool CurrentState { get; set; }
    }
}
