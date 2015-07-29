namespace EPMS.Web.ViewModels.ProductSection
{
    public class ProductSectionViewModel
    {
        public ProductSectionViewModel()
        {
            ProductSection = new Models.ProductSection();
        }
        public Models.ProductSection ProductSection{ get; set; }
    }
}