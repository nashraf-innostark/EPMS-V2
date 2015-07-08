using System;
using System.Linq;
using System.Web.Mvc;
using EPMS.Interfaces.IServices;
using EPMS.Models.RequestModels;
using EPMS.Models.ResponseModels;
using EPMS.Web.Controllers;
using EPMS.Web.ModelMappers;
using EPMS.Web.ViewModels.Common;
using EPMS.Web.ViewModels.PhysicalCount;

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
            PhysicalCountViewModel physicalCountViewModel=new PhysicalCountViewModel();
            physicalCountViewModel.PhysicalCount.RecCreatedDate = DateTime.Now;
            var pcResponse = physicalCountService.LoadPhysicalCountResponseData(id, Session["UserID"].ToString());

            physicalCountViewModel.Warehouses = pcResponse.Warehouses.Select(x => x.CreateDDL());

            if (pcResponse.PhysicalCount!=null)
                physicalCountViewModel.PhysicalCount = pcResponse.PhysicalCount.CreateFromServerToClient();
            if (pcResponse.RequesterEmpId != null)
            {
                physicalCountViewModel.PhysicalCount.RequesterEmpId = pcResponse.RequesterEmpId;
                physicalCountViewModel.PhysicalCount.RequesterName =Resources.Shared.Common.TextDirection == "ltr" ? pcResponse.RequesterNameE:pcResponse.RequesterNameA;
            }
            

            physicalCountViewModel.PhysicalCountItems = pcResponse.PhysicalCountItems.Select(x=>x.CreateFromServerToClient()).ToList();
            return View(physicalCountViewModel);
        }
        [HttpPost]
        public ActionResult Create(PhysicalCountViewModel physicalCountViewModel)
        {
            return View();
        }
    }
}