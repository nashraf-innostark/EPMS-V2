using System.Collections.Generic;
using System.Web.Mvc;
using EPMS.Web.Models;

namespace EPMS.Web.ViewModels.Slider
{
    public class SliderViewModel
    {
        public SliderViewModel()
        {
            Position = new List<SelectListItem>();
        }
        public ImageSlider ImageSlider { get; set; }
        public IEnumerable<SelectListItem> Position { get; set; }
    }
}