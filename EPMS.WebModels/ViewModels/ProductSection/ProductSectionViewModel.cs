using System.Collections.Generic;

namespace EPMS.WebModels.ViewModels.ProductSection
{
    public class ProductSectionViewModel
    {
        public ProductSectionViewModel()
        {
            ProductSection = new WebsiteModels.ProductSection();
        }
        public WebsiteModels.ProductSection ProductSection{ get; set; }
        public IList<WebsiteModels.ProductSection> ProductSections{ get; set; }
        public IList<WebsiteModels.ProductSectionsListForTree> ProductSectionsChildList { get; set; }
    }
}