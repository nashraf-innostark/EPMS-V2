using System.Collections.Generic;
using System.Web.Mvc;

namespace EPMS.WebModels.ViewModels.Slider
{
    public class SliderViewModel
    {
        public SliderViewModel()
        {
            Position = new List<SelectListItem>();
        }
        public WebsiteModels.ImageSlider ImageSlider { get; set; }
        public IEnumerable<SelectListItem> Position { get; set; }
    }
}