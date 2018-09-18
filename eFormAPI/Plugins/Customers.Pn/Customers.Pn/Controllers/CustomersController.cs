using Customers.Pn.Infrastructure.Models.Customer;
using Customers.Pn.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microting.eFormApi.BasePn.Infrastructure.Models.API;

namespace Customers.Pn.Controllers
{
    [Authorize]
    public class CustomersController : Controller
    {
        private readonly ICustomersService _customersService;

        public CustomersController(ICustomersService customersService)
        {
            _customersService = customersService;
        }

        [HttpPost]
        [Route("api/customers-pn/get-all")]
        public OperationDataResult<CustomersModel> GetCustomers(CustomersRequestModel pnRequestModel)
        {
            return _customersService.GetCustomers(pnRequestModel);
        }

        [HttpGet]
        [Route("api/customers-pn/{id}")]
        public OperationDataResult<CustomerFullModel> GetSingleCustomer(int id)
        {
            return _customersService.GetSingleCustomer(id);
        }

        [HttpPost]
        [Route("api/customers-pn")]
        public OperationResult CreateCustomer(CustomerFullModel customerPnCreateModel)
        {
            return _customersService.CreateCustomer(customerPnCreateModel);
        }

        [HttpPut]
        [Route("api/customers-pn")]
        public OperationResult UpdateCustomer(CustomerFullModel customerUpdateModel)
        {
            return _customersService.UpdateCustomer(customerUpdateModel);
        }

        [HttpDelete]
        [Route("api/customers-pn/{id}")]
        public OperationResult DeleteCustomer(int id)
        {
            return _customersService.DeleteCustomer(id);
        }
    }
}