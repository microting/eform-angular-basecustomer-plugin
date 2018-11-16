﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Customers.Pn.Infrastructure.Data.Entities;
using Customers.Pn.Infrastructure.Models.Fields;
using Microsoft.EntityFrameworkCore;
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
            FieldModel fieldModel = new FieldModel();
            fieldModel.Name = Guid.NewGuid().ToString();
            fieldModel.Value = Guid.NewGuid().ToString();

            // Act
            fieldModel.Save(DbContext);

            Field dbField = DbContext.Fields.AsNoTracking().First();
            List<Field> fieldList = DbContext.Fields.AsNoTracking().ToList();
            // Assert

            Assert.NotNull(dbField);

            Assert.AreEqual(1, fieldList.Count());

            Assert.AreEqual(fieldModel.Name, dbField.Name);
            
        }
        [Test]
        public void FieldUpdateModel_Udate_DoesUpdate()
        {
            // Arrange
            Field field = new Field();
            field.Name = Guid.NewGuid().ToString();

            DbContext.Fields.Add(field);
            DbContext.SaveChanges();

            // Act
            FieldUpdateModel fieldUpdateModel = new FieldUpdateModel();
            fieldUpdateModel.Name = field.Name;

            List<FieldUpdateModel> list = new List<FieldUpdateModel>();
            list.Add(fieldUpdateModel);

            FieldsUpdateModel fieldsUpdate = new FieldsUpdateModel();
            fieldsUpdate.Fields = list;

            fieldsUpdate.Update(DbContext);

            Field dbField = DbContext.Fields.AsNoTracking().First();
            List<Field> fieldList = DbContext.Fields.AsNoTracking().ToList();
            // Assert
            Assert.NotNull(dbField);

            Assert.AreEqual(1, fieldList.Count());

            Assert.AreEqual(fieldUpdateModel.Name, dbField.Name);

        }
    }
}