using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Task_FullStackDevDotNet.Entities
{
    public class Ticket
    {
        [Key]
        public int IdTicket { get; set; }

        public required string Description { get; set; }

        public bool Status { get; set; }

        public DateTime Date { get; set; } 
    }
}
