using System.Collections.Generic;
using EPMS.Models.DomainModels;

namespace EPMS.Models.ResponseModels
{
    public class ProductDetails
    {
        public IList<Product> Products { get; set; }
    }
}
