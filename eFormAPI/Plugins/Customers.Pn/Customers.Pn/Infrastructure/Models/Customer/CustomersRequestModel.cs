namespace Customers.Pn.Infrastructure.Models.Customer
{
    public class CustomersRequestModel
    {
        public string SortColumnName { get; set; }
        public int Offset { get; set; }
        public int PageSize { get; set; }
        public bool IsSortDsc { get; set; }
    }
}
