using System;
using System.Diagnostics;
using System.Linq;
using Customers.Pn.Infrastructure.Data;
using Customers.Pn.Infrastructure.Helpers;
using Customers.Pn.Infrastructure.Models.Fields;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microting.eFormApi.BasePn.Infrastructure.Models.API;

namespace Customers.Pn.Services
{
    public class FieldsService : IFieldsService
    {
        private readonly ILogger<FieldsService> _logger;
        private readonly CustomersPnDbContext _dbContext;

        public FieldsService(ILogger<FieldsService> logger, 
            CustomersPnDbContext dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
        }

        public OperationDataResult<FieldsUpdateModel> GetFields()
        {
            try
            {
                var fields = _dbContext.CustomerFields
                    .Include("Field")
                    .Select(x => new FieldUpdateModel()
                    {
                        FieldStatus = x.FieldStatus,
                        Id = x.FieldId,
                        Name = x.Field.Name,
                    }).ToList();
                // Mode Id field to top
                var index = fields.FindIndex(x => x.Name == "Id");
                var item = fields[index];
                fields[index] = fields[0];
                fields[0] = item;
                var result = new FieldsUpdateModel()
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
                    CustomersPnLocaleHelper.GetString("ErrorWhileObtainingFieldsInfo"));
            }
        }

        public OperationResult UpdateFields(FieldsUpdateModel fieldsModel)
        {
            try
            {
                var list = fieldsModel.Fields.Select(s => s.Id).ToList();
                var fields = _dbContext.CustomerFields
                    .Where(x => list.Contains(x.FieldId))
                    .ToList();

                foreach (var field in fields)
                {
                    var fieldModel = fieldsModel.Fields.FirstOrDefault(x => x.Id == field.FieldId);
                    if (fieldModel != null)
                    {
                        field.FieldStatus = fieldModel.FieldStatus;
                    }
                }

                _dbContext.SaveChanges();
                return new OperationResult(true,
                    CustomersPnLocaleHelper.GetString("FieldsUpdatedSuccessfully"));
            }
            catch (Exception e)
            {
                Trace.TraceError(e.Message);
                _logger.LogError(e.Message);
                return new OperationResult(false,
                    CustomersPnLocaleHelper.GetString("ErrorWhileUpdatingFields"));
            }
        }
    }
}