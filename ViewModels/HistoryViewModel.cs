using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using TechTime.Models;
using TechTime.Models.Enum;

namespace TechTime.ViewModels
{
    public class HistoryViewModel
    {
        [Required]
        public int Id { get; set; } //corresponds to the entry ID in the database

        [Required]
        public Customer Customer { get; set; }

        [Required]
        [StringLength(1000, MinimumLength = 10)]
        public string WorkDescription { get; set; }

        [Required]
        [Range(0, 50, ErrorMessage = "Must enter a number between 0 and 100")]
        public double Hours { get; set; }

        [Required]
        public JobType JobType { get; set; }

        public string ContactName { get; set; } = string.Empty;


    }
}
