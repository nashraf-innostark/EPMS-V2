using System.Collections.Generic;
using EPMS.Models.DomainModels;

namespace EPMS.Models.ResponseModels
{
    public class ProductResponse
    {
        public Product Product { get; set; }
        public IList<Product> Products { get; set; }
        public IList<ProductSection> ProductSections { get; set; }
        public int TotalCount { get; set; }
        public bool Status { get; set; }
    }
}
