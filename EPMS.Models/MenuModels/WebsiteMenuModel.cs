using System.Collections.Generic;
using EPMS.Models.DomainModels;

namespace EPMS.Models.MenuModels
{
    public class WebsiteMenuModel
    {
        public IEnumerable<ProductSection> ProductSections { get; set; }
    }
}
