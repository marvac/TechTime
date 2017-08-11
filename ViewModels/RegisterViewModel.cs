using System.ComponentModel.DataAnnotations;

namespace TechTime.ViewModels
{
    public class RegisterViewModel
    {
        [Required, MaxLength(256)]
        public string Username { get; set; }

        [Required, DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password), Compare(nameof(Password), ErrorMessage = "Passwords do not match.")]
        [Display(Description = "Confirm Password")]
        public string ConfirmPassword { get; set; }

        [Required, DataType(DataType.EmailAddress), EmailAddress(ErrorMessage = "Not a valid email address.")]
        public string Email { get; set; }
    }
}
