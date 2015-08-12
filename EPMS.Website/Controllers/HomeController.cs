using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EPMS.Interfaces.IServices;
using EPMS.Models.ResponseModels;
using EPMS.WebModels.ModelMappers.Website.Department;
using EPMS.WebModels.ModelMappers.Website.Partner;
using EPMS.WebModels.ViewModels.Website.Home;
using EPMS.WebModels.WebsiteModels;

namespace EPMS.Website.Controllers
{
    public class HomeController : Controller
    {
        #region Private

        private readonly IWebsiteDepartmentService websiteDepartmentService;
        private readonly IWebsiteHomePageService _websiteHomePageService;

        #endregion

        #region Constructor
        #endregion

        #region Public
        #endregion

        public HomeController(IWebsiteDepartmentService websiteDepartmentService, IWebsiteHomePageService websiteHomePageService)
        {
            this.websiteDepartmentService = websiteDepartmentService;
            _websiteHomePageService = websiteHomePageService;
        }

        public ActionResult Index()
        {

            WebsiteHomeResponse response;
            response = _websiteHomePageService.websiteHomeResponse();
            return View(new WebsiteHomeViewModel
            {
                WebsiteDepartments = response.WebsiteDepartments.Select(x=>x.CreateFromServerToClient()),
                Partners = response.Partners.Select(x=>x.CreateFromServerToClient())
            });
            
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}