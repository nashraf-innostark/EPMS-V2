using System.Collections.Generic;
using EPMS.Models.DomainModels;

namespace EPMS.Models.ResponseModels
{
    public class ProductListViewResponse
    {
        public ProductListViewResponse()
        {
            Products = new List<Product>();
        }
        public IEnumerable<Product> Products { get; set; }
        public int TotalCount { get; set; }
        public int TotalRecords { get; set; }
        public int TotalDisplayRecords { get; set; }
    }
}
