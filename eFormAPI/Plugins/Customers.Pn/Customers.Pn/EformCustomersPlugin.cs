using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Customers.Pn.Abstractions;
using Customers.Pn.Infrastructure.Data;
using Customers.Pn.Infrastructure.Data.Entities;
using Customers.Pn.Infrastructure.Data.Factories;
using Customers.Pn.Infrastructure.Data.Seed;
using Customers.Pn.Infrastructure.Data.Seed.Data;
using Customers.Pn.Infrastructure.Extensions;
using Customers.Pn.Infrastructure.Models.Settings;
using Customers.Pn.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microting.eFormApi.BasePn;
using Microting.eFormApi.BasePn.Infrastructure.Database.Extensions;
using Microting.eFormApi.BasePn.Infrastructure.Models.Application;
using Microting.eFormApi.BasePn.Infrastructure.Settings;

namespace Customers.Pn
{
    public class EformCustomersPlugin : IEformPlugin
    {
        public string Name => "Microting Customers plugin";
        public string PluginId => "EFormCustomersPn";
        public string PluginPath => PluginAssembly().Location;

        public Assembly PluginAssembly()
        {
            return typeof(EformCustomersPlugin).GetTypeInfo().Assembly;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<ICustomersLocalizationService, CustomersLocalizationService>();
            services.AddTransient<IFieldsService, FieldsService>();
            services.AddTransient<ICustomersService, CustomersService>();
            services.AddTransient<ICustomersSettingsService, CustomersSettingsService>();
        }

        public void ConfigureOptionsServices(
            IServiceCollection services,
            IConfiguration configuration)
        {
            services.ConfigurePluginDbOptions<CustomersSettings>(configuration.GetSection("CustomersSettings"));
        }

        public void ConfigureDbContext(IServiceCollection services, string connectionString)
        {
            if (connectionString.ToLower().Contains("convert zero datetime"))
            {
                services.AddDbContext<CustomersPnDbAnySql>(o => o.UseMySql(connectionString,
                    b => b.MigrationsAssembly(PluginAssembly().FullName)));
            }
            else
            {
                services.AddDbContext<CustomersPnDbAnySql>(o => o.UseSqlServer(connectionString,
                    b => b.MigrationsAssembly(PluginAssembly().FullName)));
            }

            CustomersPnContextFactory contextFactory = new CustomersPnContextFactory();
            using (CustomersPnDbAnySql context = contextFactory.CreateDbContext(new[] {connectionString}))
            {
                context.Database.Migrate();
            }

            // Seed database
            SeedDatabase(connectionString);
        }

        public void Configure(IApplicationBuilder appBuilder)
        {
        }

        public MenuModel HeaderMenu(IServiceProvider serviceProvider)
        {
            var localizationService = serviceProvider
                .GetService<ICustomersLocalizationService>();

            var result = new MenuModel();
            result.LeftMenu.Add(new MenuItemModel()
            {
                Name = localizationService.GetString("Customers"),
                E2EId = "customers-pn",
                Link = "/plugins/customers-pn"
            });
            return result;
        }

        public void SeedDatabase(string connectionString)
        {
            // Get DbContext
            CustomersPnContextFactory contextFactory = new CustomersPnContextFactory();
            using (CustomersPnDbAnySql context = contextFactory.CreateDbContext(new[] {connectionString}))
            {
                // Add data
                List<string>
                    customerFields =
                        new Customer().GetPropList(); //Find all attributes for cusomers and puts them in a list
                customerFields.Remove(nameof(Customer
                    .RelatedEntityId)); // removes the related entity, because it's not relevant for fields
                foreach (string name in customerFields)
                {
                    if (!context.Fields.Any(x => x.Name == name))
                    {
                        Field newField = new Field
                        {
                            Name = name
                        };
                        newField.Create(context);
                    }
                }

                context.SaveChanges();
                Field fieldForRemove = context.Fields.FirstOrDefault(x => x.Name == nameof(Customer.RelatedEntityId));
                if (fieldForRemove != null)
                {
                    context.Fields.Remove(fieldForRemove);
                    context.SaveChanges();
                }

                List<Field> fields = context.Fields.ToList();
                foreach (Field field in fields)
                {
                    CustomerField customerField = new CustomerField
                    {
                        FieldId = field.Id,
                        FieldStatus = 1
                    };
                    if (!context.CustomerFields.Any(x => x.FieldId == field.Id))
                    {
                        context.CustomerFields.Add(customerField);
                    }
                }

                context.SaveChanges();

                // Seed configuration
                CustomersPluginSeed.SeedData(context);
            }
        }

        public void AddPluginConfig(
            IConfigurationBuilder builder,
            string connectionString)
        {
            var seedData = new CustomersConfigurationSeedData();
            var contextFactory = new CustomersPnContextFactory();
            builder.AddPluginConfiguration(
                connectionString,
                seedData,
                contextFactory);
        }
    }
}