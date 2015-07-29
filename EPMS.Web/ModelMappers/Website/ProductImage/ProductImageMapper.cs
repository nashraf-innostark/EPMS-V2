using WebModels = EPMS.Web.Models;
using DomainModels = EPMS.Models.DomainModels;

namespace EPMS.Web.ModelMappers.Website.ProductImage
{
    public static class ProductImageMapper
    {
        public static WebModels.ProductImage CreateFromServerToClient(this DomainModels.ProductImage source)
        {
            return new WebModels.ProductImage
            {
                ImageId = source.ImageId,
                ImageOrder = source.ImageOrder,
                ProductImagePath = source.ProductImagePath,
                ProductId = source.ImageId,
                ShowImage = source.ShowImage,
                ShowImageOnList = source.ShowImage ? "Yes" : "No"
            };
        }

        public static DomainModels.ProductImage CreateFromClientToServer(this WebModels.ProductImage source)
        {
            return new DomainModels.ProductImage
            {
                ImageId = source.ImageId,
                ImageOrder = source.ImageOrder,
                ProductImagePath = source.ProductImagePath,
                ProductId = source.ImageId,
                ShowImage = source.ShowImage
            };
        }
    }
}