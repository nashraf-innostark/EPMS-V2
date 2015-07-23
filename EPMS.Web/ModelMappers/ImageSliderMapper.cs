using EPMS.Models.DomainModels;

namespace EPMS.Web.ModelMappers
{
    public static class ImageSliderMapper
    {
        public static ImageSlider CreateFromClientToServer(this Models.ImageSlider source)
        {
            string descp = source.Description.Replace("\n", "");
            descp = descp.Replace("\r", "");
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