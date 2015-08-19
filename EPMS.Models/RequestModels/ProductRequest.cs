using System.Collections.Generic;
using EPMS.Models.DomainModels;

namespace EPMS.Models.RequestModels
{
    public class ProductRequest
    {
        public Product Product { get; set; }
        public List<ProductImage> ProductImages { get; set; }
    }
}
