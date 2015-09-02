using System.Linq;
using System.Web.Mvc;
using EPMS.Interfaces.IServices;
using EPMS.Models.MenuModels;

namespace EPMS.Website.Controllers
{
    public class WebsiteSliderController : Controller
    {
        private readonly IImageSliderService sliderService;

        public WebsiteSliderController(IImageSliderService sliderService)
        {
            this.sliderService = sliderService;
        }

        public ActionResult Index()
        {
            ImageSliderModel sliders = new ImageSliderModel {ImageSliders = sliderService.GetAll().ToList()};
            return View(sliders);
        }
    }
}