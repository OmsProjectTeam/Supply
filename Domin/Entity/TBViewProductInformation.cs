using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domin.Entity
{
    public class TBViewProductInformation
    {
        public int IdProductInformation { get; set; }
        public string ProductCategory { get; set; }
        public int IdProductCategory { get; set; }
        public int IdTypesProduct { get; set; }
        public string TypesProduct { get; set; }
        public string ProductName { get; set; }
        public string Make { get; set; }
        public string UPC { get; set; }
        public string Qrcode { get; set; }
        public string Photo { get; set; }
        public bool Active { get; set; }
        public string DataEntry { get; set; }
        public DateTime DateTimeEntry { get; set; }
        public bool CurrentState { get; set; }
        public string Model { get; set; }
    }
}
