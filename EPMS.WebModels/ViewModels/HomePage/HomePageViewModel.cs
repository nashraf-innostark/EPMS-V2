using System.Collections.Generic;
using System.Web.Mvc;

namespace EPMS.WebModels.ViewModels.HomePage
{
    public class HomePageViewModel
    {
        public HomePageViewModel()
        {
            Position = new List<SelectListItem>();
        }
        public IList<WebsiteModels.ImageSlider> ImageSlider { get; set; }
        public IList<WebsiteModels.Partner> Partners { get; set; }
        public IList<WebsiteModels.WebsiteDepartment> WebsiteDepartments { get; set; }
        public IEnumerable<SelectListItem> Position { get; set; }
    }
}