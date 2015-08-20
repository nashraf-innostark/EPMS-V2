using System.Collections.Generic;
using EPMS.Models.DomainModels;

namespace EPMS.Models.ResponseModels
{
    public class ProductsListResponse
    {
        public IList<Product> Products { get; set; }
        public int TotalCount { get; set; }
    }
}
