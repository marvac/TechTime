using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

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
