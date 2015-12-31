using System.Collections.Generic;
using EPMS.Models.Common;
using EPMS.Models.DomainModels;

namespace EPMS.Models.ResponseModels
{
    public class ProductDetailResponse
    {
        public ProductDetailResponse()
        {
            Product = new Product();
            ProductSizes = new List<ProductSize>();
        }
        public bool ShowProductPrice { get; set; }
        public Product Product { get; set; }
        public ItemVariation ItemVariation { get; set; }
        public IList<ProductSection> ProductSections { get; set; }
        public IList<ProductSize> ProductSizes { get; set; }
    }
}
