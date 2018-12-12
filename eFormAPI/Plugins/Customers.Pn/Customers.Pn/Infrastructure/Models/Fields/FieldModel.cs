using Customers.Pn.Infrastructure.Data;
using Customers.Pn.Infrastructure.Data.Entities;

namespace Customers.Pn.Infrastructure.Models.Fields
{
    public class FieldModel : IModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }

        public void Save(CustomersPnDbAnySql _dbContext)
        {
            Field field = new Field();
            field.Name = Name;
            field.Id = Id;

            _dbContext.Fields.Add(field);
            _dbContext.SaveChanges();
        }
        public void Update(CustomersPnDbAnySql _dbContext)
        {

        }
        public void Delete(CustomersPnDbAnySql _dbContext)
        {

        }
    }
}