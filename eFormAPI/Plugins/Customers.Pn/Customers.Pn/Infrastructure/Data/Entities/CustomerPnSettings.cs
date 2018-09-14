using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eFormApi.BasePn.Infrastructure.Data.Base;

namespace Customers.Pn.Infrastructure.Data.Entities
{
    public class CustomerPnSettings : BaseEntity
    {
        public int? RelatedEntityGroupId { get; set; }
    }
}
