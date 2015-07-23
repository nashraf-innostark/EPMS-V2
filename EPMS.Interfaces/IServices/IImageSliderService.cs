using EPMS.Models.DomainModels;

namespace EPMS.Interfaces.IServices
{
    public interface IImageSliderService
    {
        ImageSlider FindImageSliderById(long id);
        bool AddImageSlider(ImageSlider imageSlider);
        bool UpdateImageSlider(ImageSlider imageSlider);
        void DeleteImageSlider(ImageSlider imageSlider);
    }
}
