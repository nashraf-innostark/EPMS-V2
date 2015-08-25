using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using EPMS.Interfaces.IServices;
using EPMS.Web.Controllers;
using EPMS.WebModels.ModelMappers.Website.Services;
using EPMS.WebModels.ViewModels.Common;
using EPMS.WebModels.WebsiteModels.Common;
using DomainModels = EPMS.Models.DomainModels;
using EPMS.WebBase.Mvc;
using Microsoft.AspNet.Identity;
using ServicesCreateViewModel = EPMS.WebModels.ViewModels.Website.Services.ServicesCreateViewModel;
using ServicesListViewModel = EPMS.WebModels.ViewModels.Website.Services.ServicesListViewModel;
using WebsiteService = EPMS.WebModels.WebsiteModels.WebsiteService;

namespace EPMS.Web.Areas.Website.Controllers
{
    public class WebsiteServicesController : BaseController
    {
        #region Private
        private readonly IWebsiteServicesService websiteServices;
        
        #endregion

        #region Constructor

        public WebsiteServicesController(IWebsiteServicesService websiteServices)
        {
            this.websiteServices = websiteServices;
        }

        #endregion

        #region Public

        #region Index
        // GET: Website/Service
        [SiteAuthorize(PermissionKey = "ServiceIndex")]
        public ActionResult Index()
        {
            string direction = WebModels.Resources.Shared.Common.TextDirection;
            // All website services
            IEnumerable<DomainModels.WebsiteService> websiteServiceses = websiteServices.GetAll().ToList();
            // website services for service table
            IList<DomainModels.WebsiteService> services = websiteServiceses.Any()
                ? websiteServiceses.Where(x => x.ParentServiceId == null).ToList() : new List<DomainModels.WebsiteService>();
            // website services for section table
            IList<DomainModels.WebsiteService> sections = websiteServiceses.Any()
                ? websiteServiceses.Where(x => x.ParentServiceId != null).ToList() : new List<DomainModels.WebsiteService>();
            ServicesListViewModel viewModel = new ServicesListViewModel
            {
                WebsiteServices = websiteServiceses.Any() ? websiteServiceses.Select(y => y.CreateFromServerToClient()).ToList() : new List<WebsiteService>(),
                Services = services.Any() ? services.Select(y => y.CreateFromServerToClient()).ToList() : new List<WebsiteService>(),
                Sections = sections.Any() ? sections.Select(y => y.CreateFromServerToClient()).ToList() : new List<WebsiteService>(),
                ServicesTree = new List<JsTreeJson>()
            };
            viewModel.ServicesTree.Add(new JsTreeJson
            {
                id = "parentNode",
                text = WebModels.Resources.Website.WebsiteService.Service.Services,
                parent = "#"
            });
            foreach (DomainModels.WebsiteService websiteService in websiteServiceses.ToList())
            {
                JsTreeJson node = websiteService.CreateForJsTree(direction);
                viewModel.ServicesTree.Add(node);
            }
            // Javascript Serializer
            var serializer = new JavaScriptSerializer();
            ViewBag.JsTree = serializer.Serialize(viewModel.ServicesTree);
            // use new version of JsTree
            ViewBag.IsIncludeNewJsTree = true;
            ViewBag.MessageVM = TempData["message"] as MessageViewModel;
            return View(viewModel);
        }
        #endregion

        #region Create
        [SiteAuthorize(PermissionKey = "ServiceCreate,ServiceView")]
        public ActionResult Create(long? id)
        {
            string direction = WebModels.Resources.Shared.Common.TextDirection;
            ServicesCreateViewModel viewModel = new ServicesCreateViewModel
            {
                WebsiteService = new WebsiteService(),
                WebsiteServices = new List<WebsiteService>(),
                ServicesTree = new List<JsTreeJson>()
            };
            viewModel.ServicesTree.Add(new JsTreeJson
            {
                id = "parentNode",
                text = WebModels.Resources.Website.WebsiteService.Service.Services,
                parent = "#"
            });
            long serviceId = id != null ? (long)id : 0;
            var servicesResponse = websiteServices.LoadWebsiteServices(serviceId);
            viewModel.WebsiteService =
                servicesResponse.WebsiteService != null ? servicesResponse.WebsiteService.CreateFromServerToClient() : new WebsiteService();
            viewModel.WebsiteServices =
                servicesResponse.WebsiteServices.Select(x => x.CreateFromServerToClient()).ToList();
            //viewModel.ServicesTree = servicesResponse.WebsiteServices.Select()
            foreach (DomainModels.WebsiteService websiteService in servicesResponse.WebsiteServices.ToList())
            {
                JsTreeJson node = websiteService.CreateForJsTree(direction);
                viewModel.ServicesTree.Add(node);
            }
            // Javascript Serializer
            var serializer = new JavaScriptSerializer();
            ViewBag.JsTree = serializer.Serialize(viewModel.ServicesTree);
            // use new version of JsTree
            ViewBag.IsIncludeNewJsTree = true;
            ViewBag.ServiceId = serviceId;
            return View(viewModel);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(ServicesCreateViewModel viewModel)
        {
            try
            {
                if (viewModel.WebsiteService.ServiceId > 0)
                {
                    // Update
                    viewModel.WebsiteService.RecLastUpdatedBy = User.Identity.GetUserId();
                    viewModel.WebsiteService.RecLastUpdatedDate = DateTime.Now;
                    var dataToUpdate = viewModel.WebsiteService.CreateFromClientToServer();
                    if (websiteServices.UpdateWebsiteService(dataToUpdate))
                    {
                        TempData["message"] = new MessageViewModel
                        {
                            Message = "Website Services has been updated successfully",
                            IsUpdated = true
                        };
                        return RedirectToAction("Index");
                    }
                }
                else
                {
                    // Add
                    viewModel.WebsiteService.RecCreatedBy = User.Identity.GetUserId();
                    viewModel.WebsiteService.RecCreatedDate = DateTime.Now;
                    viewModel.WebsiteService.RecLastUpdatedBy = User.Identity.GetUserId();
                    viewModel.WebsiteService.RecLastUpdatedDate = DateTime.Now;
                    var dataToUpdate = viewModel.WebsiteService.CreateFromClientToServer();
                    if (websiteServices.AddWebsiteService(dataToUpdate))
                    {
                        TempData["message"] = new MessageViewModel
                        {
                            Message = "Website Services has been added successfully",
                            IsUpdated = true
                        };
                        return RedirectToAction("Index");
                    }
                }
            }
            catch (Exception)
            {
                return View(viewModel);
            }
            return View(viewModel);
        }
        #endregion

        #region Upload Image

        public ActionResult UploadImage()
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
                    var filePathOriginal = Server.MapPath(ConfigurationManager.AppSettings["ServicesImage"]);
                    string savedFileName = Path.Combine(filePathOriginal, filename);
                    image.SaveAs(savedFileName);
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