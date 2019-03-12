using System.Collections.Generic;
using Customers.Pn.Infrastructure.Models.Fields;

namespace Customers.Pn.Infrastructure.Models.Customer
{
    public class CustomerModel
    {
        public int Id { get; set; }
        public List<FieldModel> Fields { get; set; } = new List<FieldModel>();
    }
}