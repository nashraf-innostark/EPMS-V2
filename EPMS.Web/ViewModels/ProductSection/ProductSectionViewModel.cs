using System.Collections.Generic;

namespace EPMS.Web.ViewModels.ProductSection
{
    public class ProductSectionViewModel
    {
        public ProductSectionViewModel()
        {
            ProductSection = new Models.ProductSection();
        }
        public Models.ProductSection ProductSection{ get; set; }
        public IList<Models.ProductSection> ProductSections{ get; set; }
        public IList<Models.ProductSectionsListForTree> ProductSectionsChildList{ get; set; }
    }
}