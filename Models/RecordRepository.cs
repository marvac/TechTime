using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TechTime.Models
{
    public class RecordRepository : IRecordRepository
    {
        private RecordContext _context;
        private UserManager<UserLogin> _userManager;
        public RecordRepository(RecordContext context, UserManager<UserLogin> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public void Add(Tech tech)
        {
            _context.Techs.Add(tech);
        }

        public void Add(Customer customer)
        {
            _context.Customers.Add(customer);
        }

        public void Add(JobEntry jobEntry)
        {
            _context.JobEntries.Add(jobEntry);
        }

        public async Task AddUser(string userName, string password, string email)
        {
            if (await _userManager.FindByEmailAsync(email) == null)
            {
                var user = new UserLogin()
                {
                    UserName = userName,
                    Email = email,
                    Level = UserLevel.Regular,
                    DateCreated = DateTime.Now
                };

                await _userManager.CreateAsync(user, password);
            }
        }

        public IEnumerable<Customer> GetCustomers()
        {
            return _context.Customers
                .ToList();
        }

        public IEnumerable<JobEntry> GetJobEntries()
        {
            return _context.JobEntries
                .Include(x => x.Customer)
                .ToList();
        }

        public IEnumerable<Tech> GetTechs()
        {
            return _context.Techs
                .ToList();
        }

        public async Task<bool> SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync()) > 0;
        }
    }
}
