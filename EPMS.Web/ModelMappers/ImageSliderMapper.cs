using EPMS.Models.DomainModels;

namespace EPMS.Web.ModelMappers
{
    public static class ImageSliderMapper
    {
        public static ImageSlider CreateFromClientToServer(this Models.ImageSlider source)
        {
            return new ImageSlider
            {
                SliderId = source.SliderId,
                TitleEn = source.TitleEn,
                TitleAr = source.TitleAr,
                SubTitleEn = source.SubTitleEn,
                SubTitleAr = source.SubTitleAr,
                ImageOrder = source.ImageOrder,
                ImagePath = source.ImagePath,
                Position = source.Position,
                Description = source.Description,
                Link = source.Link,
                RecCreatedBy = source.RecCreatedBy,
                RecCreatedDate = source.RecCreatedDate,
                RecUpdatedBy = source.RecUpdatedBy,
                RecUpdatedDate = source.RecUpdatedDate
            };
        }
        public static Models.ImageSlider CreateFromServerToClient(this ImageSlider source)
        {
            return new Models.ImageSlider
            {
                SliderId = source.SliderId,
                TitleEn = source.TitleEn,
                TitleAr = source.TitleAr,
                SubTitleEn = source.SubTitleEn,
                SubTitleAr = source.SubTitleAr,
                ImageOrder = source.ImageOrder,
                ImagePath = source.ImagePath,
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