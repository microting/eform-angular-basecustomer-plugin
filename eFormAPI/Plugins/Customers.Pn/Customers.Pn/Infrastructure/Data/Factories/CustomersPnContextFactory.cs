using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Customers.Pn.Infrastructure.Data.Factories
{
    public class CustomersPnContextFactory : IDesignTimeDbContextFactory<CustomersPnDbContext>
    {
        public CustomersPnDbContext CreateDbContext(string[] args)
        {
            DbContextOptionsBuilder<CustomersPnDbContext> optionsBuilder = new DbContextOptionsBuilder<CustomersPnDbContext>();
            if (args.Any())
            {
                optionsBuilder.UseSqlServer(args.FirstOrDefault());
            }
            else
            {
                optionsBuilder.UseSqlServer("...");
            }
            return new CustomersPnDbContext(optionsBuilder.Options);
        }
    }
}
