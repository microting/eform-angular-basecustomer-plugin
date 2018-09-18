using Customers.Pn.Infrastructure.Models.Fields;
using Microting.eFormApi.BasePn.Infrastructure.Models.API;

namespace Customers.Pn.Services
{
    public interface IFieldsService
    {
        OperationDataResult<FieldsUpdateModel> GetFields();
        OperationResult UpdateFields(FieldsUpdateModel fieldsModel);
    }
}