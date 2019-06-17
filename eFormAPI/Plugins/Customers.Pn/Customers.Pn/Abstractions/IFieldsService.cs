using Customers.Pn.Infrastructure.Models.Fields;
using Microting.eFormApi.BasePn.Infrastructure.Models.API;
using Microting.eFormBaseCustomerBase.Infrastructure.Models.Fields;

namespace Customers.Pn.Abstractions
{
    public interface IFieldsService
    {
        OperationDataResult<FieldsUpdateModel> GetFields();
        OperationResult UpdateFields(FieldsUpdateModel fieldsModel);
    }
}