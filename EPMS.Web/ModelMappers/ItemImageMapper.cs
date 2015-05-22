using WebModels = EPMS.Web.Models;
using DomainModels = EPMS.Models.DomainModels;

namespace EPMS.Web.ModelMappers
{
    public static class ItemImageMapper
    {
        public static WebModels.ItemImage CreateFromServerToClient(this DomainModels.ItemImage source)
        {
            return new WebModels.ItemImage
            {
                ImageId = source.ImageId,
                ItemImagePath = source.ItemImagePath,
                ImageOrder = source.ImageOrder,
                ItemVariationId = source.ItemVariationId,
                ShowImage = source.ShowImage
            };
        }
    }
}