using System.Collections.Generic;
using EPMS.Models.DomainModels;

namespace EPMS.Models.MenuModels
{
    public class WebsiteMenuModel
    {
        public IEnumerable<ProductSection> ProductSections { get; set; }
        public IEnumerable<WebsiteService> WebsiteServices { get; set; }
    }
}
