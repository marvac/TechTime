using System.ComponentModel.DataAnnotations;


namespace TechTime.Models
{
    public class JobType
    {
        [Key]
        public string Description { get; set; }
        public double DefaultRate { get; set; }
        public string ColorCode { get; set; }
    }
}
