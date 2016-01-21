using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web.Script.Serialization;
using EPMS.Models.Common;
using EPMS.Models.RequestModels;
using System.Web.Mvc;
using EPMS.Interfaces.IServices;
using EPMS.Models.ResponseModels;
using EPMS.Web.Controllers;
using EPMS.Web.ViewModels.DIF;
using EPMS.WebModels.ModelMappers;
using EPMS.Web.Models.Common;
using EPMS.WebBase.Mvc;
using EPMS.WebModels.ModelMappers.Inventory.DIF;
using EPMS.WebModels.ViewModels.Common;
using EPMS.WebModels.ViewModels.DIF;
using EPMS.WebModels.WebsiteModels;
using Microsoft.AspNet.Identity;

namespace EPMS.Web.Areas.Inventory.Controllers
{
    [Authorize]
    [SiteAuthorize(PermissionKey = "IS", IsModule = true)]
    public class DIFController : BaseController
    {
        private readonly IDIFService rifService;
        private readonly IItemVariationService itemVariationService;

        public DIFController(IDIFService rifService, IItemVariationService itemVariationService)
        {
            this.rifService = rifService;
            this.itemVariationService = itemVariationService;
        }

        // GET: Inventory/Dif
        [SiteAuthorize(PermissionKey = "DIFIndex")]
        public ActionResult Index()
        {
            DifSearchRequest searchRequest = Session["PageMetaData"] as DifSearchRequest;
            ViewBag.UserRole = Session["RoleName"].ToString().ToLower();
            Session["PageMetaData"] = null;

            DifListViewModel viewModel = new DifListViewModel
            {
                SearchRequest = searchRequest ?? new DifSearchRequest()
            };

            ViewBag.MessageVM = TempData["message"] as MessageViewModel;
            return View(viewModel);
        }
        [HttpPost]
        public ActionResult Index(DifSearchRequest searchRequest)
        {
            searchRequest.SearchString = Request["search"];
            DifListViewModel viewModel = new DifListViewModel();
            ViewBag.UserRole = Session["RoleName"].ToString().ToLower();
            searchRequest.Requester = (UserRole)Convert.ToInt32(Session["RoleKey"].ToString()) == UserRole.Employee ? Session["UserID"].ToString() : "Admin";
            var requestResponse = rifService.LoadAllDifs(searchRequest);
            var data = requestResponse.Difs.Select(x => x.CreateDifServerToClient());
            var responseData = data as IList<DIF> ?? data.ToList();
            if (responseData.Any())
            {
                viewModel.aaData = responseData;
                viewModel.iTotalRecords = requestResponse.TotalCount;
                viewModel.iTotalDisplayRecords = requestResponse.TotalCount;
                viewModel.sEcho = searchRequest.sEcho;
                //viewModel.sLimit = searchRequest.iDisplayLength;
            }
            else
            {
                viewModel.aaData = Enumerable.Empty<DIF>();
                viewModel.iTotalRecords = requestResponse.TotalCount;
                viewModel.iTotalDisplayRecords = requestResponse.TotalCount;
                viewModel.sEcho = searchRequest.sEcho;
                //viewModel.sLimit = searchRequest.iDisplayLength;
            }
            // Keep Search Request in Session
            Session["PageMetaData"] = searchRequest;
            return Json(viewModel, JsonRequestBehavior.AllowGet);
        }

        // GET: Inventory/Dif/Details/5
        [SiteAuthorize(PermissionKey = "DIFDetails")]
        public ActionResult Details(int id, string from)
        {
            var Difresponse = rifService.LoadDifResponseData(id, from);
            DIFViewModel rifViewModel = new DIFViewModel();
            if (Difresponse.Dif != null)
            {
                rifViewModel.Dif = Difresponse.Dif.CreateDifServerToClient();
                if (EPMS.WebModels.Resources.Shared.Common.TextDirection == "ltr")
                {
                    rifViewModel.Dif.RequesterName = Difresponse.RequesterNameE;
                    rifViewModel.Dif.ManagerName = Difresponse.ManagerNameE;
                }
                else
                {
                    rifViewModel.Dif.RequesterName = Difresponse.RequesterNameA;
                    rifViewModel.Dif.ManagerName = Difresponse.ManagerNameA;
                }
                rifViewModel.Dif.EmpJobId = Difresponse.EmpJobId;
                rifViewModel.DifItem = Difresponse.DifItem.Select(x => x.CreateDifItemDetailsServerToClient()).ToList();
            }
            else
            {
                rifViewModel.Dif = new DIF
                {
                    RequesterName = Session["FullName"].ToString()
                };
                rifViewModel.DifItem = new List<DIFItem>();
            }
            rifViewModel.ItemVariationDropDownList = Difresponse.ItemVariationDropDownList;
            ViewBag.From = from;
            return View(rifViewModel);
        }
        [HttpPost]
        [ValidateInput(false)]//this is due to CK Editor
        public ActionResult Details(DIFViewModel rifViewModel)
        {
            try
            {
                rifViewModel.Dif.RecUpdatedBy = User.Identity.GetUserId();
                rifViewModel.Dif.RecUpdatedDate = DateTime.Now;
                rifViewModel.Dif.ManagerId = User.Identity.GetUserId();
                TempData["message"] = new MessageViewModel
                {
                    //Message = Resources.Inventory.DIF.DIF.DIFReplied,
                    IsUpdated = true
                };

                var DifToBeSaved = rifViewModel.CreateDifDetailsClientToServer();
                if (rifService.UpdateDIF(DifToBeSaved))
                {
                    //success
                    return RedirectToAction("Index");
                }
                //failed to save
                return View();
            }
            catch (Exception)
            {
                return View();
            }
        }

        // GET: Inventory/Dif/Create
        [SiteAuthorize(PermissionKey = "DIFCreate")]
        public ActionResult Create(long? id)
        {
            var Difresponse = rifService.LoadDifResponseData(id, "");
            DIFViewModel rifViewModel = new DIFViewModel();
            if (Difresponse.Dif != null)
            {
                rifViewModel.Dif = Difresponse.Dif.CreateDifServerToClient();
                rifViewModel.Dif.RequesterName = EPMS.WebModels.Resources.Shared.Common.TextDirection == "ltr" ? Difresponse.RequesterNameE : Difresponse.RequesterNameA;
                rifViewModel.DifItem = Difresponse.DifItem.Select(x => x.CreateDifItemServerToClient()).ToList();
            }
            else
            {
                rifViewModel.Dif = new DIF
                {
                    FormNumber = Utility.GenerateFormNumber("DI", Difresponse.LastFormNumber),
                    RequesterName = Session["UserFullName"].ToString()
                };
                rifViewModel.DifItem = new List<DIFItem>();
            }
            rifViewModel.Warehouses = Difresponse.Warehouses.Select(x => x.CreateDDL());
            rifViewModel.ItemVariationDropDownList = Difresponse.ItemVariationDropDownList;
            ViewBag.IsIncludeNewJsTree = true;
            return View(rifViewModel);
        }

        // POST: Inventory/Dif/Create
        [HttpPost]
        [ValidateInput(false)]//this is due to CK Editor
        public ActionResult Create(DIFViewModel rifViewModel)
        {
            try
            {
                if (rifViewModel.Dif.Id > 0)
                {
                    rifViewModel.Dif.RecUpdatedBy = User.Identity.GetUserId();
                    rifViewModel.Dif.RecUpdatedDate = DateTime.Now;

                    TempData["message"] = new MessageViewModel
                    {
                        //Message = Resources.Inventory.DIF.DIF.RFIUpdated,
                        IsUpdated = true
                    };
                }
                else
                {
                    rifViewModel.Dif.RecCreatedBy = User.Identity.GetUserId();
                    rifViewModel.Dif.RecCreatedDate = DateTime.Now.ToString("dd/MM/yyyy", new CultureInfo("en"));

                    rifViewModel.Dif.RecUpdatedBy = User.Identity.GetUserId();
                    rifViewModel.Dif.RecUpdatedDate = DateTime.Now;
                    TempData["message"] = new MessageViewModel
                    {
                        //Message = Resources.Inventory.DIF.DIF.DIFCreated,
                        IsSaved = true
                    };
                }

                var DifToBeSaved = rifViewModel.CreateDifClientToServer();
                if (rifService.SaveDIF(DifToBeSaved))
                {
                    //success
                    return RedirectToAction("Index");
                }
                //failed to save
                return View();
            }
            catch
            {
                return View();
            }
        }
        [SiteAuthorize(PermissionKey = "DIFHistory")]
        public ActionResult History(long? id)
        {
            DifHistoryResponse response = rifService.GetDifHistoryData(id);
            DifHistoryViewModel viewModel = new DifHistoryViewModel
            {
                Difs = response.Difs != null ? response.Difs.Select(x => x.CreateDifServerToClient()).ToList() : new List<DIF>(),
                RecentDif = response.RecentDif != null ? response.RecentDif.CreateDifServerToClient() : new DIF(),
                DifItems = response.DifItems.Any() ? response.DifItems.Select(x => x.CreateDifItemDetailsServerToClient()).ToList() : new List<DIFItem>()
            };
            if (response.RecentDif != null)
            {
                viewModel.RecentDif.RequesterName = response.RequesterNameEn;
                viewModel.RecentDif.RequesterNameAr = response.RequesterNameAr;
                viewModel.RecentDif.ManagerName = response.ManagerNameEn;
                viewModel.RecentDif.ManagerNameAr = response.ManagerNameAr;
            }
            return View(viewModel);
        }
        // POST: Inventory/Dif/History
        [HttpPost]
        [ValidateInput(false)]//this is due to CK Editor
        public ActionResult History(DifHistoryViewModel viewModel)
        {
            try
            {
                viewModel.RecentDif.RecUpdatedBy = User.Identity.GetUserId();
                viewModel.RecentDif.RecUpdatedDate = DateTime.Now;
                viewModel.RecentDif.ManagerId = User.Identity.GetUserId();
                TempData["message"] = new MessageViewModel
                {
                    //Message = Resources.Inventory.DIF.DIF.DIFReplied,
                    IsUpdated = true
                };

                var difToBeSaved = viewModel.RecentDif.CreateDifDetailsClientToServer();
                if (rifService.UpdateDIF(difToBeSaved))
                {
                    //success
                    return RedirectToAction("Index");
                }
                //failed to save
                return View();
            }
            catch (Exception)
            {
                return View();
            }
        }

        #region GetWarehouseItems
        [HttpGet]
        public JsonResult GetWarehouseItems(long warehouseId, string direction)
        {
            ItemVariationForWarehouse warehouseItems = itemVariationService.GetItemVariationByWarehouseId(warehouseId);
            WarehouseItems items = new WarehouseItems
            {
                ItemVariationDropDownListItems = warehouseItems.ItemVariationDropDownListItems
            };
            IList<WebModels.WebsiteModels.Common.JsTreeJson> details = Utility.InventoryDepartmentTreeByWarehouse(warehouseItems.InventoryDepartments, warehouseId, direction);
            var serializer = new JavaScriptSerializer();
            var serializedResult = serializer.Serialize(details);
            items.InventoryDepartments = serializedResult;
            return Json(items, JsonRequestBehavior.AllowGet);
        }
        
        #endregion
    }
}
