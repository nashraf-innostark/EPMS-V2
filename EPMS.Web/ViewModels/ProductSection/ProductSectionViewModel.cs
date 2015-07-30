using System.Collections.Generic;
using EPMS.Web.Models.Common;

namespace EPMS.Web.ViewModels.ProductSection
{
    public class ProductSectionViewModel
    {
        public ProductSectionViewModel()
        {
            ProductSection = new Models.ProductSection();
        }
        public Models.ProductSection ProductSection{ get; set; }
        public string JsTree { get; set; }
    }
}