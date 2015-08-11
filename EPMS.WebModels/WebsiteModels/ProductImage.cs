namespace EPMS.WebModels.WebsiteModels
{
    public class ProductImage
    {
        public long ImageId { get; set; }
        public string ProductImagePath { get; set; }
        public int? ImageOrder { get; set; }
        public bool ShowImage { get; set; }
        public long ProductId { get; set; }
        public string ShowImageOnList { get; set; }
    }
}