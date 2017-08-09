using System.ComponentModel.DataAnnotations;

namespace TechTime.Models
{
    public class JobType
    {
        [Key]
        public string Description { get; set; }
        public double Rate { get; set; }
    }
}
