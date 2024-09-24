using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domin.Entity
{
    public class TBViewSupportTicket
    {
        public int IdSupportTicket { get; set; }
        public int IdSupportTicketType { get; set; }
        public string SupportTicketType { get; set; }
        public int IdSupportTicketStatus { get; set; }
        public string SupportTicketStatus { get; set; }
        public string IdUser { get; set; }
        public string Name { get; set; }
        public string ImageUser { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public int SupportTicketNo { get; set; }
        public string FollowUpMail { get; set; }
        public DateOnly TicketDate { get; set; }
        public string Titel { get; set; }
        public string Description { get; set; }
        public string? Photo { get; set; }
        public string DataEntry { get; set; }
        public DateTime DateTimeEntry { get; set; }
        public bool CurrentState { get; set; }
    }
}
