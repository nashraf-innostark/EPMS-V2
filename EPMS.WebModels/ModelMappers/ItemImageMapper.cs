﻿namespace EPMS.WebModels.ModelMappers
{
    public static class ItemImageMapper
    {
        public static WebsiteModels.ItemImage CreateFromServerToClient(this Models.DomainModels.ItemImage source)
        {
            return new WebsiteModels.ItemImage
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