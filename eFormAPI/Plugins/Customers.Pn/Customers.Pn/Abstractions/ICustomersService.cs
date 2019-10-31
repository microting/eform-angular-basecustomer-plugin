using System.Threading.Tasks;
using Customers.Pn.Infrastructure.Models.Customer;
using Microting.eFormApi.BasePn.Infrastructure.Models.API;

namespace Customers.Pn.Abstractions
{
    public interface ICustomersService
    {
        Task<OperationResult> CreateCustomer(CustomerFullModel customerPnCreateModel);
        Task<OperationResult> ImportCustomers(CustomerImportModel customerImportModel);
        Task<OperationResult> DeleteCustomer(int id);
        Task<OperationDataResult<CustomersModel>> GetCustomers(CustomersRequestModel pnRequestModel);
        Task<OperationDataResult<CustomerFullModel>> GetSingleCustomer(int id);
        Task<OperationResult> UpdateCustomer(CustomerFullModel customerUpdateModel);
    }
}