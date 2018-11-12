﻿using Customers.Pn.Infrastructure.Models.Customer;
using Microting.eFormApi.BasePn.Infrastructure.Models.API;

namespace Customers.Pn.Abstractions
{
    public interface ICustomersService
    {
        OperationResult CreateCustomer(CustomerFullModel customerPnCreateModel);
        OperationResult DeleteCustomer(int id);
        OperationDataResult<CustomersModel> GetCustomers(CustomersRequestModel pnRequestModel);
        OperationDataResult<CustomerFullModel> GetSingleCustomer(int id);
        OperationResult UpdateCustomer(CustomerFullModel customerUpdateModel);
        OperationDataResult<CustomerSettingsModel> GetSettings();
        OperationResult UpdateSettings(CustomerSettingsModel customerUpdateModel);
    }
}