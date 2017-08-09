using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
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
        private ILogger<DatabaseSeeder> _logger;

        public DatabaseSeeder(RecordContext context, UserManager<UserLogin> userManager, ILogger<DatabaseSeeder> logger)
        {
            _context = context;
            _userManager = userManager;
            _logger = logger;
        }

        public async Task Seed()
        {
            if (!_context.Users.Any())
            {
                UserLogin userLogin = new UserLogin
                {
                    UserName = "Demo",
                    Email = "demo@example.com",
                    Name = "Demo"
                };

                await _userManager.CreateAsync(userLogin, "Demo");
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
            }

            if (!_context.JobTypes.Any())
            {
                _context.JobTypes.AddRange(
                    new JobType { Description = "Support", Rate = 150.00 },
                    new JobType { Description = "Programming", Rate = 300.00 },
                    new JobType { Description = "Training", Rate = 150.00 },
                    new JobType { Description = "Other", Rate = 150.00 }
                    );
            }

            if (!(await _context.SaveChangesAsync() > 0))
            {
                _logger.LogError("Could not save seed data to database");
            }
        }
    }
}
