using System;
using System.Collections;
using System.Collections.Generic;
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
using Microting.eFormApi.BasePn.Consts;
using Microting.eFormApi.BasePn.Infrastructure;
using Microting.eFormApi.BasePn.Infrastructure.Models.API;
using Newtonsoft.Json.Linq;
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
        [Route("api/customers-pn/import-customers")]
        public OperationResult ImportCustomers(CustomerImportModel customersAsJson)
        {
            {
                JToken rawJson = JRaw.Parse(customersAsJson.ImportList);
                JToken rawHeadersJson = JRaw.Parse(customersAsJson.Headers);

                JToken headers = rawHeadersJson;
                IEnumerable<JToken> customerObjects = rawJson.Skip(1);

                foreach (JToken customerObj in customerObjects)
                {
                    int locationId;
                    if (!int.TryParse(headers[4]["headerValue"].ToString(), out locationId)
                        || !int.TryParse(headers[2]["headerValue"].ToString(), out locationId)
                        || !int.TryParse(headers[3]["headerValue"].ToString(), out locationId)
                        )
                    {
                        return new OperationResult(false,
                                            CustomersPnLocaleHelper.GetString("ErrorWhileCreatingCustomer"));
                        /*            throw new NotImplementedException()*/
                    }
                    string customerNo = customerObj[int.Parse(headers[4]["headerValue"].ToString())].ToString();
                    Customer existingCustomer = _dbContext.Customers.SingleOrDefault(x => x.CustomerNo == customerNo);
                    if (existingCustomer == null)
                    {
                        Customer customer = new Customer();

                        if (int.TryParse(headers[0]["headerValue"].ToString(), out locationId))
                        {
                            customer.CityName = customerObj[int.Parse(headers[0]["headerValue"].ToString())].ToString(); // Cityname
                        }
                        if (int.TryParse(headers[1]["headerValue"].ToString(), out locationId))
                        {
                            customer.CompanyAddress = customerObj[int.Parse(headers[1]["headerValue"].ToString())].ToString(); //CompanyAddress
                        }
                        if (int.TryParse(headers[2]["headerValue"].ToString(), out locationId))
                        {
                            customer.CompanyName = customerObj[int.Parse(headers[2]["headerValue"].ToString())].ToString(); //Companyname
                        }
                        if (int.TryParse(headers[3]["headerValue"].ToString(), out locationId))
                        {
                            customer.ContactPerson = customerObj[int.Parse(headers[3]["headerValue"].ToString())].ToString(); //Contactperson
                        }
                        if (int.TryParse(headers[4]["headerValue"].ToString(), out locationId))
                        {
                            customer.CustomerNo = customerObj[int.Parse(headers[4]["headerValue"].ToString())].ToString(); //CustomerNumber
                        }

                        customer.CreatedDate = DateTime.UtcNow; // Createddate

                        if (int.TryParse(headers[5]["headerValue"].ToString(), out locationId))
                        {
                            customer.Description = customerObj[int.Parse(headers[5]["headerValue"].ToString())].ToString(); //Description
                        }
                        if (int.TryParse(headers[6]["headerValue"].ToString(), out locationId))
                        {
                            customer.Email = customerObj[int.Parse(headers[6]["headerValue"].ToString())].ToString(); //Email
                        }
                        if (int.TryParse(headers[7]["headerValue"].ToString(), out locationId))
                        {
                            customer.Phone = customerObj[int.Parse(headers[7]["headerValue"].ToString())].ToString(); //Phonenumber
                        }
                        if (int.TryParse(headers[8]["headerValue"].ToString(), out locationId))
                        {
                            customer.ZipCode = customerObj[int.Parse(headers[8]["headerValue"].ToString())].ToString(); //Zipcode
                        }
                        _dbContext.Customers.Add(customer);
                        _dbContext.SaveChanges();
                    }                    
                    
                }
                return new OperationResult(true,
                    CustomersPnLocaleHelper.GetString("CustomerCreated"));

                //return new OperationResult(false,
                //                    CustomersPnLocaleHelper.GetString("ErrorWhileCreatingCustomer"));
                /*            throw new NotImplementedException()*/
            }
        }

        [HttpPost]
        [Route("api/customers-pn")]
        public OperationResult CreateCustomer(CustomerFullModel customerPnCreateModel)
        {
            try
            {
                Customer customer = new Customer()
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
                CustomerSettings customerSettings = _dbContext.CustomerSettings.FirstOrDefault();
                if (customerSettings?.RelatedEntityGroupId != null)
                {
                    eFormCore.Core core = _coreHelper.GetCore();
                    eFormData.EntityGroup entityGroup = core.EntityGroupRead(customerSettings.RelatedEntityGroupId.ToString());
                    if (entityGroup == null)
                    {
                        return new OperationResult(false, "Entity group not found");
                    }

                    int nextItemUid = entityGroup.EntityGroupItemLst.Count;
                    string label = customer.CompanyName;
                    label += string.IsNullOrEmpty(customer.CompanyAddress) ? "" : " - " + customer.CompanyAddress ;
                    label += string.IsNullOrEmpty(customer.ZipCode) ? "" : " - " + customer.ZipCode;
                    label += string.IsNullOrEmpty(customer.CityName) ? "" : " - " + customer.CityName;
                    label += string.IsNullOrEmpty(customer.Phone) ? "" : " - " + customer.Phone;                    
                    label += string.IsNullOrEmpty(customer.ContactPerson) ? "" : " - " + customer.ContactPerson;
                    if (label.Count(f => f == '-') == 1 && label.Contains("..."))
                    {
                        label = label.Replace(" - ", "");
                    }

                    if (string.IsNullOrEmpty(label))
                    {
                        label = $"Empty company {nextItemUid}";
                    }
                    eFormData.EntityItem item = core.EntitySearchItemCreate(entityGroup.Id, $"{label}", $"{customer.Description}",
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
                Customer customer = _dbContext.Customers.FirstOrDefault(x => x.Id == customerUpdateModel.Id);
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
                eFormCore.Core core = _coreHelper.GetCore();


                string label = customer.CompanyName;
                label += string.IsNullOrEmpty(customer.CompanyAddress) ? "" : " - " + customer.CompanyAddress;
                label += string.IsNullOrEmpty(customer.ZipCode) ? "" : " - " + customer.ZipCode;
                label += string.IsNullOrEmpty(customer.CityName) ? "" : " - " + customer.CityName;
                label += string.IsNullOrEmpty(customer.Phone) ? "" : " - " + customer.Phone;
                label += string.IsNullOrEmpty(customer.ContactPerson) ? "" : " - " + customer.ContactPerson;
                if (label.Count(f => f == '-') == 1 && label.Contains("..."))
                {
                    label = label.Replace(" - ", "");
                }
                core.EntityItemUpdate((int)customer.RelatedEntityId, label, customer.Description.Replace("</p>", "<br>").Replace("<p>", ""), "", 0);
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

                //var nextItemUid = newEntityGroup.EntityGroupItemLst.Count;
                //var customers = _dbContext.Customers.ToList();
                //foreach (var customer in customers)
                //{
                //    //if (customer.RelatedEntityId != null && customer.RelatedEntityId > 0)
                //    //{
                //    //    core.EntityItemDelete((int) customer.RelatedEntityId);
                //    //}

                //    var companyName = customer.CompanyName;
                //    if (string.IsNullOrEmpty(companyName))
                //    {
                //        companyName = $"Empty company {nextItemUid}";
                //    }
                //    var item = core.EntitySearchItemCreate(newEntityGroup.Id, $"{companyName}", "",
                //        nextItemUid.ToString());

                //    if (item != null)
                //    {
                //        var entityGroup = core.EntityGroupRead(customerUpdateModel.RelatedEntityId.ToString());
                //        if (entityGroup != null)
                //        {
                //            foreach (var entityItem in entityGroup.EntityGroupItemLst)
                //            {
                //                if (entityItem.MicrotingUUID == item.MicrotingUUID)
                //                {
                //                    customer.RelatedEntityId = entityItem.Id;
                //                }
                //            }
                //        }
                //    }
                //    nextItemUid++;
                //}

                //_dbContext.SaveChanges();
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