using System;
using System.Linq;
using Customers.Pn.Infrastructure.Data;
using Customers.Pn.Infrastructure.Data.Entities;

namespace Customers.Pn.Infrastructure.Models
{
    public class CustomerSettingsModel : IModel
    {
        public int RelatedEntityId { get; set; }
        public string RelatedEntityName { get; set; }

        public void Save(CustomersPnDbAnySql _dbContext)
        {
            CustomerSettings customerSettings = new CustomerSettings();

            customerSettings.RelatedEntityGroupId = RelatedEntityId;

            _dbContext.CustomerSettings.Add(customerSettings);
            _dbContext.SaveChanges();
        }

        public void Update(CustomersPnDbAnySql _dbContext)
        {
            CustomerSettings customerSettings = _dbContext.CustomerSettings.FirstOrDefault();

            if (customerSettings == null)
            {
                throw new ArgumentNullException($"Could not find Setting with id {RelatedEntityId}");
            }

            customerSettings.RelatedEntityGroupId = RelatedEntityId;

            if (_dbContext.ChangeTracker.HasChanges())
            {
                _dbContext.SaveChanges();
            }
        }

        public void Delete(CustomersPnDbAnySql _dbContext)
        {

        }

    }
}