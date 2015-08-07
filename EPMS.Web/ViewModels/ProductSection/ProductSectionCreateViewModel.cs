using System.Collections.Generic;
using EPMS.Web.Models.Common;

namespace EPMS.Web.ViewModels.ProductSection
{
    public class ProductSectionCreateViewModel
    {
        public ProductSectionCreateViewModel()
        {
            ProductSection = new Models.ProductSection();
        }
        public Models.ProductSection ProductSection{ get; set; }
        public IList<Models.ProductSection> ProductSections{ get; set; }
        public IList<Models.ProductSectionsListForTree> ProductSectionsChildList{ get; set; }
        public IList<JsTreeJson> JsTreeJsons { get; set; }
    }
}