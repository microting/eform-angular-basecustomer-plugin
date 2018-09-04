using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data.Entity.Infrastructure.Annotations;
using Customers.Pn.Infrastructure.Data.Entities;

namespace Customers.Pn.Infrastructure.Data
{
    public class VehiclesPnDbContext : DbContext
    {
        public VehiclesPnDbContext()
            : base("eFormVehiclesPnConnection")
        {
            Configuration.ProxyCreationEnabled = false;
            Configuration.LazyLoadingEnabled = false;
            Database.SetInitializer<VehiclesPnDbContext>(null);
        }

        public VehiclesPnDbContext(string connectionString)
            : base(connectionString)
        {
            Configuration.ProxyCreationEnabled = false;
            Configuration.LazyLoadingEnabled = false;
            Database.SetInitializer<VehiclesPnDbContext>(null);
        }

        public static VehiclesPnDbContext Create()
        {
            return new VehiclesPnDbContext();
        }

        public DbSet<VehiclePn> Vehicles { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<VehiclePn>()
                .Property(e => e.VinNumber)
                .HasColumnAnnotation(
                    IndexAnnotation.AnnotationName,
                    new IndexAnnotation(new IndexAttribute { IsUnique = true }));
        }
    }
}
