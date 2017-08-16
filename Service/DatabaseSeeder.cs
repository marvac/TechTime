using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
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
        private RoleManager<IdentityRole> _roleManager;
        private ILogger<DatabaseSeeder> _logger;

        public DatabaseSeeder(RecordContext context,
            UserManager<UserLogin> userManager,
            RoleManager<IdentityRole> roleManager,
            ILogger<DatabaseSeeder> logger)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
            _logger = logger;
        }

        public async Task Seed()
        {

            if (!(await _roleManager.RoleExistsAsync(Constants.StandardRole)))
            {
                var roleResult = await _roleManager.CreateAsync(new IdentityRole { Name = Constants.StandardRole });
                if (!roleResult.Succeeded)
                {
                    _logger.LogError($"Could not add role: {Constants.StandardRole}");
                }
            }

            if (!(await _roleManager.RoleExistsAsync(Constants.ManagerRole)))
            {
                var roleResult = await _roleManager.CreateAsync(new IdentityRole { Name = Constants.ManagerRole });
                if (!roleResult.Succeeded)
                {
                    _logger.LogError($"Could not add role: {Constants.ManagerRole}");
                }
            }

            if (!_context.Users.Any())
            {
                UserLogin userLogin = new UserLogin
                {
                    UserName = "Demo",
                    Email = "demo@example.com",
                    Name = "Demo"
                };

                var result = await _userManager.CreateAsync(userLogin, "demo");
                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(userLogin, Constants.StandardRole);
                    await _userManager.AddToRoleAsync(userLogin, Constants.ManagerRole);
                }
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
                    new JobType { Description = "Support", DefaultRate = 150.00, ColorCode = "255, 99, 132" },
                    new JobType { Description = "Programming", DefaultRate = 300.00, ColorCode = "255, 159, 64" },
                    new JobType { Description = "Training", DefaultRate = 150.00, ColorCode = "75, 192, 192" },
                    new JobType { Description = "Other", DefaultRate = 150.00, ColorCode = "54, 162, 235" });
            }



            if (!(await _context.SaveChangesAsync() > 0))
            {
                _logger.LogError("Could not save seed data to database");
            }
        }
    }
}
