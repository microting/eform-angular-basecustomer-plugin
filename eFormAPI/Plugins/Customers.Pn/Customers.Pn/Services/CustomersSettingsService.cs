using System;
using System.Diagnostics;
using System.Linq;
using Customers.Pn.Abstractions;
using Customers.Pn.Infrastructure.Data;
using Customers.Pn.Infrastructure.Data.Entities;
using Customers.Pn.Infrastructure.Models;
using eFormCore;
using eFormData;
using Microsoft.Extensions.Logging;
using Microting.eFormApi.BasePn.Abstractions;
using Microting.eFormApi.BasePn.Infrastructure.Models.API;

namespace Customers.Pn.Services
{
    public class CustomersSettingsService : ICustomersSettingsService
    {
        private readonly IEFormCoreService _coreHelper;
        private readonly ILogger<CustomersSettingsService> _logger;
        private readonly CustomersPnDbAnySql _dbContext;
        private readonly ICustomersLocalizationService _customersLocalizationService;

        public CustomersSettingsService(ILogger<CustomersSettingsService> logger,
            CustomersPnDbAnySql dbContext,
            IEFormCoreService coreHelper,
            ICustomersLocalizationService customersLocalizationService)
        {
            _logger = logger;
            _dbContext = dbContext;
            _coreHelper = coreHelper;
            _customersLocalizationService = customersLocalizationService;
        }

        public OperationDataResult<CustomerSettingsModel> GetSettings()
        {
            try
            {
                CustomerSettingsModel result = new CustomerSettingsModel();
                CustomerSettings customerSettings = _dbContext.CustomerSettings.FirstOrDefault();
                if (customerSettings?.RelatedEntityGroupId != null)
                {
                    result.RelatedEntityId = (int)customerSettings.RelatedEntityGroupId;
                    Core core = _coreHelper.GetCore();
                    EntityGroup entityGroup = core.EntityGroupRead(customerSettings.RelatedEntityGroupId.ToString());
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

        public OperationResult UpdateSettings(CustomerSettingsModel customerSettingsModel)
        {
            try
            {
                if (customerSettingsModel.RelatedEntityId == 0)
                {
                    return new OperationDataResult<CustomersModel>(true);
                }
                CustomerSettings customerSettings = _dbContext.CustomerSettings.FirstOrDefault();
                if (customerSettings == null)
                {
                    customerSettings = new CustomerSettings()
                    {
                        RelatedEntityGroupId = customerSettingsModel.RelatedEntityId
                    };

                    customerSettings.Create(_dbContext);
                }
                else
                {
                    customerSettings.RelatedEntityGroupId = customerSettingsModel.RelatedEntityId;

                    customerSettings.Update(_dbContext);
                }

                Core core = _coreHelper.GetCore();
                EntityGroup newEntityGroup = core.EntityGroupRead(customerSettingsModel.RelatedEntityId.ToString());
                if (newEntityGroup == null)
                {
                    return new OperationResult(false, "Entity group not found");
                }

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
