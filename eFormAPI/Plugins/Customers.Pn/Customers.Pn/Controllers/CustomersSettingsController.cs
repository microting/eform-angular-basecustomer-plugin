using Customers.Pn.Abstractions;
using Customers.Pn.Infrastructure.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microting.eFormApi.BasePn.Infrastructure.Database.Entities;
using Microting.eFormApi.BasePn.Infrastructure.Models.API;

namespace Customers.Pn.Controllers
{
    public class CustomersSettingsController : Controller
    {
        private readonly ICustomersSettingsService _customersSettingsService;

        public CustomersSettingsController(ICustomersSettingsService customersSettingsService)
        {
            _customersSettingsService = customersSettingsService;
        }
        
        [HttpGet]
        [Authorize(Roles = EformRole.Admin)]
        [Route("api/customers-pn/settings")]
        public OperationDataResult<CustomerSettingsModel> GetSettings()
        {
            return _customersSettingsService.GetSettings();
        }


        [HttpPost]
        [Authorize(Roles = EformRole.Admin)]
        [Route("api/customers-pn/settings")]
        public OperationResult UpdateSettings([FromBody] CustomerSettingsModel customerUpdateModel)
        {
            return _customersSettingsService.UpdateSettings(customerUpdateModel);
        }
    }
}
