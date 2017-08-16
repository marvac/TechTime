using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;

namespace TechTime.Models
{
    public class UserLogin : IdentityUser
    {
        [Required]
        public string Name { get; set; }
        public DateTime DateCreated { get; set; } = DateTime.Now;
    }
}
