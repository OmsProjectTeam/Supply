using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domin.Entity
{
    public class TBWareHouseBranch
    {
        [Key]
        public int IdBWareHouseBranch { get; set; }
        public int IdBWareHouse { get; set; }
        public int IdWareHouseType { get; set; }


        [Required(ErrorMessageResourceType = typeof(Resource.ResourceData), ErrorMessageResourceName = "VlWareHouseBranch")]
        [MaxLength(100, ErrorMessageResourceType = typeof(Resource.ResourceData), ErrorMessageResourceName = "MaxLength100")]
        [MinLength(3, ErrorMessageResourceType = typeof(Resource.ResourceData), ErrorMessageResourceName = "MinLength3")]
        public string Description { get; set; }
        [Required(ErrorMessageResourceType = typeof(Resource.ResourceData), ErrorMessageResourceName = "VlCode1")]
        [MaxLength(300, ErrorMessageResourceType = typeof(Resource.ResourceData), ErrorMessageResourceName = "MaxLength300")]
        [MinLength(3, ErrorMessageResourceType = typeof(Resource.ResourceData), ErrorMessageResourceName = "MinLength3")]
        public string Code { get; set; }
        public bool Active { get; set; }
        public string DataEntry { get; set; }
        public DateTime DateTimeEntry { get; set; }
        public bool CurrentState { get; set; }
    }
}
