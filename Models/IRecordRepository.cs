using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TechTime.Models
{
    public interface IRecordRepository
    {
        Task<bool> SaveChangesAsync();
        Task AddUser(string userName, string password, string email);

        void Add(Customer customer);
        void Add(JobEntry jobEntry);
        void Add(JobType jobType);

        void UpdateJobEntry(JobEntry jobEntry);

        IEnumerable<JobEntry> GetJobEntries();
        IEnumerable<Customer> GetCustomers();
        IEnumerable<JobType> GetJobTypes();

        JobType GetJobByDesc(string desc);
    }
}
