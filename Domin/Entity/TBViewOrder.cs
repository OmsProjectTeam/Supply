using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domin.Entity
{
    public class TBViewOrder
    {
        public int IdPurchaseOrder { get; set; }
        public int IdBondType { get; set; }
        public string BondType { get; set; }
        public int IdMerchants { get; set; }
        public string Name { get; set; }
        public string MerchantPhone { get; set; }
        public string MerchantEmaile { get; set; }
        public int IdProductCategory { get; set; }
        public string ProductCategory { get; set; }
        public int IdTypesProduct { get; set; }
        public string TypesProduct { get; set; }
        public int IdProductInformation { get; set; }
        public string ProductName { get; set; }
        public string Make { get; set; }
        public string UPC { get; set; }
        public string Photo { get; set; }
        public int IdBWareHouse { get; set; }
        public string Description { get; set; }
        public int IdBWareHouseBranch { get; set; }
        public string WareHouseBranchName { get; set; }
        public int PurchaseAuotNoumber { get; set; }  
        public string PurchaseOrderNoumber { get; set; }
        public decimal? PurchasePrice { get; set; }
        public decimal? sellingPrice { get; set; }
        public decimal? GlobalPrice { get; set; }
        public decimal? SpecialSalePrice { get; set; }
        public int? QuantityIn { get; set; }
        public int? QuantityOute { get; set; }
        public string Qrcode { get; set; }
        public string DataEntry { get; set; }
        public DateTime DateTimeEntry { get; set; }
        public bool CurrentState { get; set; }
    }
}
