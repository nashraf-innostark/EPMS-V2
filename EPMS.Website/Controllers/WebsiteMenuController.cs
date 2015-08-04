using System.Web.Mvc;
using EPMS.Interfaces.IServices;

namespace EPMS.Website.Controllers
{
    public class WebsiteMenuController : Controller
    {
        private readonly IWebsiteMenuService websiteMenuService;

        public WebsiteMenuController(IWebsiteMenuService websiteMenuService)
        {
            this.websiteMenuService = websiteMenuService;
        }

        // GET: Menu
        public ActionResult Index()
        {
            var menus = websiteMenuService.LoadWebsiteMenuItems();
            return View(menus);
        }
    }
}