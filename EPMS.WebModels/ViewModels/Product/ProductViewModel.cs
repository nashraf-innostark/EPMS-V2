using System.Collections.Generic;

namespace EPMS.WebModels.ViewModels.Product
{
    public class ProductViewModel
    {
        #region Constructor

        public ProductViewModel()
        {
            Product = new WebsiteModels.Product();
            ProductImage = new WebsiteModels.ProductImage();
            ProductSection = new WebsiteModels.ProductSection();
            Product.ProductImages = new List<WebsiteModels.ProductImage>();
        }

        #endregion
        public WebsiteModels.Product Product { get; set; }
        public WebsiteModels.ProductImage ProductImage { get; set; }
        public WebsiteModels.ProductSection ProductSection { get; set; }
        public List<WebsiteModels.ProductSection> ProductSections { get; set; }
    }
}