using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domin.Entity
{
    public class TBPurchaseDocumentation
    {
        [Key]
        public int IdPurchaseDocumentation { get; set; }
        [Required(ErrorMessageResourceType = typeof(Resource.ResourceData), ErrorMessageResourceName = "VlOrderNumber")]
        [MaxLength(50, ErrorMessageResourceType = typeof(Resource.ResourceData), ErrorMessageResourceName = "MaxLength50")]
        [MinLength(3, ErrorMessageResourceType = typeof(Resource.ResourceData), ErrorMessageResourceName = "MinLength3")]
        public string OrderNumber { get; set; }
        [Required(ErrorMessageResourceType = typeof(Resource.ResourceData), ErrorMessageResourceName = "VlStartDate")]
        public DateOnly StartDate { get; set; }
        [Required(ErrorMessageResourceType = typeof(Resource.ResourceData), ErrorMessageResourceName = "VlTransactionDate")]
        public DateOnly TransactionDate { get; set; }
        [Required(ErrorMessageResourceType = typeof(Resource.ResourceData), ErrorMessageResourceName = "VlCostOrder")]
        public decimal CostOrder { get; set; }
        public DateTime DateTimeEntry { get; set; }
        public string DataEntry { get; set; }
        public bool CurrentState { get; set; }

    }
}
