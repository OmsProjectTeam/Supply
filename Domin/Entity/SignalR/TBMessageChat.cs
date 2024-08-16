using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domin.Entity.SignalR
{
    public class TBMessageChat
    {
        [Key]
        public int IdMessageChat { get; set; }
        [Required]
        public string SenderId { get; set; }
        [Required]
        public string ReciverId { get; set; }
        [Required]
        public string Message { get; set; }
        [Required]
        public string? ImgMsg { get; set; }
        public DateTime MessageeTime { get; set; }
        public bool IsRead { get; set; }
        public bool CurrentState { get; set; }
    }
}
