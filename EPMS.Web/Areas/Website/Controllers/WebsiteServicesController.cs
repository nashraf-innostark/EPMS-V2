using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using EPMS.Interfaces.IServices;
using EPMS.Web.Controllers;
using EPMS.Web.ModelMappers.Website.WebsiteServices;
using EPMS.Web.Models;
using EPMS.Web.Models.Common;
using EPMS.Web.ViewModels.Common;
using EPMS.Web.ViewModels.Website.WebsiteServices;
using EPMS.WebBase.Mvc;
using Microsoft.AspNet.Identity;

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

        // GET: Website/Service
        [SiteAuthorize(PermissionKey = "ServiceIndex")]
        public ActionResult Index()
        {
            return View();
        }
        [SiteAuthorize(PermissionKey = "ServiceCreate,ServiceView")]
        public ActionResult Create(long? id)
        {
            string direction = Resources.Shared.Common.TextDirection;
            ServicesCreateViewModel viewModel = new ServicesCreateViewModel
            {
                WebsiteService = new WebsiteService(),
                WebsiteServices = new List<WebsiteService>(),
                ServicesTree = new List<JsTreeJson>()
            };
            viewModel.ServicesTree.Add(new JsTreeJson{
                id = "parentNode",
                text = Resources.Website.WebsiteService.Service.Services,
                parent = "#"
            });
            long serviceId = id != null ? (long)id : 0;
            var servicesResponse = websiteServices.LoadWebsiteServices(serviceId);
            viewModel.WebsiteService =
                servicesResponse.WebsiteService != null ? servicesResponse.WebsiteService.CreateFromServerToClient() : new WebsiteService();
            viewModel.WebsiteServices =
                servicesResponse.WebsiteServices.Select(x => x.CreateFromServerToClient()).ToList();
            //viewModel.ServicesTree = servicesResponse.WebsiteServices.Select()
            foreach (EPMS.Models.DomainModels.WebsiteService websiteService in servicesResponse.WebsiteServices.ToList())
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
    }
}