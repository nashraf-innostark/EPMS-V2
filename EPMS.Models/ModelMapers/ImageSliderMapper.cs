using EPMS.Models.DomainModels;

namespace EPMS.Models.ModelMapers
{
    public static class ImageSliderMapper
    {
        public static ImageSlider UpdateDbDataFromClient(this ImageSlider destination, ImageSlider source)
        {
            destination.SliderId = source.SliderId;
            destination.TitleEn = source.TitleEn;
            destination.TitleAr = source.TitleAr;
            destination.SubTitleEn = source.SubTitleEn;
            destination.SubTitleAr = source.SubTitleAr;
            destination.ImageOrder = source.ImageOrder;
            destination.ImageName = source.ImageName;
            destination.Position = source.Position;
            destination.Description = source.Description;
            destination.Link = source.Link;
            destination.RecUpdatedBy = source.RecUpdatedBy;
            destination.RecUpdatedDate = source.RecUpdatedDate;
            return destination;
        }
    }
}
