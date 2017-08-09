using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using TechTime.Models;

namespace TechTime.ViewModels
{
    public class JobEntryViewModel
    {
        [Required]
        [Display(Name = "Customer")]
        public string CustomerId { get; set; }

        [Required]
        [Display(Name = "Description")]
        [StringLength(2048, MinimumLength = 10, ErrorMessage = "Enter a longer description (2048 character max)")]
        public string WorkDescription { get; set; }

        [Required]
        [Display(Name = "Hours Worked")]
        [Range(0, 50, ErrorMessage = "Must enter a number between 0 and 50")]
        public double Hours { get; set; }

        [Required]
        [Display(Name = "Job Type")]
        public string JobType { get; set; }

        [Display(Name = "Contact Name")]
        public string ContactName { get; set; } = string.Empty;

        public List<Customer> CustomerList { get; set; }
        public List<JobType> JobTypes { get; set; }

    }
}
