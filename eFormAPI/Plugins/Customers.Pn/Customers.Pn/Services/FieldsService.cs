using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Customers.Pn.Abstractions;
using Customers.Pn.Infrastructure.Models.Fields;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microting.eFormApi.BasePn.Infrastructure.Models.API;
using Microting.eFormBaseCustomerBase.Infrastructure.Data;
using Microting.eFormBaseCustomerBase.Infrastructure.Data.Entities;
using Microting.eFormBaseCustomerBase.Infrastructure.Models;
using Microting.eFormBaseCustomerBase.Infrastructure.Models.Fields;

namespace Customers.Pn.Services
{
    public class FieldsService : IFieldsService
    {
        private readonly ILogger<FieldsService> _logger;
        private readonly ICustomersLocalizationService _localizationService;
        private readonly CustomersPnDbAnySql _dbContext;

        public FieldsService(ILogger<FieldsService> logger,
            CustomersPnDbAnySql dbContext, 
            ICustomersLocalizationService localizationService)
        {
            _logger = logger;
            _dbContext = dbContext;
            _localizationService = localizationService;
        }

        public OperationDataResult<FieldsUpdateModel> GetFields()
        {
            try
            {
                List<FieldUpdateModel> fields = _dbContext.CustomerFields
                    .Include("Field")
                    .Select(x => new FieldUpdateModel()
                    {
                        FieldStatus = x.FieldStatus,
                        Id = x.FieldId,
                        Name = x.Field.Name,
                    }).ToList();
                // Mode Id field to top
                int index = fields.FindIndex(x => x.Name == "Id");
                FieldUpdateModel item = fields[index];
                fields[index] = fields[0];
                fields[0] = item;
                FieldsUpdateModel result = new FieldsUpdateModel()
                {
                    Fields = fields,
                };
                return new OperationDataResult<FieldsUpdateModel>(true, result);
            }
            catch (Exception e)
            {
                Trace.TraceError(e.Message);
                _logger.LogError(e.Message);
                return new OperationDataResult<FieldsUpdateModel>(false,
                    _localizationService.GetString("ErrorWhileObtainingFieldsInfo"));
            }
        }

        public OperationResult UpdateFields(FieldsUpdateModel fieldsModel)
        {
            try
            {
                List<int> list = fieldsModel.Fields.Select(s => s.Id).ToList(); // list of field ids.
                List<CustomerField> fields = _dbContext.CustomerFields
                    .Where(x => list.Contains(x.FieldId))
                    .ToList(); // lists the fields for the specific customer.

                if (fields.Any())
                {
                    fields.First().UpdateFields(_dbContext, fieldsModel, fields);
                }

                return new OperationResult(true,
                    _localizationService.GetString("FieldsUpdatedSuccessfully"));
            }
            catch (Exception e)
            {
                Trace.TraceError(e.Message);
                _logger.LogError(e.Message);
                return new OperationResult(false,
                    _localizationService.GetString("ErrorWhileUpdatingFields"));
            }
        }
    }
}