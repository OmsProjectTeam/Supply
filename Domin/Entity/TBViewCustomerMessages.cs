using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domin.Entity
{
    public class TBViewCustomerMessages
    {
        public int IdCustomerMessages { get; set; }
        public int IdTypesOfMessage { get; set; }
        public string TypesOfMessage { get; set; }
        public string IdUser { get; set; }
        public string ImageUser { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Title { get; set; }
        public string MessageDescription { get; set; }
        public string DataEntry { get; set; }
        public DateTime DateTimeEntry { get; set; }
        public bool CurrentState { get; set; }
    }
}
