namespace Customers.Pn.Infrastructure.Models.Customer
{
    using Microting.eFormApi.BasePn.Infrastructure.Models.Common;

    public class CustomersRequestModel : PaginationModel
    {
        public string Name { get; set; }

        public string SortColumnName { get; set; }

        public bool IsSortDsc { get; set; }
    }
}
