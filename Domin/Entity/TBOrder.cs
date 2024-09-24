using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domin.Entity
{
    public class TBOrder
    {
        [Key]
        public int IdPurchaseOrder { get; set; }
        public int IdBondType { get; set; }
        public int IdMerchants { get; set; }
        public int IdProductCategory { get; set; }
        public int IdTypesProduct { get; set; }
        public int IdProductInformation { get; set; }
        public int IdBWareHouse { get; set; }
        public int IdBWareHouseBranch { get; set; }
        public int PurchaseAuotNoumber { get; set; }
        [Required(ErrorMessageResourceType = typeof(Resource.ResourceData), ErrorMessageResourceName = "VlPurchaseOrderNoumber")]
        [MaxLength(300, ErrorMessageResourceType = typeof(Resource.ResourceData), ErrorMessageResourceName = "MaxLength300")]
        [MinLength(3, ErrorMessageResourceType = typeof(Resource.ResourceData), ErrorMessageResourceName = "MinLength3")]
        public string PurchaseOrderNoumber { get; set; }
        public decimal? PurchasePrice { get; set; }
        public decimal? sellingPrice { get; set; }
        public decimal? GlobalPrice { get; set; }
        public decimal? SpecialSalePrice { get; set; }     
        public int? QuantityIn { get; set; }
        public int? QuantityOute { get; set; }
        [Required(ErrorMessageResourceType = typeof(Resource.ResourceData), ErrorMessageResourceName = "VlQrcode")]
        [MaxLength(1000, ErrorMessageResourceType = typeof(Resource.ResourceData), ErrorMessageResourceName = "MaxLength1000")]
        [MinLength(3, ErrorMessageResourceType = typeof(Resource.ResourceData), ErrorMessageResourceName = "MinLength3")]
        public string Qrcode { get; set; }
        public string DataEntry { get; set; }
        public DateTime DateTimeEntry { get; set; }
        public bool CurrentState { get; set; }


    }
}
