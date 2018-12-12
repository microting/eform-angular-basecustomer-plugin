using System.Collections.Generic;
using System.Linq;
using Customers.Pn.Infrastructure.Data;
using Customers.Pn.Infrastructure.Data.Entities;

namespace Customers.Pn.Infrastructure.Models.Fields
{
    public class FieldsUpdateModel : IModel
    {
        public List<FieldUpdateModel> Fields { get; set; } = new List<FieldUpdateModel>();

        public void Save(CustomersPnDbAnySql _dbContext)
        {

        }
        public void Update(CustomersPnDbAnySql _dbContext)
        {
            FieldsUpdateModel fieldsModel = new FieldsUpdateModel();
            List<int> list = fieldsModel.Fields.Select(s => s.Id).ToList(); // list of field ids.
            List<CustomerField> fields = _dbContext.CustomerFields
                .Where(x => list.Contains(x.FieldId))
                .ToList(); // lists the fields for the specific customer.

            foreach (CustomerField field in fields)// Itterating through a list of customerFields.
            {
                FieldUpdateModel fieldModel = fieldsModel.Fields.FirstOrDefault(x => x.Id == field.FieldId); // takes field from list of fields
                if (fieldModel != null)
                {
                    field.FieldStatus = fieldModel.FieldStatus;// sets new status for field, based on the updatemodels status.
                }
            }

            _dbContext.SaveChanges();
        }
        public void Delete(CustomersPnDbAnySql _dbContext)
        {

        }
    }
}