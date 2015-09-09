using System.Collections.Generic;
using EPMS.Models.RequestModels;

namespace EPMS.WebModels.ViewModels.Product
{
    public class ProductListViewModel
    {
        public IList<WebsiteModels.Product> Products { get; set; }
        public IList<WebsiteModels.Product> NewArrivals { get; set; }
        public IList<WebsiteModels.Product> BestSell { get; set; }
        public IList<WebsiteModels.Product> RandomProducts { get; set; }
        public IList<WebsiteModels.Product> FeaturedProducts { get; set; }
        public ProductSearchRequest SearchRequest { get; set; }
    }
}