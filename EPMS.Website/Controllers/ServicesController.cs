using System.Web.Mvc;
using EPMS.Interfaces.IServices;
using EPMS.WebModels.ModelMappers.Website.Services;
using EPMS.WebModels.ViewModels.Common;
using EPMS.WebModels.ViewModels.Website.Services;

namespace EPMS.Website.Controllers
{
    public class ServicesController : BaseController
    {
        private readonly IWebsiteServicesService websiteServicesService;

        #region Private
        #endregion

        #region Constructor

        public ServicesController(IWebsiteServicesService websiteServicesService)
        {
            this.websiteServicesService = websiteServicesService;
        }

        #endregion

        #region Public

        public ActionResult Detail(long id)
        {
            ViewBag.ShowSlider = false;
            ServicesCreateViewModel viewmodel = new ServicesCreateViewModel
            {
                WebsiteService = websiteServicesService.FindWebsiteServiceById(id).CreateFromServerToClient()
            };
            ViewBag.MessageVM = TempData["message"] as MessageViewModel;
            return View(viewmodel);
        }

        #endregion
    }
}