using Customers.Pn.Infrastructure.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microting.eFormApi.BasePn.Abstractions;
using Microting.eFormApi.BasePn.Infrastructure.Database.Entities;

namespace Customers.Pn.Infrastructure.Data
{
    public class CustomersPnDbAnySql : DbContext, IPluginDbContext
    {
        public CustomersPnDbAnySql() { }

        public CustomersPnDbAnySql(DbContextOptions<CustomersPnDbAnySql> options) : base(options)
        {
        }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<CustomerVersion> CustomerVersions { get; set; }
        public DbSet<Field> Fields { get; set; }
        public DbSet<CustomerField> CustomerFields { get; set; }
        public DbSet<PluginConfigurationValue> PluginConfigurationValues { get; set; }
        public DbSet<PluginConfigurationValueVersion> PluginConfigurationValueVersions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Customer>()
                .HasIndex(x => x.RelatedEntityId)
                .IsUnique();
            modelBuilder.Entity<Field>()
                .HasIndex(x => x.Name);
        }
    }
}
