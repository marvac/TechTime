using System.ComponentModel.DataAnnotations;

namespace TechTime.ViewModels.Api
{
    public class AddUserViewModel
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string Email { get; set; }
    }
}
