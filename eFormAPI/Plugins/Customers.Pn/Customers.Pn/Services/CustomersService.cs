using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using Customers.Pn.Abstractions;
using Customers.Pn.Infrastructure.Data;
using Customers.Pn.Infrastructure.Data.Entities;
using Customers.Pn.Infrastructure.Extensions;
using Customers.Pn.Infrastructure.Models;
using Customers.Pn.Infrastructure.Models.Fields;
using eFormCore;
using eFormData;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microting.eFormApi.BasePn.Abstractions;
using Microting.eFormApi.BasePn.Infrastructure.Models.API;
using Newtonsoft.Json.Linq;

namespace Customers.Pn.Services
{
    public class CustomersService : ICustomersService
    {
        private readonly IEFormCoreService _coreHelper;
        private readonly ILogger<CustomersService> _logger;
        private readonly CustomersPnDbAnySql _dbContext;
        private readonly ICustomersLocalizationService _customersLocalizationService;

        public CustomersService(ILogger<CustomersService> logger,
            CustomersPnDbAnySql dbContext,
            IEFormCoreService coreHelper,
            ICustomersLocalizationService customersLocalizationService)
        {
            _logger = logger;
            _dbContext = dbContext;
            _coreHelper = coreHelper;
            _customersLocalizationService = customersLocalizationService;
        }


        public OperationDataResult<CustomersModel> GetCustomers(CustomersRequestModel pnRequestModel)
        {
            try
            {
                CustomersModel customersPnModel = new CustomersModel();
                IQueryable<Customer> customersQuery = _dbContext.Customers.AsQueryable();
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

                List<Customer> customers = customersQuery.ToList();
                customersPnModel.Total = _dbContext.Customers.Count();
                List<FieldUpdateModel> fields = _dbContext.CustomerFields
                    .Include("Field")
                    .Select(x => new FieldUpdateModel()
                    {
                        FieldStatus = x.FieldStatus,
                        Id = x.FieldId,
                        Name = x.Field.Name,
                    }).ToList();

                foreach (Customer customer in customers)
                {
                    CustomerModel customerModel = new CustomerModel()
                    {
                        Id = customer.Id,
                    };
                    foreach (FieldUpdateModel field in fields)
                    {
                        if (field.FieldStatus == 1)
                        {
                            FieldModel fieldModel = new FieldModel
                            {
                                Id = field.Id,
                                Name = field.Name,
                            };
                            object val = customer.GetPropValue(field.Name);
                            if (val != null)
                            {
                                if (val is DateTime date)
                                {
                                    string text = date.ToString("yyy/MM/dd HH:mm:ss",
                                        CultureInfo.InvariantCulture);
                                    fieldModel.Value = text;
                                }
                                else
                                {
                                    fieldModel.Value = val.ToString();
                                }
                            }
                            customerModel.Fields.Add(fieldModel);
                        }
                    }

                    if (customerModel.Fields.Any())
                    {
                        // Mode Id field to top
                        int index = customerModel.Fields.FindIndex(x => x.Name == "Id");
                        FieldModel item = customerModel.Fields[index];
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
                _logger.LogError(e.Message);
                return new OperationDataResult<CustomersModel>(false,
                    _customersLocalizationService.GetString("ErrorObtainingCustomersInfo"));
            }
        }

        public OperationResult ImportCustomers(CustomerImportModel customersAsJson)
        {
            {
                JToken rawJson = JRaw.Parse(customersAsJson.ImportList);
                JToken rawHeadersJson = JRaw.Parse(customersAsJson.Headers);

                JToken headers = rawHeadersJson;
                IEnumerable<JToken> customerObjects = rawJson.Skip(1);

                foreach (JToken customerObj in customerObjects)
                {
                    string customerNo = customerObj[int.Parse(headers[4]["headerValue"].ToString())].ToString();
                    Customer existingCustomer = _dbContext.Customers.SingleOrDefault(x => x.CustomerNo == customerNo);
                    if (existingCustomer == null)
                    {
                        CustomerFullModel customer = new CustomerFullModel();

                        customer.CityName = customerObj[int.Parse(headers[0]["headerValue"].ToString())].ToString(); // Cityname
                        customer.CompanyAddress = customerObj[int.Parse(headers[1]["headerValue"].ToString())].ToString(); //CompanyAddress
                        customer.CompanyName = customerObj[int.Parse(headers[2]["headerValue"].ToString())].ToString(); //Companyname
                        customer.ContactPerson = customerObj[int.Parse(headers[3]["headerValue"].ToString())].ToString(); //Contactperson
                        customer.CustomerNo = customerObj[int.Parse(headers[4]["headerValue"].ToString())].ToString(); //CustomerNumber
                        customer.CreatedDate = DateTime.UtcNow; // Createddate
                        customer.Description = customerObj[int.Parse(headers[5]["headerValue"].ToString())].ToString(); //Description
                        customer.Email = customerObj[int.Parse(headers[6]["headerValue"].ToString())].ToString(); //Email
                        customer.Phone = customerObj[int.Parse(headers[7]["headerValue"].ToString())].ToString(); //Phonenumber
                        customer.ZipCode = customerObj[int.Parse(headers[8]["headerValue"].ToString())].ToString(); //Zipcode

                        customer.Save(_dbContext);
                    }
                }
                return new OperationResult(true
                    //CustomersPnLocaleHelper.GetString("CustomerCreated")
                    );
                //return new OperationResult(false,
                //                    CustomersPnLocaleHelper.GetString("ErrorWhileCreatingCustomer"));
                /*            throw new NotImplementedException()*/
            }
        }


        public OperationDataResult<CustomerFullModel> GetSingleCustomer(int id)
        {
            try
            {
                CustomerFullModel customer = _dbContext.Customers.Select(x => new CustomerFullModel()
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
                        _customersLocalizationService.GetString("CustomerNotFound"));
                }

                return new OperationDataResult<CustomerFullModel>(true, customer);
            }
            catch (Exception e)
            {
                Trace.TraceError(e.Message);
                _logger.LogError(e.Message);
                return new OperationDataResult<CustomerFullModel>(false,
                    _customersLocalizationService.GetString("ErrorObtainingCustomersInfo"));
            }
        }

        [HttpPost]
        [Route("api/customers-pn")]
        public OperationResult CreateCustomer(CustomerFullModel customerPnCreateModel)
        {
            try
            {
                //Customer customer = new Customer()
                //{
                //    CityName = customerPnCreateModel.CityName,
                //    CompanyAddress = customerPnCreateModel.CompanyAddress,
                //    CompanyName = customerPnCreateModel.CompanyName,
                //    ContactPerson = customerPnCreateModel.ContactPerson,
                //    CreatedBy = customerPnCreateModel.CreatedBy,
                //    CustomerNo = customerPnCreateModel.CustomerNo,
                //    CreatedDate = DateTime.UtcNow,
                //    Description = customerPnCreateModel.Description,
                //    Email = customerPnCreateModel.Email,
                //    Phone = customerPnCreateModel.Phone,
                //    ZipCode = customerPnCreateModel.ZipCode
                //};
                //_dbContext.Customers.Add(customer);
                //_dbContext.SaveChanges();
                customerPnCreateModel.Save(_dbContext);
                // create item
                CustomerSettings customerSettings = _dbContext.CustomerSettings.FirstOrDefault();
                if (customerSettings?.RelatedEntityGroupId != null)
                {
                    Core core = _coreHelper.GetCore();
                    EntityGroup entityGroup = core.EntityGroupRead(customerSettings.RelatedEntityGroupId.ToString());
                    if (entityGroup == null)
                    {
                        return new OperationResult(false, "Entity group not found");
                    }

                    int nextItemUid = entityGroup.EntityGroupItemLst.Count;
                    string label = customerPnCreateModel.CompanyName + " - " + customerPnCreateModel.CompanyAddress + " - "
                        + customerPnCreateModel.ZipCode +
                                " - " + customerPnCreateModel.CityName + " - " + customerPnCreateModel.Phone + " - " +
                                customerPnCreateModel.ContactPerson;
                    if (string.IsNullOrEmpty(label))
                    {
                        label = $"Empty company {nextItemUid}";
                    }

                    EntityItem item = core.EntitySearchItemCreate(entityGroup.Id, $"{label}", $"{customerPnCreateModel.Description}",
                        nextItemUid.ToString());
                    if (item != null)
                    {
                        entityGroup = core.EntityGroupRead(customerSettings.RelatedEntityGroupId.ToString());
                        if (entityGroup != null)
                        {
                            foreach (EntityItem entityItem in entityGroup.EntityGroupItemLst)
                            {
                                if (entityItem.MicrotingUUID == item.MicrotingUUID)
                                {
                                    customerPnCreateModel.RelatedEntityId = entityItem.Id;
                                }
                            }
                        }
                    }

                    _dbContext.SaveChanges();
                }

                return new OperationResult(true,
                    _customersLocalizationService.GetString("CustomerCreated"));
            }
            catch (Exception e)
            {
                Trace.TraceError(e.Message);
                _logger.LogError(e.Message);
                return new OperationResult(false,
                    _customersLocalizationService.GetString("ErrorWhileCreatingCustomer"));
            }
        }

        public OperationResult UpdateCustomer(CustomerFullModel customerUpdateModel)
        {
            try
            {
                //Customer customer = _dbContext.Customers.FirstOrDefault(x => x.Id == customerUpdateModel.Id);
                //if (customer == null)
                //{
                //    return new OperationResult(false,
                //        _customersLocalizationService.GetString("CustomerNotFound"));
                //}

                //customer.Description = customerUpdateModel.Description;
                //customer.CityName = customerUpdateModel.CityName;
                //customer.CompanyAddress = customerUpdateModel.CompanyAddress;
                //customer.ContactPerson = customerUpdateModel.ContactPerson;
                //customer.CreatedBy = customerUpdateModel.CreatedBy;
                //customer.CustomerNo = customerUpdateModel.CustomerNo;
                //customer.CompanyName = customerUpdateModel.CompanyName;
                //customer.Email = customerUpdateModel.Email;
                //customer.Phone = customerUpdateModel.Phone;
                //customer.ZipCode = customerUpdateModel.ZipCode;
                //_dbContext.SaveChanges();
                //if (customer.RelatedEntityId != null)
                //{
                //    Core core = _coreHelper.GetCore();
                //    string label = customer.CompanyName + " - " + customer.CompanyAddress + " - " + customer.ZipCode + " - " + customer.CityName + " - " + customer.Phone + " - " + customer.ContactPerson;
                //    core.EntityItemUpdate((int)customer.RelatedEntityId, label, customer.Description, "", 0);

                //}
                customerUpdateModel.Update(_dbContext);
                return new OperationDataResult<CustomersModel>(true,
                    _customersLocalizationService.GetString("CustomerUpdatedSuccessfully"));
            }
            catch (Exception e)
            {
                Trace.TraceError(e.Message);
                _logger.LogError(e.Message);
                return new OperationDataResult<CustomersModel>(false,
                    _customersLocalizationService.GetString("ErrorWhileUpdatingCustomerInfo"));
            }
        }

        public OperationResult DeleteCustomer(int id)
        {
            try
            {
                //Customer customer = _dbContext.Customers.FirstOrDefault(x => x.Id == id);
                //if (customer == null)
                //{
                //    return new OperationResult(false,
                //        _customersLocalizationService.GetString("CustomerNotFound"));
                //}

                //Core core = _coreHelper.GetCore();
                //if (customer.RelatedEntityId != null)
                //{
                //    core.EntityItemDelete((int) customer.RelatedEntityId);
                //}

                //_dbContext.Customers.Remove(customer);
                //_dbContext.SaveChanges();
                CustomerFullModel customer = new CustomerFullModel();
                customer.Delete(_dbContext);
                return new OperationResult(true,
                    _customersLocalizationService.GetString("CustomerDeletedSuccessfully"));
            }
            catch (Exception e)
            {
                Trace.TraceError(e.Message);
                _logger.LogError(e.Message);
                return new OperationDataResult<CustomerFullModel>(false,
                    _customersLocalizationService.GetString("ErrorWhileDeletingCustomer"));
            }
        }

        public OperationDataResult<CustomerSettingsModel> GetSettings()
        {
            try
            {
                CustomerSettingsModel result = new CustomerSettingsModel();
                CustomerSettings customerSettings = _dbContext.CustomerSettings.FirstOrDefault();
                if (customerSettings?.RelatedEntityGroupId != null)
                {
                    result.RelatedEntityId = (int) customerSettings.RelatedEntityGroupId;
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

        public OperationResult UpdateSettings(CustomerSettingsModel customerUpdateModel)
        {
            try
            {
                if (customerUpdateModel.RelatedEntityId == 0)
                {
                    return new OperationDataResult<CustomersModel>(true);
                }
                CustomerSettings customerSettings = _dbContext.CustomerSettings.FirstOrDefault();
                if (customerSettings == null)
                {
                    customerUpdateModel.Save(_dbContext);
                    //customerSettings = new CustomerSettings()
                    //{
                    //    RelatedEntityGroupId = customerUpdateModel.RelatedEntityId
                    //};
                    //_dbContext.CustomerSettings.Add(customerSettings);
                }
                else
                {
                    customerUpdateModel.Update(_dbContext);
                    //customerSettings.RelatedEntityGroupId = customerUpdateModel.RelatedEntityId;
                    //_dbContext.Entry(customerSettings).State = EntityState.Modified;
                }

                //_dbContext.SaveChanges();
                Core core = _coreHelper.GetCore();
                EntityGroup newEntityGroup = core.EntityGroupRead(customerUpdateModel.RelatedEntityId.ToString());
                if (newEntityGroup == null)
                {
                    return new OperationResult(false, "Entity group not found");
                }

                return new OperationDataResult<CustomersModel>(true,
                    _customersLocalizationService.GetString("CustomerUpdatedSuccessfully"));
            }
            catch (Exception e)
            {
                Trace.TraceError(e.Message);
                _logger.LogError(e.Message);
                return new OperationDataResult<CustomersModel>(false,
                    _customersLocalizationService.GetString("ErrorWhileUpdatingCustomerInfo"));
            }
        }
    }
}