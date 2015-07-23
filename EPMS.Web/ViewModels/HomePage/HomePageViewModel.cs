using System.Collections.Generic;
using System.Web.Mvc;

namespace EPMS.Web.ViewModels.HomePage
{
    public class HomePageViewModel
    {
        public HomePageViewModel()
        {
            Position = new List<SelectListItem>();
        }
        public IList<Models.ImageSlider> ImageSlider { get; set; }
        public IList<Models.Partner> Partners { get; set; }
        public IEnumerable<SelectListItem> Position { get; set; }
    }
}