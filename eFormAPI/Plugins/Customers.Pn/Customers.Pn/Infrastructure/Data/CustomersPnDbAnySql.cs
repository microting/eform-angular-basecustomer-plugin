namespace Customers.Pn.Infrastructure.Data
{
    using Customers.Pn.Infrastructure.Data.Entities;
    using Microsoft.EntityFrameworkCore;

    public class CustomersPnDbAnySql : DbContext
    {

        public CustomersPnDbAnySql() { }

        public CustomersPnDbAnySql(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Field> Fields { get; set; }
        public DbSet<CustomerField> CustomerFields { get; set; }
        public DbSet<CustomerSettings> CustomerSettings { get; set; }

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
