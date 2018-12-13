using Microting.eFormApi.BasePn.Infrastructure.Data.Base;

namespace Customers.Pn.Infrastructure.Data.Entities
{
    public class CustomerSettings : BaseEntity
    {
        public int? RelatedEntityGroupId { get; set; }
    }
}
