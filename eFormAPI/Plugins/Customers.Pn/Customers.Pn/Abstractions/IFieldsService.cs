using Customers.Pn.Infrastructure.Models.Fields;
using Microting.eFormApi.BasePn.Infrastructure.Models.API;

namespace Customers.Pn.Abstractions
{
    public interface IFieldsService
    {
        OperationDataResult<FieldsUpdateModel> GetFields();
        OperationResult UpdateFields(FieldsUpdateModel fieldsModel);
    }
}