using EPMS.Models.DomainModels;

namespace EPMS.WebModels.ModelMappers
{
    public static class ImageSliderMapper
    {
        public static ImageSlider CreateFromClientToServer(this WebsiteModels.ImageSlider source)
        {
            string descp = source.Description;
            if (!string.IsNullOrEmpty(descp))
            {
                descp = descp.Replace("\n", "");
                descp = descp.Replace("\r", "");
            }
            return new ImageSlider
            {
                SliderId = source.SliderId,
                TitleEn = source.TitleEn,
                TitleAr = source.TitleAr,
                SubTitleEn = source.SubTitleEn,
                SubTitleAr = source.SubTitleAr,
                ImageOrder = source.ImageOrder,
                ImageName = source.ImageName,
                Position = source.Position,
                Description = descp,
                Link = source.Link,
                RecCreatedBy = source.RecCreatedBy,
                RecCreatedDate = source.RecCreatedDate,
                RecUpdatedBy = source.RecUpdatedBy,
                RecUpdatedDate = source.RecUpdatedDate
            };
        }
        public static WebsiteModels.ImageSlider CreateFromServerToClient(this ImageSlider source)
        {
            return new WebsiteModels.ImageSlider
            {
                SliderId = source.SliderId,
                TitleEn = source.TitleEn,
                TitleAr = source.TitleAr,
                SubTitleEn = source.SubTitleEn,
                SubTitleAr = source.SubTitleAr,
                ImageOrder = source.ImageOrder,
                ImageName = source.ImageName,
                Position = source.Position,
                Description = source.Description,
                Link = source.Link,
                RecCreatedBy = source.RecCreatedBy,
                RecCreatedDate = source.RecCreatedDate,
                RecUpdatedBy = source.RecUpdatedBy,
                RecUpdatedDate = source.RecUpdatedDate
            };
        }
    }
}