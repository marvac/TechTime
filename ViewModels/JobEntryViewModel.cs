using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using TechTime.Models;

namespace TechTime.ViewModels
{
    public class JobEntryViewModel
    {
        [Required]
        public string CustomerId { get; set; }

        [Required]
        [StringLength(1000, MinimumLength = 10)]
        public string WorkDescription { get; set; }

        [Required]
        [Range(0, 50, ErrorMessage = "Must enter a number between 0 and 100")]
        public double Hours { get; set; }

        [Required]
        public string JobType { get; set; }

        public string ContactName { get; set; } = string.Empty;

        public List<Customer> CustomerList { get; set; }
        public List<JobType> JobTypes { get; set; }

    }
}
