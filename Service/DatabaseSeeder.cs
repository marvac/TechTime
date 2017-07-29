using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TechTime.Models;

namespace TechTime.Service
{
    public class DatabaseSeeder
    {
        private RecordContext _context;
        private UserManager<UserLogin> _userManager;

        public DatabaseSeeder(RecordContext context, UserManager<UserLogin> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task Seed()
        {
            if (!_context.Users.Any())
            {
                UserLogin userLogin = new UserLogin
                {
                    UserName = "demo",
                    Email = "demo@example.com",
                    Name = "Demo"
                };

                await _userManager.CreateAsync(userLogin, "demo");
            }
        }
    }
}
