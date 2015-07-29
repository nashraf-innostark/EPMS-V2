namespace EPMS.Web.ViewModels.ProductImage
{
    public class ProductImageViewModel
    {
        #region Constructor

        public ProductImageViewModel()
        {
            ProductImage = new Models.ProductImage();
        }

        #endregion
        public Models.ProductImage ProductImage { get; set; }
    }
}