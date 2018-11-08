using System;
using System.ComponentModel.DataAnnotations;
using Microting.eFormApi.BasePn.Infrastructure.Database.Base;

namespace Customers.Pn.Infrastructure.Data.Entities
{
    public class Customer : BaseEntity
    {
        //public DateTime? Created_at { get; set; }

        //public DateTime? Updated_at { get; set; }

        //[StringLength(255)]
        //public string Workflow_state { get; set; }

        //public int Version { get; set; }

        //public int Created_By_User_Id { get; set; }

        //public int Updated_By_User_Id { get; set; }

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
    }
}