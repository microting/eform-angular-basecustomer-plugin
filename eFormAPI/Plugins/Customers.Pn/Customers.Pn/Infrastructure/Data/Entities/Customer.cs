using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Microting.eFormApi.BasePn.Abstractions;
using Microting.eFormApi.BasePn.Infrastructure.Database.Base;
// ReSharper disable InconsistentNaming

namespace Customers.Pn.Infrastructure.Data.Entities
{
    public class Customer : BaseEntity, IDataAccessObject<CustomersPnDbAnySql>
    {
        public DateTime? Created_at { get; set; }

        public DateTime? Updated_at { get; set; }

        [StringLength(255)]
        public string Workflow_state { get; set; }

        public int Version { get; set; }

        public int Created_By_User_Id { get; set; }

        public int Updated_By_User_Id { get; set; }

        public DateTime CreatedDate { get; set; }

        [StringLength(250)]
        public string CreatedBy { get; set; }
        public string CustomerNo { get; set; }
        [StringLength(250)]
        public string CompanyName { get; set; }
        [StringLength(250)]
        public string CompanyAddress { get; set; }
        [StringLength(50)]
        public string ZipCode { get; set; }
        [StringLength(250)]
        public string CityName { get; set; }
        [StringLength(250)]
        public string Phone { get; set; }
        [StringLength(250)]
        public string Email { get; set; }
        [StringLength(250)]
        public string ContactPerson { get; set; }
        public string Description { get; set; }
        public int? RelatedEntityId { get; set; }

        public void Create(CustomersPnDbAnySql dbContext)
        {
            dbContext.Customers.Add(this);
            dbContext.SaveChanges();

            //_dbContext.CustomersVersion.add(MapCustomerVersions(_dbContext, customer));
            //_dbContext.SaveChanges();
        }

        public void Update(CustomersPnDbAnySql dbContext)
        {
            Customer customer = dbContext.Customers.FirstOrDefault(x => x.Id == Id);

            if (customer == null)
            {
                throw new NullReferenceException($"Could not find Customer with {Id}");
            }

            customer.CityName = CityName;
            customer.CompanyAddress = CompanyAddress;
            customer.CompanyName = CompanyName;
            customer.ContactPerson = ContactPerson;
            customer.CreatedBy = CreatedBy;
            customer.CustomerNo = CustomerNo;
            customer.Description = Description;
            customer.Email = Email;
            customer.Phone = Phone;
            customer.ZipCode = ZipCode;
            customer.RelatedEntityId = RelatedEntityId;
            customer.Id = Id;
            customer.Workflow_state = Workflow_state;

            if (dbContext.ChangeTracker.HasChanges())
            {
                //customer.Updated_at = DateTime.Now;
                //customer.Version += 1;

                //_dbContext.CustomerVersions.Add(MapCustomerVersions(_dbContext, customer));
                dbContext.SaveChanges();
            }
        }

        public void Delete(CustomersPnDbAnySql dbContext)
        {
            Customer customer = dbContext.Customers.FirstOrDefault(x => x.Id == Id);

            if (customer == null)
            {
                throw new NullReferenceException($"Could not find Customer with {Id}");
            }

            customer.Workflow_state = eFormShared.Constants.WorkflowStates.Removed;
            customer.RelatedEntityId = null;

            if (dbContext.ChangeTracker.HasChanges())
            {
                //customer.Updated_at = DateTime.Now;
                //customer.Version += 1;
                //_dbContext.Customers.Remove(customer);
                //_dbContext.CustomerVersions.Add(MapCustomerVersions(_dbContext, customer));
                dbContext.SaveChanges();
            }
        }

        //private CustomerVersions MapCustomerVersions(CustomersPnDbAnySql _dbContext, Customer customer)
        //{
        //    CustomerVersions customerVer = new CustomerVersions();

        //    customerVer.CityName = customer.CityName;
        //    customerVer.CompanyAddress = customer.CompanyAddress;
        //    customerVer.CompanyName = customer.CompanyName;
        //    customerVer.ContactPerson = customer.ContactPerson;
        //    customerVer.CreatedBy = customer.CreatedBy;
        //    customerVer.CreatedDate = customer.CreatedDate;
        //    customerVer.CustomerNo = customer.CustomerNo;
        //    customerVer.Description = customer.Description;
        //    customerVer.Email = customer.Email;
        //    customerVer.Phone = customer.Phone;
        //    customerVer.ZipCode = customer.ZipCode;
        //    customerVer.CustomerId = customer.Id;

        //    return customerVer;
        //}
    }
}