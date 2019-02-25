using Customers.Pn.Infrastructure.Models;
using Microting.eFormApi.BasePn.Infrastructure.Models.API;

namespace Customers.Pn.Abstractions
{
    public interface ICustomersSettingsService
    {
        OperationDataResult<CustomerSettingsModel> GetSettings();
        OperationResult UpdateSettings(CustomerSettingsModel customerUpdateModel);
    }
}
