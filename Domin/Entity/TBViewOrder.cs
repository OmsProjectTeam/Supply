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
        public int IdProductInformation { get; set; }
        public string ProductCategory { get; set; }
        public string TypesProduct { get; set; }
        public string ProductName { get; set; }
        public string UPC { get; set; }
        public string Qrcode { get; set; }
        public string Photo { get; set; }
        public string Model { get; set; }
        public string ScrapingHtmlTitle { get; set; }
        public string brand { get; set; }
        public string? storeSku { get; set; }
        public string? storeSoSku { get; set; }
        public int IdBWareHouse { get; set; }
        public string WareHouseType { get; set; }
        public string Description { get; set; }
        public string Code { get; set; }
        public int IdBWareHouseBranch { get; set; }
        public string WareHouseBranchName { get; set; }
        public string CodeBranch { get; set; }
        public string WareHouseTypeBranch { get; set; }
        public int PurchaseAuotNoumber { get; set; }
        public int IdPurchaseDocumentation { get; set; }
        public string OrderNumber { get; set; }
        public DateOnly StartDate { get; set; }
        public DateOnly TransactionDate { get; set; }
        public decimal CostOrder { get; set; }
        public decimal sellingPrice { get; set; }
        public decimal GlobalPrice { get; set; }
        public int QuantityIn { get; set; }
        public int QuantityOute { get; set; }
        public string QrcodeOrder { get; set; }
        public string DataEntry { get; set; }
        public DateTime DateTimeEntry { get; set; }
        public bool CurrentState { get; set; }
        public string IdUser { get; set; }
        public string Name { get; set; }
        public string ImageUser { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
        public string PhoneNumber { get; set; }




    }
}
