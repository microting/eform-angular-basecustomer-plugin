using Microting.eFormApi.BasePn.Infrastructure.Database.Base;

namespace Customers.Pn.Infrastructure.Data.Entities
{
    public class CustomerField : BaseEntity
    {
        public int FieldId { get; set; }
        public virtual Field Field { get; set; }

        public short? FieldStatus { get; set; }
    }
}