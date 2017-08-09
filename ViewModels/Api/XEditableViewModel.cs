using System.ComponentModel.DataAnnotations;

namespace TechTime.ViewModels.Api
{
    public class XEditableViewModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public int Pk { get; set; }

        [Required]
        public string Value { get; set; }
    }
}
