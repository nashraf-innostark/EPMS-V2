using System.Linq;
using System.Web.Mvc;
using EPMS.Interfaces.IServices;
using EPMS.Models.ResponseModels;
using EPMS.WebModels.ModelMappers.Website.Department;
using EPMS.WebModels.ModelMappers.Website.Partner;
using EPMS.WebModels.ViewModels.Common;
using EPMS.WebModels.ViewModels.Website.Department;
using EPMS.WebModels.ViewModels.Website.Home;

namespace EPMS.Website.Controllers
{
    public class HomeController : Controller
    {
        #region Private

        private readonly IWebsiteDepartmentService websiteDepartmentService;
        private readonly IWebsiteHomePageService websiteHomePageService;

        #endregion

        #region Constructor

        public HomeController(IWebsiteDepartmentService websiteDepartmentService, IWebsiteHomePageService websiteHomePageService)
        {
            this.websiteDepartmentService = websiteDepartmentService;
            this.websiteHomePageService = websiteHomePageService;
        }

        #endregion

        #region Public

        public ActionResult Index()
        {
            WebsiteHomeResponse response = websiteHomePageService.websiteHomeResponse();
            ViewBag.ShowSlider = true;
            ViewBag.MessageVM = TempData["message"] as MessageViewModel;
            return View(new WebsiteHomeViewModel
            {
                WebsiteDepartments = response.WebsiteDepartments.Select(x=>x.CreateFromServerToClient()),
                Partners = response.Partners.Select(x=>x.CreateFromServerToClient())
            });
        }

        public ActionResult DepartmentDetail(long id)
        {
            WebsiteDepartmentResponse response = websiteDepartmentService.websiteDepartmentResponse(id);
            DepartmentDetailViewModel detailViewModel = new DepartmentDetailViewModel
            {
                WebsiteDepartment = response.websiteDepartment.CreateFromServerToClient(),
                ProductSections = response.ProductSections.ToList()
            };
            return View(detailViewModel);

        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        #endregion
    }
}