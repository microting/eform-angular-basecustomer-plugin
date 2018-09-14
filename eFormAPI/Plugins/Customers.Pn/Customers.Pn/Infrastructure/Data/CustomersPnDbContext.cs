using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data.Entity.Infrastructure.Annotations;
using Customers.Pn.Infrastructure.Data.Entities;

namespace Customers.Pn.Infrastructure.Data
{
    public class CustomersPnDbContext : DbContext
    {
        public CustomersPnDbContext()
            : base("eFormCustomersPnConnection")
        {
            Configuration.ProxyCreationEnabled = false;
            Configuration.LazyLoadingEnabled = false;
            Database.SetInitializer<CustomersPnDbContext>(null);
        }

        public CustomersPnDbContext(string connectionString)
            : base(connectionString)
        {
            Configuration.ProxyCreationEnabled = false;
            Configuration.LazyLoadingEnabled = false;
            Database.SetInitializer<CustomersPnDbContext>(null);
        }

        public static CustomersPnDbContext Create()
        {
            return new CustomersPnDbContext();
        }

        public DbSet<CustomerPn> Customers { get; set; }
        public DbSet<FieldPn> Fields { get; set; }
        public DbSet<CustomerFieldPn> CustomerFields { get; set; }
        public DbSet<CustomerPnSettings> CustomerPnSettings { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<CustomerPn>()
                .Property(e => e.RelatedEntityId)
                .HasColumnAnnotation(
                    IndexAnnotation.AnnotationName,
                    new IndexAnnotation(new IndexAttribute { IsUnique = true }));
        }
    }
}
