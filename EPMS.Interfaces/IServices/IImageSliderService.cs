using System.Collections.Generic;
using EPMS.Models.DomainModels;
using EPMS.Models.ResponseModels;

namespace EPMS.Interfaces.IServices
{
    public interface IImageSliderService
    {
        HomePageResponse GetHomePageResponse();
        IEnumerable<ImageSlider> GetAll();
        ImageSlider FindImageSliderById(long id);
        bool AddImageSlider(ImageSlider imageSlider);
        bool UpdateImageSlider(ImageSlider imageSlider);
        void DeleteImageSlider(long id);
    }
}
