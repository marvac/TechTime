using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TechTime.Models
{
    public class JobType
    {
        [Key]
        public string Description { get; set; }
        public double Rate { get; set; }
    }
}
