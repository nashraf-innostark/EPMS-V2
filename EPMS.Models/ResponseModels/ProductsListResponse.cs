using System.Collections.Generic;
using EPMS.Models.DomainModels;

namespace EPMS.Models.ResponseModels
{
    public class ProductsListResponse
    {
        public IList<Product> Products { get; set; }
        public IList<Product> AllProducts { get; set; }
        public IEnumerable<ProductSection> ProductSections { get; set; }
        public int TotalCount { get; set; }
    }
}
