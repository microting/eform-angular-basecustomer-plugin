using System;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Web.Http;
using Customers.Pn.Enums;
using Customers.Pn.Helpers;
using Customers.Pn.Infrastructure.Data;
using Customers.Pn.Infrastructure.Data.Entities;
using Customers.Pn.Infrastructure.Extensions;
using Customers.Pn.Infrastructure.Models.Customer;
using Customers.Pn.Infrastructure.Models.Fields;
using eFormApi.BasePn.Consts;
using eFormApi.BasePn.Infrastructure;
using eFormApi.BasePn.Infrastructure.Models.API;
using NLog;

namespace Customers.Pn.Controllers
{
    [Authorize]
    public class CustomersPnController : ApiController
    {
        private readonly Logger _logger;
        private readonly CustomersPnDbContext _dbContext;
        private readonly EFormCoreHelper _coreHelper = new EFormCoreHelper();

        public CustomersPnController()
        {
            _dbContext = CustomersPnDbContext.Create();
            _logger = LogManager.GetCurrentClassLogger();
        }

        [HttpPost]
        [Route("api/customers-pn/get-all")]
        public OperationDataResult<CustomersModel> GetCustomers(CustomersRequestModel pnRequestModel)
        {
            try
            {
                var customersPnModel = new CustomersModel();
                var customersQuery = _dbContext.Customers.AsQueryable();
                if (!string.IsNullOrEmpty(pnRequestModel.SortColumnName))
                {
                    if (pnRequestModel.IsSortDsc)
                    {
                        customersQuery = customersQuery
                            .OrderByDescending(pnRequestModel.SortColumnName);
                    }
                    else
                    {
                        customersQuery = customersQuery
                            .OrderBy(pnRequestModel.SortColumnName);
                    }
                }
                else
                {
                    customersQuery = _dbContext.Customers
                        .OrderBy(x => x.Id);
                }

                if (!string.IsNullOrEmpty(pnRequestModel.Name))
                {
                    customersQuery = customersQuery.Where(x => x.CompanyName.Contains(pnRequestModel.Name));
                }

                customersQuery = customersQuery
                    .Skip(pnRequestModel.Offset)
                    .Take(pnRequestModel.PageSize);

                var customers = customersQuery.ToList();
                customersPnModel.Total = _dbContext.Customers.Count();
                var fields = _dbContext.CustomerFields
                    .Include("Field")
                    .Select(x => new FieldPnUpdateModel()
                    {
                        FieldStatus = x.FieldStatus,
                        Id = x.FieldId,
                        Name = x.Field.Name,
                    }).ToList();

                foreach (var customer in customers)
                {
                    var customerModel = new CustomerModel()
                    {
                        Id = customer.Id,
                    };
                    foreach (var field in fields)
                    {
                        if (field.FieldStatus == FieldPnStatus.Enabled)
                        {
                            var fieldModel = new FieldPnModel
                            {
                                Id = field.Id,
                                Name = field.Name,
                            };
                            var val = customer.GetPropValue(field.Name);
                            if (val != null)
                            {
                                fieldModel.Value = val.ToString();
                            }

                            customerModel.Fields.Add(fieldModel);
                        }
                    }

                    if (customerModel.Fields.Any())
                    {
                        // Mode Id field to top
                        var index = customerModel.Fields.FindIndex(x => x.Name == "Id");
                        var item = customerModel.Fields[index];
                        customerModel.Fields[index] = customerModel.Fields[0];
                        customerModel.Fields[0] = item;
                    }

                    customersPnModel.Customers.Add(customerModel);
                }

                return new OperationDataResult<CustomersModel>(true, customersPnModel);
            }
            catch (Exception e)
            {
                Trace.TraceError(e.Message);
                _logger.Error(e);
                return new OperationDataResult<CustomersModel>(false,
                    CustomersPnLocaleHelper.GetString("ErrorObtainingCustomersInfo"));
            }
        }

        [HttpGet]
        [Route("api/customers-pn/{id}")]
        public OperationDataResult<CustomerFullModel> GetSingleCustomer(int id)
        {
            try
            {
                var customer = _dbContext.Customers.Select(x => new CustomerFullModel()
                    {
                        Id = x.Id,
                        Description = x.Description,
                        Phone = x.Phone,
                        CityName = x.CityName,
                        CustomerNo = x.CustomerNo,
                        ZipCode = x.ZipCode,
                        Email = x.Email,
                        ContactPerson = x.ContactPerson,
                        CreatedBy = x.CreatedBy,
                        CompanyAddress = x.CompanyAddress,
                        CompanyName = x.CompanyName,
                    })
                    .FirstOrDefault(x => x.Id == id);

                if (customer == null)
                {
                    return new OperationDataResult<CustomerFullModel>(false,
                        CustomersPnLocaleHelper.GetString("CustomerNotFound"));
                }

                return new OperationDataResult<CustomerFullModel>(true, customer);
            }
            catch (Exception e)
            {
                Trace.TraceError(e.Message);
                _logger.Error(e);
                return new OperationDataResult<CustomerFullModel>(false,
                    CustomersPnLocaleHelper.GetString("ErrorObtainingCustomersInfo"));
            }
        }

        [HttpPost]
        [Route("api/customers-pn")]
        public OperationResult CreateCustomer(CustomerFullModel customerPnCreateModel)
        {
            try
            {
                var customer = new Customer()
                {
                    CityName = customerPnCreateModel.CityName,
                    CompanyAddress = customerPnCreateModel.CompanyAddress,
                    CompanyName = customerPnCreateModel.CompanyName,
                    ContactPerson = customerPnCreateModel.ContactPerson,
                    CreatedBy = customerPnCreateModel.CreatedBy,
                    CustomerNo = customerPnCreateModel.CustomerNo,
                    CreatedDate = DateTime.UtcNow,
                    Description = customerPnCreateModel.Description,
                    Email = customerPnCreateModel.Email,
                    Phone = customerPnCreateModel.Phone,
                    ZipCode = customerPnCreateModel.ZipCode
                };
                _dbContext.Customers.Add(customer);
                _dbContext.SaveChanges();
                // create item
                var customerSettings = _dbContext.CustomerSettings.FirstOrDefault();
                if (customerSettings?.RelatedEntityGroupId != null)
                {
                    var core = _coreHelper.GetCore();
                    var entityGroup = core.EntityGroupRead(customerSettings.RelatedEntityGroupId.ToString());
                    if (entityGroup == null)
                    {
                        return new OperationResult(false, "Entity group not found");
                    }

                    var nextItemUid = entityGroup.EntityGroupItemLst.Count;
                    var companyName = customer.CompanyName;
                    if (string.IsNullOrEmpty(companyName))
                    {
                        companyName = $"Empty company {nextItemUid}";
                    }
                    var item = core.EntitySearchItemCreate(entityGroup.Id, $"{companyName}", "",
                        nextItemUid.ToString());
                    if (item != null)
                    {
                        entityGroup = core.EntityGroupRead(customerSettings.RelatedEntityGroupId.ToString());
                        if (entityGroup != null)
                        {
                            foreach (var entityItem in entityGroup.EntityGroupItemLst)
                            {
                                if (entityItem.MicrotingUUID == item.MicrotingUUID)
                                {
                                    customer.RelatedEntityId = entityItem.Id;
                                }
                            }
                        }
                    }
                    _dbContext.SaveChanges();
                }

                return new OperationResult(true,
                    CustomersPnLocaleHelper.GetString("CustomerCreated"));
            }
            catch (Exception e)
            {
                Trace.TraceError(e.Message);
                _logger.Error(e);
                return new OperationResult(false,
                    CustomersPnLocaleHelper.GetString("ErrorWhileCreatingCustomer"));
            }
        }

        [HttpPut]
        [Route("api/customers-pn")]
        public OperationResult UpdateCustomer(CustomerFullModel customerUpdateModel)
        {
            try
            {
                var customer = _dbContext.Customers.FirstOrDefault(x => x.Id == customerUpdateModel.Id);
                if (customer == null)
                {
                    return new OperationResult(false,
                        CustomersPnLocaleHelper.GetString("CustomerNotFound"));
                }

                customer.Description = customerUpdateModel.Description;
                customer.CityName = customerUpdateModel.CityName;
                customer.CompanyAddress = customerUpdateModel.CompanyAddress;
                customer.ContactPerson = customerUpdateModel.ContactPerson;
                customer.CreatedBy = customerUpdateModel.CreatedBy;
                customer.CustomerNo = customerUpdateModel.CustomerNo;
                customer.CompanyName = customerUpdateModel.CompanyName;
                customer.Email = customerUpdateModel.Email;
                customer.Phone = customerUpdateModel.Phone;
                customer.ZipCode = customerUpdateModel.ZipCode;
                _dbContext.SaveChanges();
                return new OperationDataResult<CustomersModel>(true,
                    CustomersPnLocaleHelper.GetString("CustomerUpdatedSuccessfully"));
            }
            catch (Exception e)
            {
                Trace.TraceError(e.Message);
                _logger.Error(e);
                return new OperationDataResult<CustomersModel>(false,
                    CustomersPnLocaleHelper.GetString("ErrorWhileUpdatingCustomerInfo"));
            }
        }


        [HttpDelete]
        [Route("api/customers-pn/{id}")]
        public OperationResult DeleteCustomer(int id)
        {
            try
            {
                var customer = _dbContext.Customers.FirstOrDefault(x => x.Id == id);
                if (customer == null)
                {
                    return new OperationResult(false,
                        CustomersPnLocaleHelper.GetString("CustomerNotFound"));
                }

                var core = _coreHelper.GetCore();
                if (customer.RelatedEntityId != null)
                {
                    core.EntityItemDelete((int) customer.RelatedEntityId);
                }

                _dbContext.Customers.Remove(customer);
                _dbContext.SaveChanges();
                return new OperationResult(true,
                    CustomersPnLocaleHelper.GetString("CustomerDeletedSuccessfully"));
            }
            catch (Exception e)
            {
                Trace.TraceError(e.Message);
                _logger.Error(e);
                return new OperationDataResult<CustomerFullModel>(false,
                    CustomersPnLocaleHelper.GetString("ErrorWhileDeletingCustomer"));
            }
        }

        [HttpGet]
        [Authorize(Roles = EformRoles.Admin)]
        [Route("api/customers-pn/settings")]
        public OperationDataResult<CustomerSettingsModel> GetSettings()
        {
            try
            {
                var result = new CustomerSettingsModel();
                var customerSettings = _dbContext.CustomerSettings.FirstOrDefault();
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
                _logger.Error(e);
                return new OperationDataResult<CustomerSettingsModel>(false,
                    CustomersPnLocaleHelper.GetString("ErrorObtainingCustomersInfo"));
            }
        }


        [HttpPost]
        [Authorize(Roles = EformRoles.Admin)]
        [Route("api/customers-pn/settings")]
        public OperationResult UpdateSettings(CustomerSettingsModel customerUpdateModel)
        {
            try
            {
                var customerSettings = _dbContext.CustomerSettings.FirstOrDefault();
                if (customerSettings == null)
                {
                    customerSettings = new CustomerSettings()
                    {
                        RelatedEntityGroupId = customerUpdateModel.RelatedEntityId
                    };
                    _dbContext.CustomerSettings.Add(customerSettings);
                }
                else
                {
                    customerSettings.RelatedEntityGroupId = customerUpdateModel.RelatedEntityId;
                    _dbContext.Entry(customerSettings).State = EntityState.Modified;
                }
                _dbContext.SaveChanges();
                var core = _coreHelper.GetCore();
                var newEntityGroup = core.EntityGroupRead(customerUpdateModel.RelatedEntityId.ToString());
                if (newEntityGroup == null)
                {
                    return new OperationResult(false, "Entity group not found");
                }

                var nextItemUid = newEntityGroup.EntityGroupItemLst.Count;
                var customers = _dbContext.Customers.ToList();
                foreach (var customer in customers)
                {
                    if (customer.RelatedEntityId != null && customer.RelatedEntityId > 0)
                    {
                        core.EntityItemDelete((int) customer.RelatedEntityId);
                    }

                    var companyName = customer.CompanyName;
                    if (string.IsNullOrEmpty(companyName))
                    {
                        companyName = $"Empty company {nextItemUid}";
                    }
                    var item = core.EntitySearchItemCreate(newEntityGroup.Id, $"{companyName}", "",
                        nextItemUid.ToString());

                    if (item != null)
                    {
                        var entityGroup = core.EntityGroupRead(customerUpdateModel.RelatedEntityId.ToString());
                        if (entityGroup != null)
                        {
                            foreach (var entityItem in entityGroup.EntityGroupItemLst)
                            {
                                if (entityItem.MicrotingUUID == item.MicrotingUUID)
                                {
                                    customer.RelatedEntityId = entityItem.Id;
                                }
                            }
                        }
                    }
                    nextItemUid++;
                }

                _dbContext.SaveChanges();
                return new OperationDataResult<CustomersModel>(true,
                    CustomersPnLocaleHelper.GetString("CustomerUpdatedSuccessfully"));
            }
            catch (Exception e)
            {
                Trace.TraceError(e.Message);
                _logger.Error(e);
                return new OperationDataResult<CustomersModel>(false,
                    CustomersPnLocaleHelper.GetString("ErrorWhileUpdatingCustomerInfo"));
            }
        }
    }
}