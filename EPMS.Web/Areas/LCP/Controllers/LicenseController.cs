using System.Linq;
using System.Web.Mvc;
using EPMS.Interfaces.IServices;
using EPMS.Web.ModelMappers;
using EPMS.Web.ViewModels.Common;
using EPMS.Web.ViewModels.License;

namespace EPMS.Web.Areas.LCP.Controllers
{
    [Authorize]
    public class LicenseController : Controller
    {
        #region Private
        private readonly ILicenseControlPanelService ControlPanelService;
        #endregion

        #region Constructor
        public LicenseController(ILicenseControlPanelService controlPanelService)
        {
            ControlPanelService = controlPanelService;
        }
        #endregion

        // GET: LCP/License
        public ActionResult Index()
        {
            LicenseIndexViewModel viewModel = new LicenseIndexViewModel
            {
                LicenseControlPanels = ControlPanelService.GetAll().Select(x => x.CreateFromServerToClient())
            };
            ViewBag.MessageVM = TempData["message"] as MessageViewModel;
            return View(viewModel);
        }
        // GET: LCP/Create
        public ActionResult Create()
        {
            return View(new LicenseCreateViewModel());
        }
        /// <summary>
        /// POST: LCP/Create
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Create(LicenseCreateViewModel viewModel)
        {
            if (Request.Form["Deactivate"] != null)
            {
                viewModel.LicenseCoP.Status = false;
                var licenseToUpdate = viewModel.LicenseCoP.CreateFromClientToServer();
                if (ControlPanelService.UpdateLicense(licenseToUpdate))
                {
                    TempData["message"] = new MessageViewModel
                    {
                        Message = "License has been Updated",
                        IsUpdated = true
                    };
                    return RedirectToAction("Index");
                }
            }
            if (Request.Form["Activate"] != null)
            {
                if (viewModel.LicenseCoP.LicenseControlPanelId == 0)
                {
                    viewModel.LicenseCoP.Status = true;
                    var licenseToAdd = viewModel.LicenseCoP.CreateFromClientToServer();
                    if (ControlPanelService.AddLicense(licenseToAdd))
                    {
                        TempData["message"] = new MessageViewModel
                        {
                            Message = "License has been Added",
                            IsSaved = true
                        };
                        return RedirectToAction("Index");
                    }
                }
                else
                {
                    viewModel.LicenseCoP.Status = true;
                    var licenseToUpdate = viewModel.LicenseCoP.CreateFromClientToServer();
                    if (ControlPanelService.UpdateLicense(licenseToUpdate))
                    {
                        TempData["message"] = new MessageViewModel
                        {
                            Message = "License has been Updated",
                            IsUpdated = true
                        };
                        return RedirectToAction("Index");
                    }
                }
            }
            return View(viewModel);
        }
    }
}