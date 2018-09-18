using Microting.eFormApi.BasePn.Database.Base;

namespace Customers.Pn.Infrastructure.Data.Entities
{
    public class CustomerSettings : BaseEntity
    {
        public int? RelatedEntityGroupId { get; set; }
    }
}
