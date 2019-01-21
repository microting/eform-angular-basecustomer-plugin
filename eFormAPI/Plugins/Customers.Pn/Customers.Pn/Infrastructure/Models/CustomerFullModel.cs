using System;
using System.Linq;
using Customers.Pn.Infrastructure.Data;
using Customers.Pn.Infrastructure.Data.Entities;
namespace Customers.Pn.Infrastructure.Models
{
    public class CustomerFullModel : IModel
    {
        public int Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public string CustomerNo { get; set; }
        public string CompanyName { get; set; }
        public string CompanyAddress { get; set; }
        public string ZipCode { get; set; }
        public string CityName { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string ContactPerson { get; set; }
        public string Description { get; set; }
        public int? RelatedEntityId { get; set; }

		public void Read(CustomersPnDbAnySql _dbContext, int id)
		{
			Customer customer = _dbContext.Customers.FirstOrDefault(x => x.Id == id);

			Id = customer.Id;
			Description = customer.Description;
			Phone = customer.Phone;
			CityName = customer.CityName;
			CustomerNo = customer.CustomerNo;
			ZipCode = customer.ZipCode;
			Email = customer.Email;
			ContactPerson = customer.ContactPerson;
			CreatedBy = customer.CreatedBy;
			CompanyAddress = customer.CompanyAddress;
			CompanyName = customer.CompanyName;
			RelatedEntityId = customer.RelatedEntityId;
		}

		public void Save(CustomersPnDbAnySql _dbContext)
        {
            Customer customer = new Customer();
            customer.CityName = CityName;
            customer.CompanyAddress = CompanyAddress;
            customer.CompanyName = CompanyName;
            customer.ContactPerson = ContactPerson;
            customer.CreatedBy = CreatedBy;
			customer.Workflow_state = eFormShared.Constants.WorkflowStates.Created;
            //customer.Created_at = DateTime.Now;
            customer.CreatedDate = DateTime.Now;
            customer.CustomerNo = CustomerNo;
            customer.Description = Description;
            customer.Email = Email;
            customer.Phone = Phone;
            //customer.Updated_at = DateTime.Now;
            customer.ZipCode = ZipCode;
            customer.RelatedEntityId = RelatedEntityId;

            _dbContext.Customers.Add(customer);
            _dbContext.SaveChanges();
			Id = customer.Id;

            //_dbContext.CustomersVersion.add(MapCustomerVersions(_dbContext, customer));
            //_dbContext.SaveChanges();
        }

        public void Update(CustomersPnDbAnySql _dbContext)
        {
            Customer customer = _dbContext.Customers.FirstOrDefault(x => x.Id == Id);

            if (customer == null)
            {
                throw new NullReferenceException($"Culd not find Customer with {Id}");
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

            if (_dbContext.ChangeTracker.HasChanges())
            {
                //customer.Updated_at = DateTime.Now;
                //customer.Version += 1;

                //_dbContext.CustomerVersions.Add(MapCustomerVersions(_dbContext, customer));
                _dbContext.SaveChanges();
            }
        }

        public void Delete(CustomersPnDbAnySql _dbContext)
        {
            Customer customer = _dbContext.Customers.FirstOrDefault(x => x.Id == Id);

            if (customer == null)
            {
                throw new NullReferenceException($"Culd not find Customer with {Id}");
            }

            customer.Workflow_state = eFormShared.Constants.WorkflowStates.Removed;

            if (_dbContext.ChangeTracker.HasChanges())
            {
                //customer.Updated_at = DateTime.Now;
                //customer.Version += 1;
                //_dbContext.Customers.Remove(customer);
                //_dbContext.CustomerVersions.Add(MapCustomerVersions(_dbContext, customer));
                _dbContext.SaveChanges();
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