using Customers.Pn.Infrastructure.Enums;
using Microting.eFormApi.BasePn.Infrastructure.Database.Base;

namespace Customers.Pn.Infrastructure.Data.Entities
{
    public class CustomerField : BaseEntity
    {
        public int FieldId { get; set; }
        public Field Field { get; set; }

        public FieldStatus FieldStatus { get; set; }
    }
}