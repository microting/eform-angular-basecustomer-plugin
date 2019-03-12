using System.Linq;
using Customers.Pn.Infrastructure.Data.Seed.Data;
using Microting.eFormApi.BasePn.Infrastructure.Database.Entities;

namespace Customers.Pn.Infrastructure.Data.Seed
{
    public class CustomersPluginSeed
    {
        public static void SeedData(CustomersPnDbAnySql dbContext)
        {
            var seedData = new CustomersConfigurationSeedData();
            var configurationList = seedData.Data;
            foreach (var configurationItem in configurationList)
            {
                if (!dbContext.PluginConfigurationValues.Any(x=>x.Id == configurationItem.Id))
                {
                    var newConfigValue = new PluginConfigurationValue()
                    {
                        Id = configurationItem.Id,
                        Value = configurationItem.Value,
                    };
                    dbContext.PluginConfigurationValues.Add(newConfigValue);
                    dbContext.SaveChanges();
                }
            }
        }
    }
}
