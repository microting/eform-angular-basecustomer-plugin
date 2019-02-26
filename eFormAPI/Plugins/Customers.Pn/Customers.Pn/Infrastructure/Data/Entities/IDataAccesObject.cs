namespace Customers.Pn.Infrastructure.Data.Entities
{
    public interface IDataAccessObject
    {
        void Create(CustomersPnDbAnySql dbContext);

        void Update(CustomersPnDbAnySql dbContext);

        void Delete(CustomersPnDbAnySql dbContext);
    }
}
