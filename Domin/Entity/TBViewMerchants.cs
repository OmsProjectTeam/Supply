using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domin.Entity
{
    public class TBViewMerchants
    {
        public int IdMerchants { get; set; }
        public string IdUserIdentity { get; set; }
        public string Name { get; set; }
        public string MerchantPhone { get; set; }
        public string MerchantEmaile { get; set; }
        public string MerchantWeb { get; set; }
        public string MerchantAddres { get; set; }
        public string MerchantOnerName { get; set; }
        public string MerchantOnerPhone { get; set; }
        public string MerchantOnerEmail { get; set; }
        public string Photo { get; set; }
        public string DataEntry { get; set; }
        public DateTime DateTimeEntry { get; set; }
        public bool Active { get; set; }
        public bool CurrentState { get; set; }
    }
}
