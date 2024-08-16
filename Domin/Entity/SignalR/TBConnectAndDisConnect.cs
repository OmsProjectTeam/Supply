using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domin.Entity.SignalR
{
    public class TBConnectAndDisConnect
    {
        [Key]
        public int IdConnectAndDisConnect { get; set; }
        [Required]
        public string ConnectId { get; set; }
        [Required]
        public string UserName { get; set; }
        public string UserImg { get; set; }
        public DateTime TimeConnection { get; set; }

    }
}
