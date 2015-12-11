using System.Configuration;
using System.Linq;
using System.Web.Mvc;
using EPMS.Interfaces.IServices;
using EPMS.Models.ResponseModels;
using EPMS.WebModels.ModelMappers.Website.Department;
using EPMS.WebModels.ModelMappers.Website.Partner;
using EPMS.WebModels.ModelMappers.Website.ShoppingCart;
using EPMS.WebModels.ViewModels.Common;
using EPMS.WebModels.ViewModels.Website.Department;
using EPMS.WebModels.ViewModels.Website.Home;
using EPMS.WebModels.WebsiteModels;
using Microsoft.AspNet.Identity;

namespace EPMS.Website.Controllers
{
    public class HomeController : BaseController
    {
        #region Private

        private readonly IWebsiteDepartmentService websiteDepartmentService;
        private readonly IWebsiteHomePageService websiteHomePageService;

        #endregion

        #region Constructor

        public HomeController(IWebsiteDepartmentService websiteDepartmentService, IWebsiteHomePageService websiteHomePageService, IShoppingCartService cartService)
        {
            this.websiteDepartmentService = websiteDepartmentService;
            this.websiteHomePageService = websiteHomePageService;
        }

        #endregion

        #region Public

        public ActionResult Index(string div, string width, string code, string userId)
        {
            WebsiteHomeResponse response = websiteHomePageService.WebsiteHomeResponse();
            WebsiteHomeViewModel viewModel = new WebsiteHomeViewModel
            {
                WebsiteDepartments = response.WebsiteDepartments.Select(x => x.CreateFromServerToClient()).OrderBy(x=>x.DepartmentOrder),
                Partners = response.Partners.Select(x => x.CreateFromServerToClient()).OrderBy(x=>x.ImageOrder),
                Div = div,
                Width = width,
                Code = code,
                UserId = userId
            };

            ViewBag.Controller = "Home";
            ViewBag.Action = "Index";
            ViewBag.MessageVM = TempData["message"] as MessageViewModel;
            return View(viewModel);
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