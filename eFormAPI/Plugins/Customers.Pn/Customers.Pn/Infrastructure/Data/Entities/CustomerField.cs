using System.Collections.Generic;
using System.Linq;
using Customers.Pn.Infrastructure.Models.Fields;
using Microting.eFormApi.BasePn.Abstractions;
using Microting.eFormApi.BasePn.Infrastructure.Database.Base;

namespace Customers.Pn.Infrastructure.Data.Entities
{
    public class CustomerField : BaseEntity, IDataAccessObject<CustomersPnDbAnySql>
    {
        public int FieldId { get; set; }
        public virtual Field Field { get; set; }

        public short? FieldStatus { get; set; }

        public void Create(CustomersPnDbAnySql dbContext)
        {
            throw new System.NotImplementedException();
        }

        public void Update(CustomersPnDbAnySql dbContext)
        {
            throw new System.NotImplementedException();
        }

        public void Delete(CustomersPnDbAnySql dbContext)
        {
            throw new System.NotImplementedException();
        }

        public void UpdateFields(CustomersPnDbAnySql dbContext, FieldsUpdateModel fieldsUpdate, List<CustomerField> customerFields)
        {
            foreach (CustomerField field in customerFields)// Itterating through a list of customerFields.
            {
                FieldUpdateModel fieldModel = fieldsUpdate.Fields.FirstOrDefault(x => x.Id == field.FieldId); // takes field from list of fields
                if (fieldModel != null) 
                {
                    field.FieldStatus = fieldModel.FieldStatus;// sets new status for field, based on the updatemodels status.
                }
            }

            dbContext.SaveChanges();
        }
    }
}