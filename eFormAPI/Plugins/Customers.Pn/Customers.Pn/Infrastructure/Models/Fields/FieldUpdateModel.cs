using Customers.Pn.Infrastructure.Enums;

namespace Customers.Pn.Infrastructure.Models.Fields
{
    public class FieldUpdateModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public FieldStatus FieldStatus { get; set; }
    }
}