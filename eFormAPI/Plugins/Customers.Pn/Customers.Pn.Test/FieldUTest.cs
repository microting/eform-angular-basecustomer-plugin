using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microting.eFormBaseCustomerBase.Infrastructure.Data.Entities;
using Microting.eFormBaseCustomerBase.Infrastructure.Models;
using Microting.eFormBaseCustomerBase.Infrastructure.Models.Fields;
using NUnit.Framework;

namespace Customers.Pn.Test
{
    [TestFixture]
    public class FieldUTest : DbTestFixture
    {
        [Test]
        public void FieldModel_Save_DoesSave()
        {
            // Arrange
            Field newField = new Field()
            {
                Name = Guid.NewGuid().ToString()
            };

            // Act
            newField.Create(DbContext);

            Field dbField = DbContext.Fields.AsNoTracking().First();
            List<Field> fieldList = DbContext.Fields.AsNoTracking().ToList();
            // Assert

            Assert.NotNull(dbField);

            Assert.AreEqual(1, fieldList.Count());

            Assert.AreEqual(newField.Name, dbField.Name);
            
        }
        [Test]
        public void FieldUpdateModel_Update_DoesUpdate()
        {
            // Arrange
            Field newField = new Field
            {
                Name = Guid.NewGuid().ToString()
            };
            

            newField.Create(DbContext);

            // Act
            FieldUpdateModel fieldUpdateModel = new FieldUpdateModel();
            fieldUpdateModel.Name = newField.Name;

            List<FieldUpdateModel> list = new List<FieldUpdateModel>();
            list.Add(fieldUpdateModel);

            FieldsUpdateModel fieldsUpdate = new FieldsUpdateModel();
            fieldsUpdate.Fields = list;
            // TODO: FIX
          //  fieldsUpdate.Update(DbContext);

            Field dbField = DbContext.Fields.AsNoTracking().First();
            List<Field> fieldList = DbContext.Fields.AsNoTracking().ToList();
            // Assert
            Assert.NotNull(dbField);

            Assert.AreEqual(1, fieldList.Count());

            Assert.AreEqual(fieldUpdateModel.Name, dbField.Name);

        }
    }
}
