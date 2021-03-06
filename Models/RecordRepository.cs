﻿using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TechTime.Models
{
    public interface IRecordRepository
    {
        Task<bool> SaveChangesAsync();

        void Add(Customer customer);
        void Add(JobEntry jobEntry);
        void Add(JobType jobType);

        void UpdateJobEntry(JobEntry jobEntry);

        IEnumerable<JobEntry> GetJobEntries();
        IEnumerable<Customer> GetCustomers();
        IEnumerable<JobType> GetJobTypes();

        JobType GetJobByDesc(string desc);
    }

    public class RecordRepository : IRecordRepository
    {
        private RecordContext _context;
        private UserManager<UserLogin> _userManager;
        public RecordRepository(RecordContext context, UserManager<UserLogin> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public void Add(Customer customer)
        {
            _context.Customers.Add(customer);
        }

        public void Add(JobEntry jobEntry)
        {
            _context.JobEntries.Add(jobEntry);
        }

        public void Add(JobType jobType)
        {
            _context.JobTypes.Add(jobType);
        }

        public void UpdateJobEntry(JobEntry jobEntry)
        {
            int primaryKey = jobEntry.Id;
            var entry = _context.JobEntries.FirstOrDefault(x => x.Id == primaryKey);
            if (entry != null)
            {
                entry = jobEntry;
            }
        }

        public IEnumerable<Customer> GetCustomers()
        {
            return _context.Customers;
        }

        public IEnumerable<JobEntry> GetJobEntries()
        {
            return _context.JobEntries
                .Include(x => x.Customer);
        }

        public IEnumerable<JobType> GetJobTypes()
        {
            return _context.JobTypes;
        }

        public JobType GetJobByDesc(string desc)
        {
            return _context.JobTypes.FirstOrDefault(x => x.Description.ToLower() == desc.ToLower());
        }

        public async Task<bool> SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync()) > 0;
        }
    }
}
