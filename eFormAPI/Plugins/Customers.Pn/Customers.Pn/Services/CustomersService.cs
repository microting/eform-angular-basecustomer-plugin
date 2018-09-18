using System;
using System.Diagnostics;
using System.Linq;
using Customers.Pn.Infrastructure.Data;
using Customers.Pn.Infrastructure.Data.Entities;
using Customers.Pn.Infrastructure.Enums;
using Customers.Pn.Infrastructure.Extensions;
using Customers.Pn.Infrastructure.Helpers;
using Customers.Pn.Infrastructure.Models.Customer;
using Customers.Pn.Infrastructure.Models.Fields;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microting.eFormApi.BasePn.Infrastructure.Extensions;
using Microting.eFormApi.BasePn.Infrastructure.Models.API;

namespace Customers.Pn.Services
{
    public class CustomersService : ICustomersService
    {
        private readonly ILogger<CustomersService> _logger;
        private readonly CustomersPnDbContext _dbContext;

        public CustomersService(ILogger<CustomersService> logger, 
            CustomersPnDbContext dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
        }

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
                            .CustomOrderByDescending(pnRequestModel.SortColumnName);
                    }
                    else
                    {
                        customersQuery = customersQuery
                            .CustomOrderBy(pnRequestModel.SortColumnName);
                    }
                }
                else
                {
                    customersQuery = _dbContext.Customers
                        .OrderBy(x => x.Id);
                }

                customersQuery = customersQuery
                    .Skip(pnRequestModel.Offset)
                    .Take(pnRequestModel.PageSize);

                var customers = customersQuery.ToList();
                customersPnModel.Total = _dbContext.Customers.Count();
                var fields = _dbContext.CustomerFields
                    .Include(x=>x.Field)
                    .Select(x => new FieldUpdateModel()
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
                            var fieldModel = new FieldModel
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
                _logger.LogError(e.Message);
                return new OperationDataResult<CustomersModel>(false,
                    CustomersPnLocaleHelper.GetString("ErrorObtainingCustomersInfo"));
            }
        }

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
                _logger.LogError(e.Message);
                return new OperationDataResult<CustomerFullModel>(false,
                    CustomersPnLocaleHelper.GetString("ErrorObtainingCustomersInfo"));
            }
        }
        
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
                return new OperationResult(true,
                    CustomersPnLocaleHelper.GetString("CustomerCreated"));
            }
            catch (Exception e)
            {
                Trace.TraceError(e.Message);
                _logger.LogError(e.Message);
                return new OperationResult(false,
                    CustomersPnLocaleHelper.GetString("ErrorWhileCreatingCustomer"));
            }
        }

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
                _logger.LogError(e.Message);
                return new OperationDataResult<CustomersModel>(false,
                    CustomersPnLocaleHelper.GetString("ErrorWhileUpdatingCustomerInfo"));
            }
        }

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

                _dbContext.Customers.Remove(customer);
                _dbContext.SaveChanges();
                return new OperationResult(true,
                    CustomersPnLocaleHelper.GetString("CustomerDeletedSuccessfully"));
            }
            catch (Exception e)
            {
                Trace.TraceError(e.Message);
                _logger.LogError(e.Message);
                return new OperationDataResult<CustomerFullModel>(false,
                    CustomersPnLocaleHelper.GetString("ErrorWhileDeletingCustomer"));
            }
        }
    }
}