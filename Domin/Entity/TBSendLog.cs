using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domin.Entity
{
    public class TBSendLog
    {
        [Key]
        public int IdSendLog { get; set; }
        public int IdNewsletter { get; set; }
        public string Title { get; set; }
        public string IdUser { get; set; }
        public string Name { get; set; }
        public string SendStatus { get; set; }
        public int SendAttempts { get; set; }
        public int FailureCount { get; set; }
        public DateTime SentDate { get; set; }
        public bool CurrentState { get; set; }
    }
}
