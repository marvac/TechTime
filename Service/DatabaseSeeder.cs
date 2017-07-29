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

            if (!_context.Customers.Any())
            {
                _context.Customers.AddRange(
                    new Customer { CustomerId = "312", Name = "Edwin's Painting" },
                    new Customer { CustomerId = "313", Name = "Down Under Clothing" },
                    new Customer { CustomerId = "314", Name = "Henrik Clausen" },
                    new Customer { CustomerId = "315", Name = "Pocahontas Foods USA" },
                    new Customer { CustomerId = "316", Name = "Fowlers Grocery Store" },
                    new Customer { CustomerId = "317", Name = "Douglas Tucker" });

                await _context.SaveChangesAsync();
            }
        }
    }
}
