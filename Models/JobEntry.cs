using System;
using System.ComponentModel.DataAnnotations;

namespace TechTime.Models
{
    public class JobEntry
    {
        [Key]
        public int Id { get; set; }
        public double Hours { get; set; }
        public Customer Customer { get; set; }
        public string Description { get; set; }
        public string Contact { get; set; } = string.Empty;
        public DateTime DateCreated { get; set; } = DateTime.Now;
        public string Type { get; set; }

        public string OwnerId { get; set; }
        public PaymentStatus Status { get; set; } = PaymentStatus.Unpaid;
    }

    public enum PaymentStatus : int
    {
        Unpaid = 0,
        Paid = 1,
        Cancelled = 2,
        Partial = 3
    }
}
