using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Net;
using System.Web;
using System.Linq;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using EPMS.Interfaces.IServices;
using EPMS.Models.ResponseModels;
using EPMS.WebBase.Mvc;
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
    [SiteAuthorize(PermissionKey = "Website", IsModule = true)]
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
                WebsiteDepartments = response.WebsiteDepartments.Select(x => x.CreateFromServerToClient()).ToList(),
                ShowProductPrice = response.ShowProductPrice
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
                        WebsiteLogoPath = filename,
                        ShowProductPrice = false
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

        #region ShowHideProductPrice

        public JsonResult ShowHideProductPrice(bool showPrice)
        {
            bool status = homePageService.UpdateShowProductPrice(showPrice);
            if (status)
            {
                return
                Json(
                    new
                    {
                        response = "Successfully uploaded!",
                        status = (int)HttpStatusCode.OK
                    }, JsonRequestBehavior.AllowGet);
            }
            return
                    Json(
                        new
                        {
                            response = "Error in updating record.",
                            status = (int)HttpStatusCode.BadRequest
                        }, JsonRequestBehavior.AllowGet);
        }
        #endregion
        
        #endregion
    }
}