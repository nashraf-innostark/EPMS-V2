using System.Collections.Generic;
using EPMS.Models.DomainModels;

namespace EPMS.Models.ResponseModels
{
    public class ProductDetailResponse
    {
        public Product Product { get; set; }
        public ItemVariation ItemVariation { get; set; }
        public IList<ProductSection> ProductSections { get; set; }
    }
}
