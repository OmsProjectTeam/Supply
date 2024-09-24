using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domin.Entity.SignalR
{
    public class TBViewChatMessage
    {
        public int IdMessageChat { get; set; }
        public string SenderId { get; set; }
        public string SenderName { get; set; }
        public string SenderImage { get; set; }
        public string ReciverId { get; set; }
        public string ReciverName { get; set; }
        public string ReciverImage { get; set; }
        public string Message { get; set; }
        public string ImgMsg { get; set; }
        public DateTime MessageeTime { get; set; }
        public bool CurrentState { get; set; }
        public bool IsRead { get; set; }
        public bool OnLineRec { get; set; }
        public bool OnLineSen { get; set; }
    }
}
