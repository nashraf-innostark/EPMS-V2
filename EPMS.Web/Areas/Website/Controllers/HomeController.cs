using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Net;
using System.Web;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using EPMS.Interfaces.IServices;
using EPMS.Models.ResponseModels;
using EPMS.WebModels.ModelMappers;
using EPMS.WebModels.ModelMappers.Website.Department;
using EPMS.WebModels.ModelMappers.Website.HomePage;
using EPMS.WebModels.ModelMappers.Website.Partner;
using EPMS.WebModels.ViewModels.Common;
using EPMS.WebModels.ViewModels.HomePage;
using EPMS.Web.Controllers;
using EPMS.Web.EnumForDropDown;
using EPMS.WebModels.WebsiteModels;

namespace EPMS.Web.Areas.Website.Controllers
{
    public class HomeController : BaseController
    {
        #region Private

        private readonly IImageSliderService sliderService;
        private readonly IWebsiteHomePageService homePageService;

        #endregion

        #region Constructor
        public HomeController(IImageSliderService sliderService, IWebsiteHomePageService homePageService)
        {
            this.sliderService = sliderService;
            this.homePageService = homePageService;
        }

        #endregion

        #region Public

        // GET: Website/Home
        public ActionResult Index()
        {
            HomePageResponse response = sliderService.GetHomePageResponse();
            HomePageViewModel viewModel = new HomePageViewModel
            {
                ImageSlider = response.ImageSlider.Select(x => x.CreateFromServerToClient()).ToList(),
                Partners = response.Partners.Select(x => x.CreateFromServerToClient()).ToList(),
                WebsiteDepartments = response.WebsiteDepartments.Select(x => x.CreateFromServerToClient()).ToList()

            };
            IEnumerable<Position> actionTypes = Enum.GetValues(typeof(Position))
                                                       .Cast<Position>();
            viewModel.Position = from action in actionTypes
                                 select new SelectListItem
                                 {
                                     Text = action.ToString(),
                                     Value = ((int)action).ToString()
                                 };
            ViewBag.MessageVM = TempData["message"] as MessageViewModel;
            return View(viewModel);
        }

        #region Upload Logo

        public ActionResult UploadLogo()
        {
            HttpPostedFileBase image = Request.Files[0];
            var filename = "";
            try
            {
                //Save File to Folder
                if ((image != null))
                {
                    filename =
                        (DateTime.Now.ToString(CultureInfo.InvariantCulture).Replace(".", "") + image.FileName)
                            .Replace("/", "").Replace("-", "").Replace(":", "").Replace(" ", "").Replace("+", "");
                    var filePathOriginal = Server.MapPath(ConfigurationManager.AppSettings["WebsiteLogo"]);
                    string savedFileName = Path.Combine(filePathOriginal, filename);
                    image.SaveAs(savedFileName);
                    WebsiteHomePage logo = new WebsiteHomePage
                    {
                        WebsiteLogoPath = filename
                    };
                    var logoToAdd = logo.CreateFromClientToServer();
                    homePageService.SaveLogo(logoToAdd);
                }
            }
            catch (Exception exp)
            {
                return
                    Json(
                        new
                        {
                            response = "Failed to upload. Error: " + exp.Message,
                            status = (int)HttpStatusCode.BadRequest
                        }, JsonRequestBehavior.AllowGet);
            }
            return
                Json(
                    new
                    {
                        filename = filename,
                        size = image.ContentLength / 1024 + "KB",
                        response = "Successfully uploaded!",
                        status = (int)HttpStatusCode.OK
                    }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #endregion
    }
}