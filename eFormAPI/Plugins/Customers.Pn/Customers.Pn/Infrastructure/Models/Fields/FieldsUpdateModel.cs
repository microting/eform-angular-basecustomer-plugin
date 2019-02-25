using System.Collections.Generic;

namespace Customers.Pn.Infrastructure.Models.Fields
{
    public class FieldsUpdateModel
    {
        public List<FieldUpdateModel> Fields { get; set; } = new List<FieldUpdateModel>();
    }
}