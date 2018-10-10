using Customers.Pn.Abstractions;
using Customers.Pn.Infrastructure.Models.Fields;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microting.eFormApi.BasePn.Infrastructure.Models.API;

namespace Customers.Pn.Controllers
{
    [Authorize]
    public class FieldsController : Controller
    {
        private readonly IFieldsService _fieldsService;

        public FieldsController(IFieldsService fieldsService)
        {
            _fieldsService = fieldsService;
        }

        [HttpGet]
        [Route("api/fields-pn")]
        public OperationDataResult<FieldsUpdateModel> GetFields()
        {
            return _fieldsService.GetFields();
        }

        [HttpPut]
        [Route("api/fields-pn")]
        public OperationResult UpdateFields([FromBody] FieldsUpdateModel fieldsModel)
        {
            return _fieldsService.UpdateFields(fieldsModel);
        }
    }
}