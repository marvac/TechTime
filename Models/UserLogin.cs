using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;

namespace TechTime.Models
{
    public class UserLogin : IdentityUser
    {
        [Required]
        public string Name { get; set; }
        public UserLevel Level { get; set; } = UserLevel.Regular;
        public DateTime DateCreated { get; set; } = DateTime.Now;
    }

    public enum UserLevel
    {
        Regular = 0,
        Administrator = 1
    }
}
