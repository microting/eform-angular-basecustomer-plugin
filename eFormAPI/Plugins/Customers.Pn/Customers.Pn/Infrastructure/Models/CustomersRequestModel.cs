namespace Customers.Pn.Infrastructure.Models
{
    public class CustomersRequestModel
    {
        public string Name { get; set; }
        public string SortColumnName { get; set; }
        public int Offset { get; set; }
        public int PageSize { get; set; }
        public bool IsSortDsc { get; set; }
    }
}
