using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Customers.Pn.Abstractions;
using Customers.Pn.Infrastructure.Helpers;
using Customers.Pn.Infrastructure.Models.Customer;
using Customers.Pn.Infrastructure.Models.Settings;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microting.eForm.Infrastructure.Constants;
using Microting.eFormApi.BasePn.Abstractions;
using Microting.eFormApi.BasePn.Infrastructure.Helpers.PluginDbOptions;
using Microting.eFormApi.BasePn.Infrastructure.Models.API;
using Microting.eFormBaseCustomerBase.Infrastructure.Data;
using Microting.eFormBaseCustomerBase.Infrastructure.Data.Entities;
using Newtonsoft.Json.Linq;

namespace Customers.Pn.Services
{
    using Microting.eFormApi.BasePn.Infrastructure.Helpers;

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

        public async Task<OperationDataResult<CustomersModel>> Index (
        CustomersRequestModel pnRequestModel)
        {
            try
            {
    
                // CustomerModel customerModel = new CustomerModel();
                var customersQuery = _dbContext.Customers
                    .Where(x => x.WorkflowState != Constants.WorkflowStates.Removed)
                    .AsQueryable();

                customersQuery = QueryHelper.AddSortToQuery(customersQuery, pnRequestModel.SortColumnName,
                    pnRequestModel.IsSortDsc);

                if (!string.IsNullOrEmpty(pnRequestModel.Name))
                {
                    customersQuery = customersQuery.Where(x =>
                        x.CompanyName.ToLower().Contains(pnRequestModel.Name.ToLower()) ||
                        x.ContactPerson.ToLower().Contains(pnRequestModel.Name.ToLower()) ||
                        x.CompanyAddress.ToLower().Contains(pnRequestModel.Name.ToLower()) ||
                        x.CompanyAddress2.ToLower().Contains(pnRequestModel.Name.ToLower()) ||
                        x.CityName.ToLower().Contains(pnRequestModel.Name.ToLower()) ||
                        x.Phone.Contains(pnRequestModel.Name) ||
                        x.VatNumber.Contains(pnRequestModel.Name) ||
                        x.EanCode.Contains(pnRequestModel.Name) ||
                        x.Email.ToLower().Contains(pnRequestModel.Name.ToLower()));
                }

                customersQuery = customersQuery
                                .Skip(pnRequestModel.Offset)
                                .Take(pnRequestModel.PageSize);

                var customers = await customersQuery.Select(x => new CustomerModel
                {
                    Id = x.Id,
                    Description = x.Description,
                    Email = x.Email,
                    ContactPerson = x.ContactPerson,
                    CompanyName = x.CompanyName,
                    Phone = x.Phone,
                    CityName = x.CityName,
                    CompanyAddress = x.CompanyAddress,
                    CompanyAddress2 = x.CompanyAddress2,
                    CountryCode = x.CountryCode,
                    CreatedBy = x.CreatedBy,
                    CreatedDate = x.CreatedDate,
                    CustomerNo = x.CustomerNo,
                    EanCode = x.EanCode,
                    VatNumber = x.VatNumber,
                    ZipCode = x.ZipCode,
                    CrmId = x.CrmId,
                    CadastralNumber = x.CadastralNumber,
                    PropertyNumber = x.PropertyNumber,
                    ApartmentNumber = x.ApartmentNumber,
                    CompletionYear = x.CompletionYear,
                    FloorsWithLivingSpace = x.FloorsWithLivingSpace,
                    CreatedAt = x.CreatedAt,
                    UpdateAt = x.UpdatedAt,
                    RelatedEntityId = x.RelatedEntityId,
                    CreatedByUserId = x.CreatedByUserId,
                    UpdatedByUserId = x.UpdatedByUserId,
                    WorkflowState = x.WorkflowState,
                    Version = x.Version,
                    CadastralType = x.CadastralType,
                    FullName = $"{x.CompanyName} - {x.ContactPerson} - {x.CompanyAddress} - {x.CityName} - {x.Phone}"
    
                }).ToListAsync();

                var customersPnModel = new CustomersModel
                {
                    Total = await customersQuery.Select(x => x.Id).CountAsync(),
                    Customers = customers
                };
                return new OperationDataResult<CustomersModel>(true, customersPnModel);
    
            }
            catch (Exception e)
            {
                Trace.TraceError(e.Message);
                _logger.LogError(e.Message);
                return new OperationDataResult<CustomersModel>(false, 
                    _customersLocalizationService.GetString("ErrorObtainingCustomersInfo") + e.Message);
            }
        }

        [HttpPost]
        [Route("api/customers-pn")]
        public async Task<OperationResult> Create(CustomerFullModel customerPnCreateModel)
        {
            try
            {
                var customerSettings = _options.Value;
                if (customerSettings?.RelatedEntityGroupId != null)
                {
                    // Customer customerCopy = _dbContext.Customers.FirstOrDefault(x =>
                    //     x.CompanyName == customerPnCreateModel.CompanyName && x.WorkflowState != Constants.WorkflowStates.Removed);

                    var existingCustomer = MatchCustomer(customerPnCreateModel);
                    if (existingCustomer == null)
                    {
                        var newCustomer = new Customer()
                        {
                            CityName = customerPnCreateModel.CityName,
                            CompanyAddress = customerPnCreateModel.CompanyAddress,
                            CompanyAddress2 = customerPnCreateModel.CompanyAddress2,
                            CompanyName = customerPnCreateModel.CompanyName,
                            ContactPerson = customerPnCreateModel.ContactPerson,
                            CountryCode = customerPnCreateModel.CountryCode,
                            CreatedBy = customerPnCreateModel.CreatedBy,
                            CustomerNo = customerPnCreateModel.CustomerNo,
                            Description = customerPnCreateModel.Description,
                            EanCode = customerPnCreateModel.EanCode,
                            Email = customerPnCreateModel.Email,
                            Phone = customerPnCreateModel.Phone,
                            VatNumber = customerPnCreateModel.VatNumber,
                            ZipCode = customerPnCreateModel.ZipCode,
                            RelatedEntityId = customerPnCreateModel.RelatedEntityId,
                            CrmId = customerPnCreateModel.CrmId,
                            CreatedDate = DateTime.Now,
                            CadastralNumber = customerPnCreateModel.CadastralNumber,
                            PropertyNumber = customerPnCreateModel.PropertyNumber,
                            ApartmentNumber = customerPnCreateModel.ApartmentNumber,
                            CompletionYear = customerPnCreateModel.CompletionYear,
                            FloorsWithLivingSpace = customerPnCreateModel.FloorsWithLivingSpace
                        };

                        await newCustomer.Create(_dbContext);
                        // create item
                        var core = await _coreHelper.GetCore();
                        var entityGroup =
                           await core.EntityGroupRead(customerSettings.RelatedEntityGroupId.ToString());
                        if (entityGroup == null)
                        {
                            return new OperationResult(false, "Entity group not found");
                        }

                        var nextItemUid = entityGroup.EntityGroupItemLst.Count;
                        var label = newCustomer.CompanyName;
                        label += string.IsNullOrEmpty(newCustomer.CompanyAddress)
                            ? ""
                            : " - " + newCustomer.CompanyAddress;
                        label += string.IsNullOrEmpty(newCustomer.ZipCode) ? "" : " - " + newCustomer.ZipCode;
                        label += string.IsNullOrEmpty(newCustomer.CityName) ? "" : " - " + newCustomer.CityName;
                        label += string.IsNullOrEmpty(newCustomer.Phone) ? "" : " - " + newCustomer.Phone;
                        label += string.IsNullOrEmpty(newCustomer.ContactPerson)
                            ? ""
                            : " - " + newCustomer.ContactPerson;
                        if (label.Count(f => f == '-') == 1 && label.Contains("..."))
                        {
                            label = label.Replace(" - ", "");
                        }

                        if (string.IsNullOrEmpty(label))
                        {
                            label = $"Empty company {nextItemUid}";
                        }

                        var item = await core.EntitySearchItemCreate(entityGroup.Id, $"{label}",
                            $"{newCustomer.Description}",
                            nextItemUid.ToString());
                        if (item != null)
                        {
                            entityGroup = await core.EntityGroupRead(customerSettings.RelatedEntityGroupId.ToString());
                            if (entityGroup != null)
                            {
                                foreach (var entityItem in entityGroup.EntityGroupItemLst)
                                {
                                    if (entityItem.MicrotingUUID == item.MicrotingUUID)
                                    {
                                        newCustomer.RelatedEntityId = entityItem.Id;
                                    }
                                }
                            }
                        }
                        await newCustomer.Update(_dbContext);
                        return new OperationResult(true,
                            _customersLocalizationService.GetString("CustomerCreated"));
                    }
                    else
                    {
                        return new OperationResult(false, "Copy already exists");
                    }
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
        
        public async Task<OperationDataResult<CustomerFullModel>> Read(int id)
        {
            try
            {
                var customer = await _dbContext.Customers.Select(x => new CustomerFullModel()
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
                        CompanyAddress2 = x.CompanyAddress2,
                        CompanyName = x.CompanyName,
                        CountryCode = x.CountryCode,
                        EanCode = x.EanCode,
                        VatNumber = x.VatNumber,
                        RelatedEntityId = x.RelatedEntityId,
                        CrmId = x.CrmId,
                        CadastralNumber = x.CadastralNumber,
                        PropertyNumber = x.PropertyNumber,
                        ApartmentNumber = x.ApartmentNumber,
                        CompletionYear = x.CompletionYear,
                        FloorsWithLivingSpace = x.FloorsWithLivingSpace
                    })
                    .FirstOrDefaultAsync(x => x.Id == id).ConfigureAwait(false);

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

        

        public async Task<OperationResult> Update(CustomerFullModel customerUpdateModel)
        {
            try
            {
                var customerForUpdate = new Customer()
                {
                    CreatedBy = customerUpdateModel.CreatedBy,
                    CustomerNo = customerUpdateModel.CustomerNo,
                    ContactPerson = customerUpdateModel.ContactPerson,
                    CompanyName = customerUpdateModel.CompanyName,
                    CompanyAddress = customerUpdateModel.CompanyAddress,
                    CompanyAddress2 = customerUpdateModel.CompanyAddress2,
                    CountryCode = customerUpdateModel.CountryCode,
                    EanCode = customerUpdateModel.EanCode,
                    ZipCode = customerUpdateModel.ZipCode,
                    CityName = customerUpdateModel.CityName,
                    Phone = customerUpdateModel.Phone,
                    VatNumber = customerUpdateModel.VatNumber,
                    Email = customerUpdateModel.Email,
                    Description = customerUpdateModel.Description,
                    RelatedEntityId = customerUpdateModel.RelatedEntityId,
                    Id = customerUpdateModel.Id,
                    CrmId = customerUpdateModel.CrmId,
                    CadastralNumber = customerUpdateModel.CadastralNumber,
                    PropertyNumber = customerUpdateModel.PropertyNumber,
                    ApartmentNumber = customerUpdateModel.ApartmentNumber,
                    CompletionYear = customerUpdateModel.CompletionYear,
                    FloorsWithLivingSpace = customerUpdateModel.FloorsWithLivingSpace
                };
                await customerForUpdate.Update(_dbContext);
                var core = await _coreHelper.GetCore();


                var label = customerUpdateModel.CompanyName;
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

                var descrption = string.IsNullOrEmpty(customerUpdateModel.Description)
                    ? ""
                    : customerUpdateModel.Description.Replace("</p>", "<br>").Replace("<p>", "");
                await core.EntityItemUpdate((int) customerUpdateModel.RelatedEntityId, label, descrption, "", 0);
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

        public async Task<OperationResult> Delete(int id)
        {
            try
            {
                var customer = new Customer {Id = id};
                await customer.Delete(_dbContext);

				if (_dbContext.Customers.SingleOrDefault(x => x.Id == id)?.RelatedEntityId != null)
                {
                    var entityId = _dbContext.Customers.SingleOrDefault(x => x.Id == id)?.RelatedEntityId;
                    if (entityId == null)
                    {
                        return new OperationResult(true,
                            _customersLocalizationService.GetString("ErrorWhileDeletingCustomer"));
                    }
                    var relatedEntityId = (int)entityId;
                    var core = await _coreHelper.GetCore();
                    await core.EntityItemDelete(relatedEntityId);
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

        public async Task<OperationResult> ImportCustomers(CustomerImportModel customersAsJson)
        {
            try
            {
                {
                    var rawJson = JToken.Parse(customersAsJson.ImportList);
                    var rawHeadersJson = JToken.Parse(customersAsJson.Headers);

                    var headers = rawHeadersJson;
                    var customerObjects = rawJson.Skip(1);
                    
                    var core = await _coreHelper.GetCore();

                    var customerSettings = _options.Value;

                    var entityGroup = await core.EntityGroupRead(customerSettings.RelatedEntityGroupId.ToString());

                    foreach (var customerObj in customerObjects)
                    {
                        var customerNoExists = int.TryParse(headers[5]["headerValue"].ToString(), out var customerNoColumn);
                        var companyNameExists = int.TryParse(headers[3]["headerValue"].ToString(), out var companyNameColumn);
                        var contactPersonExists = int.TryParse(headers[4]["headerValue"].ToString(), out var contactPersonColumn);
                        if (customerNoExists
                            || companyNameExists
                            || contactPersonExists
                            )
                        {
                            var existingCustomer = FindCustomer(customerNoExists, customerNoColumn,
                                companyNameExists, companyNameColumn, contactPersonExists,
                                contactPersonColumn, headers, customerObj);
                            if (existingCustomer == null)
                            {
                                 var customerModel = 
                                     CustomersHelper.ComposeValues(new CustomerFullModel(), headers, customerObj);

                                var newCustomer = new Customer
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
                                    CreatedDate = DateTime.Now,
                                    CrmId = customerModel.CrmId,
                                    CadastralNumber = customerModel.CadastralNumber,
                                    PropertyNumber = customerModel.PropertyNumber,
                                    ApartmentNumber = customerModel.ApartmentNumber,
                                    CompletionYear = customerModel.CompletionYear,
                                    FloorsWithLivingSpace = customerModel.FloorsWithLivingSpace
                                };

                                await newCustomer.Create(_dbContext);

                                if (customerSettings?.RelatedEntityGroupId != null)
                                {
                                    if (entityGroup == null)
                                    {
                                        return new OperationResult(false, "Entity group not found");
                                    }
    
                                    var nextItemUid = entityGroup.EntityGroupItemLst.Count;
                                    var label = newCustomer.CompanyName;
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
                                    var item = await core.EntitySearchItemCreate(entityGroup.Id, $"{label}", $"{customerModel.Description}",
                                        nextItemUid.ToString());
                                    if (item != null)
                                    {
                                        entityGroup = await core.EntityGroupRead(customerSettings.RelatedEntityGroupId.ToString());
                                        if (entityGroup != null)
                                        {
                                            foreach (var entityItem in entityGroup.EntityGroupItemLst)
                                            {
                                                if (entityItem.MicrotingUUID == item.MicrotingUUID)
                                                {
                                                    newCustomer.RelatedEntityId = entityItem.Id;
                                                    await newCustomer.Update(_dbContext);
                                                }
                                            }
                                        }
                                    }
                                }

                            }
                            else
                            {
                                var customerModel = 
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
                                existingCustomer.CrmId = customerModel.CrmId;
                                existingCustomer.CadastralNumber = customerModel.CadastralNumber;
                                existingCustomer.PropertyNumber = customerModel.PropertyNumber;
                                existingCustomer.ApartmentNumber = customerModel.ApartmentNumber;
                                existingCustomer.CompletionYear = customerModel.CompletionYear;
                                existingCustomer.FloorsWithLivingSpace = customerModel.FloorsWithLivingSpace;
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
                                                                        
                                    var nextItemUid = entityGroup.EntityGroupItemLst.Count;
                                    var label = existingCustomer.CompanyName;
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
                                    var item = await core.EntitySearchItemCreate(entityGroup.Id, $"{label}", $"{existingCustomer.Description}",
                                        nextItemUid.ToString());
                                    if (item != null)
                                    {
                                        entityGroup = await core.EntityGroupRead(customerSettings.RelatedEntityGroupId.ToString());
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
                                await existingCustomer.Update(_dbContext);                                
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
        private Customer FindCustomer(bool customerNoExists, int customerNoColumn, bool companyNameExists, int companyNameColumn, bool contactPersonExists, int contactPersonColumn, JToken headers, JToken customerObj)
        {
            Customer customer;

            if (customerNoExists)
            {
                var customerNo = customerObj[customerNoColumn]?.ToString();
                customer = _dbContext.Customers.SingleOrDefault(x => x.CustomerNo == customerNo);
                return customer;
            }
            if (companyNameExists)
            {
                var companyName = customerObj[companyNameColumn]?.ToString();
                customer = _dbContext.Customers.SingleOrDefault(x => x.CompanyName == companyName);
                return customer;
            }
            if (contactPersonExists)
            {
                var contactPerson = customerObj[contactPersonColumn]?.ToString();
                customer = _dbContext.Customers.SingleOrDefault(x => x.ContactPerson == contactPerson);
                return customer;
            }

            return null;
        }

        private Customer MatchCustomer(CustomerFullModel customerModel)
        {
            var existingCustomer = _dbContext.Customers.FirstOrDefault(x =>
                x.Description == customerModel.Description && x.Email == customerModel.Email &&
                x.Phone == customerModel.Phone && x.CityName == customerModel.CityName &&
                x.CompanyAddress == customerModel.CompanyAddress &&
                x.CompanyAddress2 == customerModel.CompanyAddress2 && x.CompanyName == customerModel.CompanyName &&
                x.ContactPerson == customerModel.ContactPerson && x.CountryCode == customerModel.CountryCode &&
                x.CreatedBy == customerModel.CreatedBy && x.CustomerNo == customerModel.CustomerNo &&
                x.EanCode == customerModel.EanCode && x.CrmId == customerModel.CrmId &&
                x.VatNumber == customerModel.VatNumber && x.ZipCode == customerModel.ZipCode && x.CadastralNumber == customerModel.CadastralNumber &&
                x.PropertyNumber == customerModel.PropertyNumber && x.ApartmentNumber == customerModel.ApartmentNumber &&
                x.CompletionYear == customerModel.CompletionYear && x.FloorsWithLivingSpace == customerModel.FloorsWithLivingSpace);
            

            return existingCustomer;
            
        }
    }
}