using System;
using System.Linq;
using System.Web.Mvc;
using EPMS.Interfaces.IServices;
using EPMS.Models.DomainModels;
using EPMS.Models.RequestModels;
using EPMS.Models.ResponseModels;
using EPMS.WebModels.ViewModels.PhysicalCount;
using EPMS.Web.Controllers;
using EPMS.WebModels.ModelMappers;
using EPMS.WebModels.ViewModels.Common;
using EPMS.WebBase.Mvc;
using Microsoft.AspNet.Identity;

namespace EPMS.Web.Areas.Inventory.Controllers
{
    [Authorize]
    [SiteAuthorize(PermissionKey = "IS", IsModule = true)]
    public class PhysicalCountController : BaseController
    {
        #region Private

        private readonly IPhysicalCountService physicalCountService;

        #endregion

        #region Constructor

        public PhysicalCountController(IPhysicalCountService physicalCountService)
        {
            this.physicalCountService = physicalCountService;
        }

        #endregion

        // GET: Inventory/PhysicalCount
        [SiteAuthorize(PermissionKey = "PhysicalCountIndex")]
        public ActionResult Index()
        {
            PhysicalCountListViewModel viewModel = new PhysicalCountListViewModel
            {
                SearchRequest = new PhysicalCountSearchRequest()
            };
            ViewBag.MessageVM = TempData["message"] as MessageViewModel;
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Index(PhysicalCountSearchRequest searchRequest)
        {
            searchRequest.SearchString = Request["search"];
            PhysicalCountResponse physicalCounts = physicalCountService.GetAllPhysicalCountResponse(searchRequest);
            PhysicalCountListViewModel countListViewModel = new PhysicalCountListViewModel
            {
                aaData = physicalCounts.PhysicalCounts.Select(x => x.CreateListFromServerToClient()),
                iTotalRecords = Convert.ToInt32(physicalCounts.TotalRecords),
                iTotalDisplayRecords = Convert.ToInt32(physicalCounts.TotalDisplayRecords),
                sEcho = searchRequest.sEcho
            };
            return Json(countListViewModel, JsonRequestBehavior.AllowGet);
        }

        [SiteAuthorize(PermissionKey = "PhysicalCountCreate")]
        public ActionResult Create(long? id)
        {
            PhysicalCountViewModel physicalCountViewModel = new PhysicalCountViewModel
            {
                PhysicalCount = { RecCreatedDate = DateTime.Now }
            };
            var pcResponse = physicalCountService.LoadPhysicalCountResponseData(id, Session["UserID"].ToString());

            physicalCountViewModel.Warehouses = pcResponse.Warehouses.Select(x => x.CreateDDL());
            if (pcResponse.PhysicalCount != null)
                physicalCountViewModel.PhysicalCount = pcResponse.PhysicalCount.CreateFromServerToClient();
            if (pcResponse.RequesterEmpId != null)
            {
                physicalCountViewModel.PhysicalCount.RequesterEmpId = pcResponse.RequesterEmpId;
                physicalCountViewModel.PhysicalCount.RequesterName = EPMS.WebModels.Resources.Shared.Common.TextDirection == "ltr" ? pcResponse.RequesterNameE : pcResponse.RequesterNameA;
            }


            physicalCountViewModel.PhysicalCountItems = pcResponse.PhysicalCountItems.Select(x => x.CreateFromServerToClient()).ToList();
            return View(physicalCountViewModel);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(PhysicalCountViewModel physicalCountViewModel)
        {
            try
            {
                DateTime date = DateTime.Now;
                string userId = User.Identity.GetUserId();
                if (physicalCountViewModel.PhysicalCount.PCId > 0)
                {
                    physicalCountViewModel.PhysicalCount.RecLastUpdatedBy = userId;
                    physicalCountViewModel.PhysicalCount.RecLastUpdatedDate = date;
                    
                    TempData["message"] = new MessageViewModel
                    {
                        Message = "Updated",
                        IsUpdated = true
                    };

                }
                else
                {
                    physicalCountViewModel.PhysicalCount.RecCreatedBy = userId;
                    physicalCountViewModel.PhysicalCount.RecCreatedDate = date;
                    physicalCountViewModel.PhysicalCount.RecLastUpdatedBy = userId;
                    physicalCountViewModel.PhysicalCount.RecLastUpdatedDate = date;
                    
                    TempData["message"] = new MessageViewModel
                    {
                        Message = "Saved",
                        IsUpdated = true
                    };
                }
                foreach (var physicalCountItemModel in physicalCountViewModel.PhysicalCountItems)
                {
                    if (physicalCountItemModel.PcItemId > 0)
                    {
                        physicalCountItemModel.RecLastUpdatedBy = userId;
                        physicalCountItemModel.RecLastUpdatedDate = date;
                    }
                    else
                    {
                        physicalCountItemModel.PcId = physicalCountViewModel.PhysicalCount.PCId;
                        physicalCountItemModel.RecCreatedBy = userId;
                        physicalCountItemModel.RecCreatedDate = date;
                        physicalCountItemModel.RecLastUpdatedBy = userId;
                        physicalCountItemModel.RecLastUpdatedDate = date;
                    }
                }
                PhysicalCount dataToSave = physicalCountViewModel.CreateFromClientToServer();
                if (physicalCountService.SavePhysicalCount(dataToSave))
                {
                    return RedirectToAction("Index");
                }
                return View();
            }
            catch (Exception)
            {
                return View();
            }
        }
    }
}