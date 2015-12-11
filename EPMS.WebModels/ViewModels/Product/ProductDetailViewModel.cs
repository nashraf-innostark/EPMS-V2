using System.Collections.Generic;

namespace EPMS.WebModels.ViewModels.Product
{
    public class ProductDetailViewModel
    {
        public WebsiteModels.Product Product { get; set; }
        public IList<Models.Common.ProductSize> ProductSizes { get; set; }
        public IEnumerable<Models.DomainModels.ProductSection> ProductSections { get; set; }
    }
}