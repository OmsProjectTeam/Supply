using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domin.Entity
{
    public class TBViewSendLog
    {
        public int IdSendLog { get; set; }
        public int NewsletterId { get; set; }
        public string Content { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string SendStatus { get; set; }
        public int SendAttempts { get; set; }
        public int FailureCount { get; set; }
        public DateTime SentDate { get; set; }
        public bool CurrentState { get; set; }
    }
}
