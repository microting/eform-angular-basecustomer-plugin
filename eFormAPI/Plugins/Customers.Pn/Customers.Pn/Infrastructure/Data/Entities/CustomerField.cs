using Customers.Pn.Enums;
using eFormApi.BasePn.Infrastructure.Data.Base;

namespace Customers.Pn.Infrastructure.Data.Entities
{
    public class CustomerField : BaseEntity
    {
        public int FieldId { get; set; }
        public Field Field { get; set; }

        public FieldPnStatus FieldStatus { get; set; }
    }
}