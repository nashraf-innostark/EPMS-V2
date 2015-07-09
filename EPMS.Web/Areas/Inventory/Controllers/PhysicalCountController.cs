using System;
using System.Linq;
using System.Web.Mvc;
using EPMS.Interfaces.IServices;
using EPMS.Models.DomainModels;
using EPMS.Models.RequestModels;
using EPMS.Models.ResponseModels;
using EPMS.Web.Controllers;
using EPMS.Web.ModelMappers;
using EPMS.Web.ViewModels.Common;
using EPMS.Web.ViewModels.PhysicalCount;
using Microsoft.AspNet.Identity;

namespace EPMS.Web.Areas.Inventory.Controllers
{
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
                physicalCountViewModel.PhysicalCount.RequesterName = Resources.Shared.Common.TextDirection == "ltr" ? pcResponse.RequesterNameE : pcResponse.RequesterNameA;
            }


            physicalCountViewModel.PhysicalCountItems = pcResponse.PhysicalCountItems.Select(x => x.CreateFromServerToClient()).ToList();
            return View(physicalCountViewModel);
        }
        [HttpPost]
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
                    foreach (var physicalCountItemModel in physicalCountViewModel.PhysicalCountItems)
                    {
                        physicalCountItemModel.RecLastUpdatedBy = userId;
                        physicalCountItemModel.RecLastUpdatedDate = date;
                    }
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
                    foreach (var physicalCountItemModel in physicalCountViewModel.PhysicalCountItems)
                    {
                        physicalCountItemModel.RecCreatedBy = userId;
                        physicalCountItemModel.RecCreatedDate = date;
                        physicalCountItemModel.RecLastUpdatedBy = userId;
                        physicalCountItemModel.RecLastUpdatedDate = date;
                    }
                    TempData["message"] = new MessageViewModel
                    {
                        Message = "Saved",
                        IsUpdated = true
                    };
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