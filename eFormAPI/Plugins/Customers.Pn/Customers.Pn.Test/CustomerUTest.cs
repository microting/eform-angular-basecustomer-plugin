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
    }
}
