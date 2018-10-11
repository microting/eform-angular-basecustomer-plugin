using System.Diagnostics;
using Customers.Pn.Abstractions;
using Customers.Pn.Infrastructure.Models.Customer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microting.eFormApi.BasePn.Infrastructure.Database.Entities;
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
        public OperationDataResult<CustomersModel> GetCustomers([FromBody] CustomersRequestModel pnRequestModel)
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
        public OperationResult CreateCustomer([FromBody] CustomerFullModel customerPnCreateModel)
        {
            return _customersService.CreateCustomer(customerPnCreateModel);
        }

        [HttpPut]
        [Route("api/customers-pn")]
        public OperationResult UpdateCustomer([FromBody] CustomerFullModel customerUpdateModel)
        {
            return _customersService.UpdateCustomer(customerUpdateModel);
        }

        [HttpDelete]
        [Route("api/customers-pn/{id}")]
        public OperationResult DeleteCustomer(int id)
        {
            return _customersService.DeleteCustomer(id);
        }

        [HttpGet]
        [Authorize(Roles = EformRole.Admin)]
        [Route("api/customers-pn/settings")]
        public OperationDataResult<CustomerSettingsModel> GetSettings()
        {
            return _customersService.GetSettings();
        }


        [HttpPost]
        [Authorize(Roles = EformRole.Admin)]
        [Route("api/customers-pn/settings")]
        public OperationResult UpdateSettings(CustomerSettingsModel customerUpdateModel)
        {
            return _customersService.UpdateSettings(customerUpdateModel);
        }
    }
}