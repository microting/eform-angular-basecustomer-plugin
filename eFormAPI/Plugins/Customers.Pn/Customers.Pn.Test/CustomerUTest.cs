using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microting.eFormBaseCustomerBase.Infrastructure.Data.Entities;
using NUnit.Framework;

namespace Customers.Pn.Test
{
    [TestFixture]
    public class CustomerUTest : DbTestFixture
    {
        [Test]
        public void CustomerFullModel_Save_DoesSave()
        {
            // Arrange
            Customer newCustomer = new Customer
            {
                CityName = Guid.NewGuid().ToString(),
                CompanyAddress = Guid.NewGuid().ToString(),
                CompanyName = Guid.NewGuid().ToString(),
                ContactPerson = Guid.NewGuid().ToString(),
                CreatedBy = Guid.NewGuid().ToString(),
                CustomerNo = Guid.NewGuid().ToString(),
                Description = Guid.NewGuid().ToString(),
                Email = Guid.NewGuid().ToString(),
                Phone = Guid.NewGuid().ToString(),
                ZipCode = Guid.NewGuid().ToString()
            };

            // Act
            newCustomer.Create(DbContext);

            Customer customer = DbContext.Customers.AsNoTracking().First();
            List<Customer> customerList = DbContext.Customers.AsNoTracking().ToList();
            // Assert
            Assert.NotNull(customer);

            Assert.AreEqual(1, customerList.Count());

            Assert.AreEqual(newCustomer.CityName , customer.CityName);
            Assert.AreEqual(newCustomer.CompanyAddress , customer.CompanyAddress);
            Assert.AreEqual(newCustomer.CompanyName , customer.CompanyName);
            Assert.AreEqual(newCustomer.CustomerNo , customer.CustomerNo);
            Assert.AreEqual(newCustomer.Description , customer.Description);
            Assert.AreEqual(newCustomer.Email , customer.Email);
            Assert.AreEqual(newCustomer.Phone , customer.Phone);
            Assert.AreEqual(newCustomer.ZipCode , customer.ZipCode);
        }
        //needs version.
        [Test]
        public void CustomerFullModel_Update_DoesUpdate()
        {
            // Arrange
            Customer newCustomer = new Customer
            {
                CityName = Guid.NewGuid().ToString(),
                CompanyAddress = Guid.NewGuid().ToString(),
                CompanyName = Guid.NewGuid().ToString(),
                ContactPerson = Guid.NewGuid().ToString(),
                CreatedBy = Guid.NewGuid().ToString(),
                CustomerNo = Guid.NewGuid().ToString(),
                Description = Guid.NewGuid().ToString(),
                Email = Guid.NewGuid().ToString(),
                Phone = Guid.NewGuid().ToString(),
                ZipCode = Guid.NewGuid().ToString()
            };

            newCustomer.Create(DbContext);

            // Act
            newCustomer.CityName = Guid.NewGuid().ToString();
            newCustomer.CompanyAddress = Guid.NewGuid().ToString();
            newCustomer.CompanyName = Guid.NewGuid().ToString();
            newCustomer.ContactPerson = Guid.NewGuid().ToString();
            newCustomer.CreatedBy = Guid.NewGuid().ToString();
            newCustomer.CustomerNo = Guid.NewGuid().ToString();
            newCustomer.Description = Guid.NewGuid().ToString();
            newCustomer.Email = Guid.NewGuid().ToString();
            newCustomer.Phone = Guid.NewGuid().ToString();
            newCustomer.ZipCode = Guid.NewGuid().ToString();

            newCustomer.Update(DbContext);

            Customer dbCustomer = DbContext.Customers.AsNoTracking().First();
            List<Customer> customerList = DbContext.Customers.AsNoTracking().ToList();

            // Assert
            Assert.NotNull(dbCustomer);

            Assert.AreEqual(1, customerList.Count());

            Assert.AreEqual(newCustomer.CityName, dbCustomer.CityName);
            Assert.AreEqual(newCustomer.CompanyAddress, dbCustomer.CompanyAddress);
            Assert.AreEqual(newCustomer.CompanyName, dbCustomer.CompanyName);
            Assert.AreEqual(newCustomer.ContactPerson, dbCustomer.ContactPerson);
            Assert.AreEqual(newCustomer.CreatedBy, dbCustomer.CreatedBy);
            Assert.AreEqual(newCustomer.CustomerNo, dbCustomer.CustomerNo);
            Assert.AreEqual(newCustomer.Description, dbCustomer.Description);
            Assert.AreEqual(newCustomer.Email, dbCustomer.Email);
            Assert.AreEqual(newCustomer.Phone, dbCustomer.Phone);
            Assert.AreEqual(newCustomer.ZipCode, dbCustomer.ZipCode);
        }
        //needs versions.
        [Test]
        public void CustomerFullModel_Delete_DoesDelete()
        {
            // Arrange
            Customer customer = new Customer
            {
                CityName = Guid.NewGuid().ToString(),
                CompanyAddress = Guid.NewGuid().ToString(),
                CompanyName = Guid.NewGuid().ToString(),
                ContactPerson = Guid.NewGuid().ToString(),
                CreatedBy = Guid.NewGuid().ToString(),
                CustomerNo = Guid.NewGuid().ToString(),
                Description = Guid.NewGuid().ToString(),
                Email = Guid.NewGuid().ToString(),
                Phone = Guid.NewGuid().ToString(),
                ZipCode = Guid.NewGuid().ToString()
            };

            customer.Create(DbContext);

            // Act
            customer.Delete(DbContext);

            Customer dbCustomer = DbContext.Customers.AsNoTracking().First();
            List<Customer> customerList = DbContext.Customers.AsNoTracking().ToList();

            // Assert
            Assert.NotNull(dbCustomer);

            Assert.AreEqual(1, customerList.Count());

            Assert.AreEqual(customer.CityName, dbCustomer.CityName);
            Assert.AreEqual(customer.CompanyAddress, dbCustomer.CompanyAddress);
            Assert.AreEqual(customer.CompanyName, dbCustomer.CompanyName);
            Assert.AreEqual(customer.ContactPerson, dbCustomer.ContactPerson);
            Assert.AreEqual(customer.CreatedBy, dbCustomer.CreatedBy);
            Assert.AreEqual(customer.CustomerNo, dbCustomer.CustomerNo);
            Assert.AreEqual(customer.Description, dbCustomer.Description);
            Assert.AreEqual(customer.Email, dbCustomer.Email);
            Assert.AreEqual(customer.Phone, dbCustomer.Phone);
            Assert.AreEqual(customer.ZipCode, dbCustomer.ZipCode);
            Assert.AreEqual(customer.Workflow_state, eFormShared.Constants.WorkflowStates.Removed);
        }
    }
}
