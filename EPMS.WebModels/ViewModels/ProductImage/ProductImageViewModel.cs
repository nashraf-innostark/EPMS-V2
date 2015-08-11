namespace EPMS.WebModels.ViewModels.ProductImage
{
    public class ProductImageViewModel
    {
        #region Constructor

        public ProductImageViewModel()
        {
            ProductImage = new WebsiteModels.ProductImage();
        }

        #endregion
        public WebsiteModels.ProductImage ProductImage { get; set; }
    }
}