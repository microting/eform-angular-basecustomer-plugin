using System;

namespace Customers.Pn.Infrastructure.Models
{
    public class CustomerFullModel
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
        
    }
}