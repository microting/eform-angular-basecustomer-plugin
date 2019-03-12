using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Customers.Pn.Abstractions;
using Customers.Pn.Infrastructure.Data;
using Customers.Pn.Infrastructure.Models.Customer;
using Customers.Pn.Infrastructure.Models.Settings;
using Microsoft.Extensions.Logging;
using Microting.eFormApi.BasePn.Abstractions;
using Microting.eFormApi.BasePn.Infrastructure.Helpers.PluginDbOptions;
using Microting.eFormApi.BasePn.Infrastructure.Models.API;

namespace Customers.Pn.Services
{
    public class CustomersSettingsService : ICustomersSettingsService
    {
        private readonly IEFormCoreService _coreHelper;
        private readonly ILogger<CustomersSettingsService> _logger;
        private readonly CustomersPnDbAnySql _dbContext;
        private readonly ICustomersLocalizationService _customersLocalizationService;
        private readonly IPluginDbOptions<CustomersSettings> _options;

        public CustomersSettingsService(ILogger<CustomersSettingsService> logger,
            CustomersPnDbAnySql dbContext,
            IEFormCoreService coreHelper,
            ICustomersLocalizationService customersLocalizationService,
            IPluginDbOptions<CustomersSettings> options)
        {
            _logger = logger;
            _dbContext = dbContext;
            _coreHelper = coreHelper;
            _customersLocalizationService = customersLocalizationService;
            _options = options;
        }

        public OperationDataResult<CustomerSettingsModel> GetSettings()
        {
            try
            {
                var result = new CustomerSettingsModel();
                var customerSettings = _options.Value;
                if (customerSettings?.RelatedEntityGroupId != null)
                {
                    result.RelatedEntityId = (int) customerSettings.RelatedEntityGroupId;
                    var core = _coreHelper.GetCore();
                    var entityGroup = core.EntityGroupRead(customerSettings.RelatedEntityGroupId.ToString());
                    if (entityGroup == null)
                    {
                        return new OperationDataResult<CustomerSettingsModel>(false, "Entity group not found");
                    }

                    result.RelatedEntityName = entityGroup.Name;
                }

                return new OperationDataResult<CustomerSettingsModel>(true, result);
            }
            catch (Exception e)
            {
                Trace.TraceError(e.Message);
                _logger.LogError(e.Message);
                return new OperationDataResult<CustomerSettingsModel>(false,
                    _customersLocalizationService.GetString("ErrorObtainingCustomersInfo"));
            }
        }

        public async Task<OperationResult> UpdateSettings(CustomerSettingsModel customerSettingsModel)
        {
            try
            {
                if (customerSettingsModel.RelatedEntityId == 0)
                {
                    return new OperationDataResult<CustomersModel>(true);
                }

                var core = _coreHelper.GetCore();
                var newEntityGroup = core.EntityGroupRead(customerSettingsModel.RelatedEntityId.ToString());
                if (newEntityGroup == null)
                {
                    return new OperationResult(false, "Entity group not found");
                }

                await _options.UpdateDb(
                    x => { x.RelatedEntityGroupId = customerSettingsModel.RelatedEntityId; },
                    _dbContext);

                return new OperationDataResult<CustomersModel>(true,
                    _customersLocalizationService.GetString("SettingsUpdatedSuccessfully"));
            }
            catch (Exception e)
            {
                Trace.TraceError(e.Message);
                _logger.LogError(e.Message);
                return new OperationDataResult<CustomersModel>(false,
                    _customersLocalizationService.GetString("ErrorWhileUpdatingSettings"));
            }
        }
    }
}