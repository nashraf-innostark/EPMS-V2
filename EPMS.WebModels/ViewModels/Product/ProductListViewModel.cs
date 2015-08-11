using System.Collections.Generic;

namespace EPMS.WebModels.ViewModels.Product
{
    public class ProductListViewModel
    {
        public IEnumerable<WebsiteModels.Product> Products { get; set; }
    }
}