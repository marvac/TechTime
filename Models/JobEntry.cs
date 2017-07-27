using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using TechTime.Models.Enum;

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
        public JobType JobType { get; set; } = JobType.TechTime;
        public Tech Tech { get; set; }
    }
}
