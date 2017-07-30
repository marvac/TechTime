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
        public string WorkDescription { get; set; } = string.Empty;
        public bool IsPaid { get; set; } = false;
        public string ContactName { get; set; } = string.Empty;
        public DateTime DateCreated { get; set; } = DateTime.Now;
        public string JobType { get; set; }

        private string _tech;

        public string Tech
        {
            get { return _tech; }
            set { _tech = value?.ToLower() ?? string.Empty; }
        }

    }
}
