using System;
using System.Collections.Generic;
using System.Linq;
using Customers.Pn.Infrastructure.Data.Entities;
using Customers.Pn.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;
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
            CustomerFullModel customerFullModel = new CustomerFullModel();
            customerFullModel.CityName = Guid.NewGuid().ToString();
            customerFullModel.CompanyAddress = Guid.NewGuid().ToString();
            customerFullModel.CompanyName = Guid.NewGuid().ToString();
            customerFullModel.ContactPerson = Guid.NewGuid().ToString();
            customerFullModel.CreatedBy = Guid.NewGuid().ToString();
            customerFullModel.CustomerNo = Guid.NewGuid().ToString();
            customerFullModel.Description = Guid.NewGuid().ToString();
            customerFullModel.Email = Guid.NewGuid().ToString();
            customerFullModel.Phone = Guid.NewGuid().ToString();
            customerFullModel.ZipCode = Guid.NewGuid().ToString();

            // Act
            customerFullModel.Save(DbContext);

            Customer customer = DbContext.Customers.AsNoTracking().First();
            List<Customer> customerList = DbContext.Customers.AsNoTracking().ToList();
            // Assert
            Assert.NotNull(customer);

            Assert.AreEqual(1, customerList.Count());

            Assert.AreEqual(customerFullModel.CityName , customer.CityName);
            Assert.AreEqual(customerFullModel.CompanyAddress , customer.CompanyAddress);
            Assert.AreEqual(customerFullModel.CompanyName , customer.CompanyName);
            Assert.AreEqual(customerFullModel.CustomerNo , customer.CustomerNo);
            Assert.AreEqual(customerFullModel.Description , customer.Description);
            Assert.AreEqual(customerFullModel.Email , customer.Email);
            Assert.AreEqual(customerFullModel.Phone , customer.Phone);
            Assert.AreEqual(customerFullModel.ZipCode , customer.ZipCode);
        }
        //needs version.
        [Test]
        public void CustomerFullModel_Update_DoesUpdate()
        {
            //// Arrange
            //Customer customer = new Customer();
            //customer.CityName = Guid.NewGuid().ToString();
            //customer.CompanyAddress = Guid.NewGuid().ToString();
            //customer.CompanyName = Guid.NewGuid().ToString();
            //customer.ContactPerson = Guid.NewGuid().ToString();
            //customer.CreatedBy = Guid.NewGuid().ToString();
            //customer.CustomerNo = Guid.NewGuid().ToString();
            //customer.Description = Guid.NewGuid().ToString();
            //customer.Email = Guid.NewGuid().ToString();
            //customer.Phone = Guid.NewGuid().ToString();
            //customer.ZipCode = Guid.NewGuid().ToString();

            //DbContext.Customers.Add(customer);
            //DbContext.SaveChanges();

            //// Act
            //CustomerFullModel customerFullModel = new CustomerFullModel();
            //customerFullModel.CityName = customer.CityName;
            //customerFullModel.CompanyAddress = customer.CompanyAddress;
            //customerFullModel.CompanyName = customer.CompanyName;
            //customerFullModel.ContactPerson = customer.ContactPerson;
            //customerFullModel.CreatedBy = customer.CreatedBy;
            //customerFullModel.CustomerNo = customer.CustomerNo;
            //customerFullModel.Description = customer.Description;
            //customerFullModel.Email = customer.Email;
            //customerFullModel.Phone = customer.Phone;
            //customerFullModel.ZipCode = customer.ZipCode;

            //customerFullModel.Update(DbContext);

            //Customer dbCustomer = DbContext.Customers.AsNoTracking().First();
            //List<Customer> customerList = DbContext.Customers.AsNoTracking().ToList();

            //// Assert
            //Assert.NotNull(dbCustomer);

            //Assert.AreEqual(1, customerList.Count());

            //Assert.AreEqual(customer.CityName, dbCustomer.CityName);
            //Assert.AreEqual(customer.CompanyAddress, dbCustomer.CompanyAddress);
            //Assert.AreEqual(customer.CompanyName, dbCustomer.CompanyName);
            //Assert.AreEqual(customer.ContactPerson, dbCustomer.ContactPerson);
            //Assert.AreEqual(customer.CreatedBy, dbCustomer.CreatedBy);
            //Assert.AreEqual(customer.CustomerNo, dbCustomer.CustomerNo);
            //Assert.AreEqual(customer.Description, dbCustomer.Description);
            //Assert.AreEqual(customer.Email, dbCustomer.Email);
            //Assert.AreEqual(customer.Phone, dbCustomer.Phone);
            //Assert.AreEqual(customer.ZipCode, dbCustomer.ZipCode);
        }
        //needs versions.
        [Test]
        public void CustomerFullModel_Delete_DoesDelete()
        {
            //// Arrange
            //Customer customer = new Customer();
            //customer.CityName = Guid.NewGuid().ToString();
            //customer.CompanyAddress = Guid.NewGuid().ToString();
            //customer.CompanyName = Guid.NewGuid().ToString();
            //customer.ContactPerson = Guid.NewGuid().ToString();
            //customer.CreatedBy = Guid.NewGuid().ToString();
            //customer.CustomerNo = Guid.NewGuid().ToString();
            //customer.Description = Guid.NewGuid().ToString();
            //customer.Email = Guid.NewGuid().ToString();
            //customer.Phone = Guid.NewGuid().ToString();
            //customer.ZipCode = Guid.NewGuid().ToString();

            //DbContext.Customers.Add(customer);
            //DbContext.SaveChanges();

            //// Act
            //CustomerFullModel customerFullModel = new CustomerFullModel();
            //customerFullModel.CityName = customer.CityName;
            //customerFullModel.CompanyAddress = customer.CompanyAddress;
            //customerFullModel.CompanyName = customer.CompanyName;
            //customerFullModel.ContactPerson = customer.ContactPerson;
            //customerFullModel.CreatedBy = customer.CreatedBy;
            //customerFullModel.CustomerNo = customer.CustomerNo;
            //customerFullModel.Description = customer.Description;
            //customerFullModel.Email = customer.Email;
            //customerFullModel.Phone = customer.Phone;
            //customerFullModel.ZipCode = customer.ZipCode;

            //customerFullModel.Update(DbContext);

            //Customer dbCustomer = DbContext.Customers.AsNoTracking().First();
            //List<Customer> customerList = DbContext.Customers.AsNoTracking().ToList();

            //// Assert
            //Assert.NotNull(dbCustomer);

            //Assert.AreEqual(1, customerList.Count());

            //Assert.AreEqual(customer.CityName, dbCustomer.CityName);
            //Assert.AreEqual(customer.CompanyAddress, dbCustomer.CompanyAddress);
            //Assert.AreEqual(customer.CompanyName, dbCustomer.CompanyName);
            //Assert.AreEqual(customer.ContactPerson, dbCustomer.ContactPerson);
            //Assert.AreEqual(customer.CreatedBy, dbCustomer.CreatedBy);
            //Assert.AreEqual(customer.CustomerNo, dbCustomer.CustomerNo);
            //Assert.AreEqual(customer.Description, dbCustomer.Description);
            //Assert.AreEqual(customer.Email, dbCustomer.Email);
            //Assert.AreEqual(customer.Phone, dbCustomer.Phone);
            //Assert.AreEqual(customer.ZipCode, dbCustomer.ZipCode);
            //Assert.AreEqual(customer.workflow_state, eFormShared.Constants.WorkflowStates.Removed);
        }
    }
}
