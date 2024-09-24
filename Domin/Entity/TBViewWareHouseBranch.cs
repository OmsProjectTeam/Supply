using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domin.Entity
{
    public class TBViewWareHouseBranch
    {
        public int IdBWareHouseBranch { get; set; }
        public int IdBWareHouse { get; set; }

        public string Description { get; set; }
        public int IdWareHouseType { get; set; }
        public string WareHouseType { get; set; }
        public string WareHouseBranchName { get; set; }
        public string Code { get; set; }
        public bool Active { get; set; }
        public string DataEntry { get; set; }
        public DateTime DateTimeEntry { get; set; }
        public bool CurrentState { get; set; }
    }
}
