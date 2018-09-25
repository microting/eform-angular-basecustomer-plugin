using Customers.Pn.Enums;
using Customers.Pn.Infrastructure.Data.Entities;
using Customers.Pn.Infrastructure.Extensions;
using System.Data.Entity.Migrations;
using System.Linq;
using Customers.Pn.Infrastructure.Data;

namespace Customers.Pn.Migrations
{
    public class PnMigrationConfiguration : DbMigrationsConfiguration<CustomersPnDbContext>
    {
        public PnMigrationConfiguration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(CustomersPnDbContext context)
        {
            var customerFields = new Customer().GetPropList();
            customerFields.Remove(nameof(Customer.RelatedEntityId));
            foreach (var name in customerFields)
            {
                var field = new Field()
                {
                    Name = name
                };
                context.Fields.AddOrUpdate(x => x.Name, field);
            }

            context.SaveChanges();
            var fieldForRemove = context.Fields.FirstOrDefault(x => x.Name == nameof(Customer.RelatedEntityId));
            if (fieldForRemove != null)
            {
                context.Fields.Remove(fieldForRemove);
                context.SaveChanges();
            }
            var fields = context.Fields.ToList();
            foreach (var field in fields)
            {
                var customerField = new CustomerField
                {
                    FieldId = field.Id,
                    FieldStatus = FieldPnStatus.Enabled
                };
                if (!context.CustomerFields.Any(x => x.FieldId == field.Id))
                {
                    context.CustomerFields.Add(customerField);
                }
            }

            context.SaveChanges();
        }
    }
}