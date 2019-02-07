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

				customersQuery =
					customersQuery.Where(x => x.Workflow_state != eFormShared.Constants.WorkflowStates.Removed);

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
            try
            {
                {
                    JToken rawJson = JRaw.Parse(customersAsJson.ImportList);
                    JToken rawHeadersJson = JRaw.Parse(customersAsJson.Headers);

                    JToken headers = rawHeadersJson;
                    IEnumerable<JToken> customerObjects = rawJson.Skip(1);

                    foreach (JToken customerObj in customerObjects)
                    {
                        bool customerNoExists = int.TryParse(headers[4]["headerValue"].ToString(), out int customerNoColumn);
                        bool companyNameExists = int.TryParse(headers[2]["headerValue"].ToString(), out int companyNameColumn);
                        bool contactPersonExists = int.TryParse(headers[3]["headerValue"].ToString(), out int contactPersonColumn);
                        if (customerNoExists
                            || companyNameExists
                            || contactPersonExists
                            )
                        {
                            Customer existingCustomer = FindCustomer(customerNoExists, customerNoColumn, companyNameExists, companyNameColumn, contactPersonExists, contactPersonColumn, headers, customerObj);
                            if (existingCustomer == null)
                            {
                                CustomerFullModel customerModel = new CustomerFullModel();
                                //Customer customer = new Customer();
                                customerModel = AddValues(customerModel, headers, customerObj);
                                customerModel.Save(_dbContext);

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
                                string label = customerModel.CompanyName;
                                label += string.IsNullOrEmpty(customerModel.CompanyAddress) ? "" : " - " + customerModel.CompanyAddress;
                                label += string.IsNullOrEmpty(customerModel.ZipCode) ? "" : " - " + customerModel.ZipCode;
                                label += string.IsNullOrEmpty(customerModel.CityName) ? "" : " - " + customerModel.CityName;
                                label += string.IsNullOrEmpty(customerModel.Phone) ? "" : " - " + customerModel.Phone;
                                label += string.IsNullOrEmpty(customerModel.ContactPerson) ? "" : " - " + customerModel.ContactPerson;
                                if (label.Count(f => f == '-') == 1 && label.Contains("..."))
                                {
                                    label = label.Replace(" - ", "");
                                }

                                if (string.IsNullOrEmpty(label))
                                {
                                    label = $"Empty company {nextItemUid}";
                                }
                                eFormData.EntityItem item = core.EntitySearchItemCreate(entityGroup.Id, $"{label}", $"{customerModel.Description}",
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
                                                customerModel.RelatedEntityId = entityItem.Id;
                                                customerModel.Update(_dbContext);
                                            }
                                        }
                                    }
                                }
                                }

                            }
                        }
                    }
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
				CustomerSettings customerSettings = _dbContext.CustomerSettings.FirstOrDefault();
				if (customerSettings?.RelatedEntityGroupId != null)
				{
					customerPnCreateModel.Save(_dbContext);
					// create item
					Core core = _coreHelper.GetCore();
					EntityGroup entityGroup = core.EntityGroupRead(customerSettings.RelatedEntityGroupId.ToString());
					if (entityGroup == null)
					{
						return new OperationResult(false, "Entity group not found");
					}

					int nextItemUid = entityGroup.EntityGroupItemLst.Count;
					string label = customerPnCreateModel.CompanyName;
					label += string.IsNullOrEmpty(customerPnCreateModel.CompanyAddress) ? "" : " - " + customerPnCreateModel.CompanyAddress;
					label += string.IsNullOrEmpty(customerPnCreateModel.ZipCode) ? "" : " - " + customerPnCreateModel.ZipCode;
					label += string.IsNullOrEmpty(customerPnCreateModel.CityName) ? "" : " - " + customerPnCreateModel.CityName;
					label += string.IsNullOrEmpty(customerPnCreateModel.Phone) ? "" : " - " + customerPnCreateModel.Phone;
					label += string.IsNullOrEmpty(customerPnCreateModel.ContactPerson) ? "" : " - " + customerPnCreateModel.ContactPerson;
					if (label.Count(f => f == '-') == 1 && label.Contains("..."))
					{
						label = label.Replace(" - ", "");
					}
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

					customerPnCreateModel.Update(_dbContext);
					return new OperationResult(true,
					_customersLocalizationService.GetString("CustomerCreated"));
				}
				else
				{
					return new OperationResult(false,
						_customersLocalizationService.GetString("ErrorWhileCreatingCustomer"));
				}

                
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
                Debugger.Break();
                customerUpdateModel.Update(_dbContext);
                Core core = _coreHelper.GetCore();


                string label = customerUpdateModel.CompanyName;
                label += string.IsNullOrEmpty(customerUpdateModel.CompanyAddress) ? "" : " - " + customerUpdateModel.CompanyAddress;
                label += string.IsNullOrEmpty(customerUpdateModel.ZipCode) ? "" : " - " + customerUpdateModel.ZipCode;
                label += string.IsNullOrEmpty(customerUpdateModel.CityName) ? "" : " - " + customerUpdateModel.CityName;
                label += string.IsNullOrEmpty(customerUpdateModel.Phone) ? "" : " - " + customerUpdateModel.Phone;
                label += string.IsNullOrEmpty(customerUpdateModel.ContactPerson) ? "" : " - " + customerUpdateModel.ContactPerson;
                if (label.Count(f => f == '-') == 1 && label.Contains("..."))
                {
                    label = label.Replace(" - ", "");
                }
                string descrption = string.IsNullOrEmpty(customerUpdateModel.Description) ? "" : customerUpdateModel.Description.Replace("</p>", "<br>").Replace("<p>", "");
                core.EntityItemUpdate((int)customerUpdateModel.RelatedEntityId, label, descrption, "", 0);
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
                CustomerFullModel customer = new CustomerFullModel();
                customer.Id = id;
                customer.Delete(_dbContext);

				if (_dbContext.Customers.SingleOrDefault(x => x.Id == id).RelatedEntityId != null)
				{
					int RelatedEntityId = (int)_dbContext.Customers.SingleOrDefault(x => x.Id == id).RelatedEntityId;
					Core core = _coreHelper.GetCore();
					core.EntityItemDelete(RelatedEntityId);
				}

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
                    customerSettingsModel.Save(_dbContext);
                }
                else
                {
                    customerSettingsModel.Update(_dbContext);
                }

                Core core = _coreHelper.GetCore();
                EntityGroup newEntityGroup = core.EntityGroupRead(customerSettingsModel.RelatedEntityId.ToString());
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
        private Customer FindCustomer(bool customerNoExists, int customerNoColumn, bool companyNameExists, int companyNameColumn, bool contactPersonExists, int contactPersonColumn, JToken headers, JToken customerObj)
        {
            Customer customer = null;

            if (customerNoExists)
            {
                string customerNo = customerObj[customerNoColumn].ToString();
                customer = _dbContext.Customers.SingleOrDefault(x => x.CustomerNo == customerNo);
            }
            if (companyNameExists)
            {
                string companyName = customerObj[companyNameColumn].ToString();
                customer = _dbContext.Customers.SingleOrDefault(x => x.CompanyName == companyName);
            }
            if (contactPersonExists)
            {
                string contactPerson = customerObj[contactPersonColumn].ToString();
                customer = _dbContext.Customers.SingleOrDefault(x => x.ContactPerson == contactPerson);
            }

            return customer;
        }
        private CustomerFullModel AddValues(CustomerFullModel customer, JToken headers, JToken customerObj)
        {
            int locationId;

            if (int.TryParse(headers[0]["headerValue"].ToString(), out locationId))
            {
                customer.CityName = customerObj[locationId].ToString(); // Cityname
            }
            if (int.TryParse(headers[1]["headerValue"].ToString(), out locationId))
            {
                customer.CompanyAddress = customerObj[locationId].ToString(); //CompanyAddress
            }
            if (int.TryParse(headers[2]["headerValue"].ToString(), out locationId))
            {
                customer.CompanyName = customerObj[locationId].ToString(); //Companyname
            }
            if (int.TryParse(headers[3]["headerValue"].ToString(), out locationId))
            {
                customer.ContactPerson = customerObj[locationId].ToString(); //Contactperson
            }
            if (int.TryParse(headers[4]["headerValue"].ToString(), out locationId))
            {
                customer.CustomerNo = customerObj[locationId].ToString(); //CustomerNumber
            }

            customer.CreatedDate = DateTime.UtcNow; // Createddate

            if (int.TryParse(headers[5]["headerValue"].ToString(), out locationId))
            {
                customer.Description = customerObj[locationId].ToString(); //Description
            }
            if (int.TryParse(headers[6]["headerValue"].ToString(), out locationId))
            {
                customer.Email = customerObj[locationId].ToString(); //Email
            }
            if (int.TryParse(headers[7]["headerValue"].ToString(), out locationId))
            {
                customer.Phone = customerObj[locationId].ToString(); //Phonenumber
            }
            if (int.TryParse(headers[8]["headerValue"].ToString(), out locationId))
            {
                customer.ZipCode = customerObj[locationId].ToString(); //Zipcode
            }

            return customer;
        }
    }
}