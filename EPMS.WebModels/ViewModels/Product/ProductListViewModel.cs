using System.Collections.Generic;
using EPMS.Models.RequestModels;

namespace EPMS.WebModels.ViewModels.Product
{
    public class ProductListViewModel
    {
        public IList<WebsiteModels.Product> Products { get; set; }
        public ProductSearchRequest SearchRequest { get; set; }
    }
}