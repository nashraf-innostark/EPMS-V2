using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EPMS.Web.ViewModels.Product
{
    public class ProductViewModel
    {
        #region Constructor

        public ProductViewModel()
        {
            Product = new Models.Product();
            ProductImage = new Models.ProductImage();
            ProductSection = new Models.ProductSection();
            Product.ProductImages = new List<Models.ProductImage>();
        }

        #endregion
        public Models.Product Product { get; set; }
        public Models.ProductImage ProductImage { get; set; }
        public Models.ProductSection ProductSection { get; set; }
        public List<Models.ProductSection> ProductSections { get; set; }
    }
}