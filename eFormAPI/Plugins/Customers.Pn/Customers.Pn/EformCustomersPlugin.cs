using System.Linq;
using System.Reflection;
using Customers.Pn.Abstractions;
using Customers.Pn.Infrastructure.Data;
using Customers.Pn.Infrastructure.Data.Entities;
using Customers.Pn.Infrastructure.Data.Factories;
using Customers.Pn.Infrastructure.Enums;
using Customers.Pn.Infrastructure.Extensions;
using Customers.Pn.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microting.eFormApi.BasePn;

namespace Customers.Pn
{
    public class EformCustomersPlugin : IEformPlugin
    {
        public string GetName() => "Microting Customers plugin";
        public string ConnectionStringName() => "EFormCustomersPnConnection";
        public string PluginPath() => PluginAssembly().Location;

        public Assembly PluginAssembly()
        {
            return typeof(EformCustomersPlugin).GetTypeInfo().Assembly;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<ICustomersLocalizationService, CustomersLocalizationService>();
            services.AddScoped<IFieldsService, FieldsService>();
            services.AddScoped<ICustomersService, CustomersService>();
        }

        public void ConfigureDbContext(IServiceCollection services, string connectionString)
        {
            services.AddDbContext<CustomersPnDbContext>(o => o.UseSqlServer(connectionString,
                b => b.MigrationsAssembly(PluginAssembly().FullName)));

            var contextFactory = new CustomersPnContextFactory();
            using (var context = contextFactory.CreateDbContext(new[] {connectionString}))
            {
                context.Database.Migrate();
            }
            // Seed database
            SeedDatabase(connectionString);
        }

        public void Configure(IApplicationBuilder appBuilder)
        {
        }

        public void SeedDatabase(string connectionString)
        {
            // Get DbContext
            var contextFactory = new CustomersPnContextFactory();
            using (var context = contextFactory.CreateDbContext(new[] {connectionString}))
            {
                // Add data
                var customerFields = new Customer().GetPropList();
                foreach (var name in customerFields)
                {
                    var field = new Field()
                    {
                        Name = name
                    };
                    if (!context.Fields.Any(x => x.Name == name))
                    {
                        context.Fields.Add(field);
                    }
                }
                context.SaveChanges();
                var fields = context.Fields.ToList();
                foreach (var field in fields)
                {
                    var customerField = new CustomerField
                    {
                        FieldId = field.Id,
                        FieldStatus = FieldStatus.Enabled
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
}