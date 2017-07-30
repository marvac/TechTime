using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;

namespace TechTime.Models
{
    public class RecordContext : IdentityDbContext<UserLogin>
    {
        private IConfigurationRoot _config;

        public RecordContext(IConfigurationRoot config, DbContextOptions options) : base(options)
        {
            _config = config;

        }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<JobEntry> JobEntries { get; set; }
        public DbSet<JobType> JobTypes { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer(_config["ConnectionStrings:RecordContextConnection"]);


        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

        }
    }
}
