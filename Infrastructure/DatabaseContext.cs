using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure
{
    public class DatabaseContext: DbContext
    {
        private readonly CosmosUserRepositoryOptions _option;

        public DatabaseContext(IOptions<CosmosUserRepositoryOptions> opions, DbContextOptions<DatabaseContext> dbOption)
            :base(dbOption)
        {
            _option = opions.Value;
            Database.EnsureCreatedAsync().GetAwaiter().GetResult();
        }

        public DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) 
        {
            optionsBuilder
                .UseCosmos(_option.AccountEndpoint, _option.AccountKey, _option.DatabaseName)
                .EnableSensitiveDataLogging()
                .EnableDetailedErrors();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder) 
        {
            modelBuilder.ApplyConfiguration(new UserEntityConfiguration());
        }
    }
}
