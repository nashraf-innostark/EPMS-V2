using System.Collections.Generic;
using EPMS.WebModels.WebsiteModels.Common;

namespace EPMS.WebModels.ViewModels.ProductSection
{
    public class ProductSectionCreateViewModel
    {
        public ProductSectionCreateViewModel()
        {
            ProductSection = new WebsiteModels.ProductSection();
        }
        public WebsiteModels.ProductSection ProductSection{ get; set; }
        public IList<WebsiteModels.ProductSection> ProductSections{ get; set; }
        public IList<WebsiteModels.ProductSectionsListForTree> ProductSectionsChildList { get; set; }
        public IList<JsTreeJson> JsTreeJsons { get; set; }
    }
}