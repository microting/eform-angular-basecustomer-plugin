﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using Customers.Pn.Abstractions;
using Customers.Pn.Infrastructure.Extensions;
using Customers.Pn.Infrastructure.Helpers;
using Customers.Pn.Infrastructure.Models.Customer;
using Customers.Pn.Infrastructure.Models.Fields;
using Customers.Pn.Infrastructure.Models.Settings;
using eFormCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microting.eForm.Infrastructure.Constants;
using Microting.eForm.Infrastructure.Models;
using Microting.eFormApi.BasePn.Abstractions;
using Microting.eFormApi.BasePn.Infrastructure.Helpers.PluginDbOptions;
using Microting.eFormApi.BasePn.Infrastructure.Models.API;
using Microting.eFormBaseCustomerBase.Infrastructure.Data;
using Microting.eFormBaseCustomerBase.Infrastructure.Data.Entities;
using Microting.eFormBaseCustomerBase.Infrastructure.Models;
using Newtonsoft.Json.Linq;

namespace Customers.Pn.Services
{
    public class CustomersService : ICustomersService
    {
        private readonly IEFormCoreService _coreHelper;
        private readonly ILogger<CustomersService> _logger;
        private readonly CustomersPnDbAnySql _dbContext;
        private readonly ICustomersLocalizationService _customersLocalizationService;
        private readonly IPluginDbOptions<CustomersSettings> _options;

        public CustomersService(ILogger<CustomersService> logger,
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
					customersQuery.Where(x => x.WorkflowState != Constants.WorkflowStates.Removed)
                    .Skip(pnRequestModel.Offset)
                    .Take(pnRequestModel.PageSize);

                List<Customer> customers = customersQuery.ToList();
                customersPnModel.Total = _dbContext.Customers.Count(x => x.WorkflowState != Constants.WorkflowStates.Removed);
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
                    JToken rawJson = JToken.Parse(customersAsJson.ImportList);
                    JToken rawHeadersJson = JToken.Parse(customersAsJson.Headers);

                    JToken headers = rawHeadersJson;
                    IEnumerable<JToken> customerObjects = rawJson.Skip(1);
                    
                    Core core = _coreHelper.GetCore();

                    var customerSettings = _options.Value;

                    EntityGroup entityGroup = core.EntityGroupRead(customerSettings.RelatedEntityGroupId.ToString());

                    foreach (JToken customerObj in customerObjects)
                    {
                        bool customerNoExists = int.TryParse(headers[5]["headerValue"].ToString(), out int customerNoColumn);
                        bool companyNameExists = int.TryParse(headers[3]["headerValue"].ToString(), out int companyNameColumn);
                        bool contactPersonExists = int.TryParse(headers[4]["headerValue"].ToString(), out int contactPersonColumn);
                        if (customerNoExists
                            || companyNameExists
                            || contactPersonExists
                            )
                        {
                            Customer existingCustomer = FindCustomer(customerNoExists, customerNoColumn, companyNameExists, companyNameColumn, contactPersonExists, contactPersonColumn, headers, customerObj);
                            if (existingCustomer == null)
                            {
                                 CustomerFullModel customerModel = 
                                     CustomersHelper.ComposeValues(new CustomerFullModel(), headers, customerObj);

                                Customer newCustomer = new Customer
                                {
                                    CityName = customerModel.CityName,
                                    CompanyAddress = customerModel.CompanyAddress,
                                    CompanyAddress2 = customerModel.CompanyAddress2,
                                    CompanyName = customerModel.CompanyName,
                                    ContactPerson = customerModel.ContactPerson,
                                    CreatedBy = customerModel.CreatedBy,
                                    CustomerNo = customerModel.CustomerNo,
                                    Description = customerModel.Description,
                                    Email = customerModel.Email,
                                    Phone = customerModel.Phone,
                                    ZipCode = customerModel.ZipCode,
                                    RelatedEntityId = customerModel.RelatedEntityId,
                                    EanCode = customerModel.EanCode,
                                    VatNumber = customerModel.VatNumber,
                                    CountryCode = customerModel.CountryCode,
                                    CreatedDate = DateTime.Now
                                };

                                newCustomer.Create(_dbContext);

                                if (customerSettings?.RelatedEntityGroupId != null)
                                {
                                    if (entityGroup == null)
                                    {
                                        return new OperationResult(false, "Entity group not found");
                                    }
    
                                    int nextItemUid = entityGroup.EntityGroupItemLst.Count;
                                    string label = newCustomer.CompanyName;
                                    label += string.IsNullOrEmpty(newCustomer.CompanyAddress) ? "" : " - " + newCustomer.CompanyAddress;
                                    label += string.IsNullOrEmpty(newCustomer.ZipCode) ? "" : " - " + newCustomer.ZipCode;
                                    label += string.IsNullOrEmpty(newCustomer.CityName) ? "" : " - " + newCustomer.CityName;
                                    label += string.IsNullOrEmpty(newCustomer.Phone) ? "" : " - " + newCustomer.Phone;
                                    label += string.IsNullOrEmpty(newCustomer.ContactPerson) ? "" : " - " + newCustomer.ContactPerson;
                                    if (label.Count(f => f == '-') == 1 && label.Contains("..."))
                                    {
                                        label = label.Replace(" - ", "");
                                    }
    
                                    if (string.IsNullOrEmpty(label))
                                    {
                                        label = $"Empty company {nextItemUid}";
                                    }
                                    EntityItem item = core.EntitySearchItemCreate(entityGroup.Id, $"{label}", $"{customerModel.Description}",
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
                                                    newCustomer.RelatedEntityId = entityItem.Id;
                                                    newCustomer.Update(_dbContext);
                                                }
                                            }
                                        }
                                    }
                                }

                            }
                            else
                            {
                                CustomerFullModel customerModel = 
                                    CustomersHelper.ComposeValues(new CustomerFullModel(), headers, customerObj);

                                existingCustomer.Description = customerModel.Description;
                                existingCustomer.CustomerNo = customerModel.CustomerNo;
                                existingCustomer.Email = customerModel.Email;
                                existingCustomer.Phone = customerModel.Phone;
                                existingCustomer.ZipCode = customerModel.ZipCode;
                                existingCustomer.CityName = customerModel.CityName;
                                existingCustomer.CompanyName = customerModel.CompanyName;
                                existingCustomer.CompanyAddress = customerModel.CompanyAddress;
                                existingCustomer.CompanyAddress2 = customerModel.CompanyAddress2;
                                existingCustomer.ContactPerson = customerModel.ContactPerson;
                                existingCustomer.CountryCode = customerModel.CountryCode;
//                                existingCustomer.Update(_dbContext);
                                
                                if (existingCustomer.WorkflowState == Constants.WorkflowStates.Removed)
                                {
//                                    Customer customer = new Customer
//                                    {
//                                        Id = existingCustomer.Id,
//                                        Description = existingCustomer.Description,
//                                        Email = existingCustomer.Email,
//                                        Phone = existingCustomer.Phone,
//                                        ZipCode = existingCustomer.ZipCode,
//                                        CityName = existingCustomer.CityName,
//                                        CreatedBy = existingCustomer.CreatedBy,
//                                        CustomerNo = existingCustomer.CustomerNo,
//                                        CompanyName = existingCustomer.CompanyName,
//                                        CreatedDate = existingCustomer.CreatedDate,
//                                        ContactPerson = existingCustomer.ContactPerson,
//                                        CompanyAddress = existingCustomer.CompanyAddress,
//                                        CountryCode = existingCustomer.CountryCode
//                                    };
                                    existingCustomer.WorkflowState = Constants.WorkflowStates.Created;
                                                                        
                                    int nextItemUid = entityGroup.EntityGroupItemLst.Count;
                                    string label = existingCustomer.CompanyName;
                                    label += string.IsNullOrEmpty(existingCustomer.CompanyAddress) ? "" : " - " + existingCustomer.CompanyAddress;
                                    label += string.IsNullOrEmpty(existingCustomer.ZipCode) ? "" : " - " + existingCustomer.ZipCode;
                                    label += string.IsNullOrEmpty(existingCustomer.CityName) ? "" : " - " + existingCustomer.CityName;
                                    label += string.IsNullOrEmpty(existingCustomer.Phone) ? "" : " - " + existingCustomer.Phone;
                                    label += string.IsNullOrEmpty(existingCustomer.ContactPerson) ? "" : " - " + existingCustomer.ContactPerson;
                                    if (label.Count(f => f == '-') == 1 && label.Contains("..."))
                                    {
                                        label = label.Replace(" - ", "");
                                    }
    
                                    if (string.IsNullOrEmpty(label))
                                    {
                                        label = $"Empty company {nextItemUid}";
                                    }
                                    EntityItem item = core.EntitySearchItemCreate(entityGroup.Id, $"{label}", $"{existingCustomer.Description}",
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
                                                    existingCustomer.RelatedEntityId = entityItem.Id;
                                                }
                                            }
                                        }
                                    }
                                }
                                existingCustomer.Update(_dbContext);                                
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
                        RelatedEntityId = x.RelatedEntityId
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
                var customerSettings = _options.Value;
				if (customerSettings?.RelatedEntityGroupId != null)
				{
                    Customer newCustomer = new Customer()
                    {
                        CityName = customerPnCreateModel.CityName,
                        CompanyAddress = customerPnCreateModel.CompanyAddress,
                        CompanyName = customerPnCreateModel.CompanyName,
                        ContactPerson = customerPnCreateModel.ContactPerson,
                        CreatedBy = customerPnCreateModel.CreatedBy,
                        CustomerNo = customerPnCreateModel.CustomerNo,
                        Description = customerPnCreateModel.Description,
                        Email = customerPnCreateModel.Email,
                        Phone = customerPnCreateModel.Phone,
                        ZipCode = customerPnCreateModel.ZipCode,
                        RelatedEntityId = customerPnCreateModel.RelatedEntityId,
                        CreatedDate = DateTime.Now
                    };

                    newCustomer.Create(_dbContext);
					// create item
					Core core = _coreHelper.GetCore();
					EntityGroup entityGroup = core.EntityGroupRead(customerSettings.RelatedEntityGroupId.ToString());
					if (entityGroup == null)
					{
						return new OperationResult(false, "Entity group not found");
					}

					int nextItemUid = entityGroup.EntityGroupItemLst.Count;
					string label = newCustomer.CompanyName;
					label += string.IsNullOrEmpty(newCustomer.CompanyAddress) ? "" : " - " + newCustomer.CompanyAddress;
					label += string.IsNullOrEmpty(newCustomer.ZipCode) ? "" : " - " + newCustomer.ZipCode;
					label += string.IsNullOrEmpty(newCustomer.CityName) ? "" : " - " + newCustomer.CityName;
					label += string.IsNullOrEmpty(newCustomer.Phone) ? "" : " - " + newCustomer.Phone;
					label += string.IsNullOrEmpty(newCustomer.ContactPerson) ? "" : " - " + newCustomer.ContactPerson;
					if (label.Count(f => f == '-') == 1 && label.Contains("..."))
					{
						label = label.Replace(" - ", "");
					}
					if (string.IsNullOrEmpty(label))
					{
						label = $"Empty company {nextItemUid}";
					}

					EntityItem item = core.EntitySearchItemCreate(entityGroup.Id, $"{label}", $"{newCustomer.Description}",
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
                                    newCustomer.RelatedEntityId = entityItem.Id;
								}
							}
						}
					}

                    newCustomer.Update(_dbContext);
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
                Customer customerForUpdate = new Customer()
                {
                    CreatedBy = customerUpdateModel.CreatedBy,
                    CustomerNo = customerUpdateModel.CustomerNo,
                    ContactPerson = customerUpdateModel.ContactPerson,
                    CompanyName = customerUpdateModel.CompanyName,
                    CompanyAddress = customerUpdateModel.CompanyAddress,
                    ZipCode = customerUpdateModel.ZipCode,
                    CityName = customerUpdateModel.CityName,
                    Phone = customerUpdateModel.Phone,
                    Email = customerUpdateModel.Email,
                    Description = customerUpdateModel.Description,
                    RelatedEntityId = customerUpdateModel.RelatedEntityId,
                    Id = customerUpdateModel.Id,
                };
                customerForUpdate.Update(_dbContext);
                Core core = _coreHelper.GetCore();


                string label = customerUpdateModel.CompanyName;
                label += string.IsNullOrEmpty(customerUpdateModel.CompanyAddress)
                    ? ""
                    : " - " + customerUpdateModel.CompanyAddress;
                label += string.IsNullOrEmpty(customerUpdateModel.ZipCode) ? "" : " - " + customerUpdateModel.ZipCode;
                label += string.IsNullOrEmpty(customerUpdateModel.CityName) ? "" : " - " + customerUpdateModel.CityName;
                label += string.IsNullOrEmpty(customerUpdateModel.Phone) ? "" : " - " + customerUpdateModel.Phone;
                label += string.IsNullOrEmpty(customerUpdateModel.ContactPerson)
                    ? ""
                    : " - " + customerUpdateModel.ContactPerson;
                if (label.Count(f => f == '-') == 1 && label.Contains("..."))
                {
                    label = label.Replace(" - ", "");
                }

                string descrption = string.IsNullOrEmpty(customerUpdateModel.Description)
                    ? ""
                    : customerUpdateModel.Description.Replace("</p>", "<br>").Replace("<p>", "");
                core.EntityItemUpdate((int) customerUpdateModel.RelatedEntityId, label, descrption, "", 0);
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
                Customer customer = new Customer {Id = id};
                customer.Delete(_dbContext);

				if (_dbContext.Customers.SingleOrDefault(x => x.Id == id)?.RelatedEntityId != null)
                {
                    int? entityId = _dbContext.Customers.SingleOrDefault(x => x.Id == id).RelatedEntityId;
                    if (entityId == null)
                    {
                        return new OperationResult(true,
                            _customersLocalizationService.GetString("ErrorWhileDeletingCustomer"));
                    }
                    int relatedEntityId = (int)entityId;
                    Core core = _coreHelper.GetCore();
                    core.EntityItemDelete(relatedEntityId);
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

        
        private Customer FindCustomer(bool customerNoExists, int customerNoColumn, bool companyNameExists, int companyNameColumn, bool contactPersonExists, int contactPersonColumn, JToken headers, JToken customerObj)
        {
            Customer customer = null;

            if (customerNoExists)
            {
                string customerNo = customerObj[customerNoColumn].ToString();
                customer = _dbContext.Customers.SingleOrDefault(x => x.CustomerNo == customerNo);
                return customer;
            }
            if (companyNameExists)
            {
                string companyName = customerObj[companyNameColumn].ToString();
                customer = _dbContext.Customers.SingleOrDefault(x => x.CompanyName == companyName);
                return customer;
            }
            if (contactPersonExists)
            {
                string contactPerson = customerObj[contactPersonColumn].ToString();
                customer = _dbContext.Customers.SingleOrDefault(x => x.ContactPerson == contactPerson);
                return customer;
            }

            return null;
        }
        
    }
}