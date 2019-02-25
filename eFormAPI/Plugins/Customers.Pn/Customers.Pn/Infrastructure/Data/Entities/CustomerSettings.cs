using System;
using System.Linq;
using Microting.eFormApi.BasePn.Infrastructure.Database.Base;

namespace Customers.Pn.Infrastructure.Data.Entities
{
    public class CustomerSettings : BaseEntity, IDataAccessObject
    {
        public int? RelatedEntityGroupId { get; set; }
        public void Read(CustomersPnDbAnySql dbContext)
        {
            throw new NotImplementedException();
        }

        public void Create(CustomersPnDbAnySql dbContext)
        {
            CustomerSettings customerSettings = new CustomerSettings {RelatedEntityGroupId = RelatedEntityGroupId};

            dbContext.CustomerSettings.Add(customerSettings);
            dbContext.SaveChanges();
        }

        public void Update(CustomersPnDbAnySql dbContext)
        {
            CustomerSettings customerSettings = dbContext.CustomerSettings.FirstOrDefault();

            if (customerSettings == null)
            {
                throw new ArgumentNullException($"Could not find Setting with id {RelatedEntityGroupId}");
            }

            customerSettings.RelatedEntityGroupId = RelatedEntityGroupId;

            if (dbContext.ChangeTracker.HasChanges())
            {
                dbContext.SaveChanges();
            }
        }

        public void Delete(CustomersPnDbAnySql dbContext)
        {
            throw new NotImplementedException();
        }
    }
}
