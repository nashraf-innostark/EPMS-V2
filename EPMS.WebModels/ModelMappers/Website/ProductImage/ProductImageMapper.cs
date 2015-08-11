namespace EPMS.WebModels.ModelMappers.Website.ProductImage
{
    public static class ProductImageMapper
    {
        public static WebsiteModels.ProductImage CreateFromServerToClient(this Models.DomainModels.ProductImage source)
        {
            return new WebsiteModels.ProductImage
            {
                ImageId = source.ImageId,
                ImageOrder = source.ImageOrder,
                ProductImagePath = source.ProductImagePath,
                ProductId = source.ProductId,
                ShowImage = source.ShowImage,
                ShowImageOnList = source.ShowImage ? "Yes" : "No"
            };
        }

        public static Models.DomainModels.ProductImage CreateFromClientToServer(this WebsiteModels.ProductImage source)
        {
            return new Models.DomainModels.ProductImage
            {
                ImageId = source.ImageId,
                ImageOrder = source.ImageOrder,
                ProductImagePath = source.ProductImagePath,
                ProductId = source.ProductId,
                ShowImage = source.ShowImage
            };
        }
    }
}