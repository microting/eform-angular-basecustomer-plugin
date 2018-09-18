using Customers.Pn.Infrastructure.Models.Customer;
using Microting.eFormApi.BasePn.Infrastructure.Models.API;

namespace Customers.Pn.Services
{
    public interface ICustomersService
    {
        OperationResult CreateCustomer(CustomerFullModel customerPnCreateModel);
        OperationResult DeleteCustomer(int id);
        OperationDataResult<CustomersModel> GetCustomers(CustomersRequestModel pnRequestModel);
        OperationDataResult<CustomerFullModel> GetSingleCustomer(int id);
        OperationResult UpdateCustomer(CustomerFullModel customerUpdateModel);
    }
}