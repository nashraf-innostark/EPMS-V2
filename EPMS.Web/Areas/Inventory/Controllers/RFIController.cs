using System;
using System.Collections.Generic;
using System.Linq;
using EPMS.Models.Common;
using EPMS.Models.RequestModels;
using EPMS.Models.ResponseModels;
using System.Configuration;
using System.Globalization;
using System.Web.Mvc;
using EPMS.Interfaces.IServices;
using EPMS.Web.Controllers;
using EPMS.WebBase.Mvc;
using EPMS.WebModels.ModelMappers;
using EPMS.WebModels.ModelMappers.Inventory.RFI;
using EPMS.WebModels.ViewModels.Common;
using EPMS.WebModels.ViewModels.RFI;
using EPMS.WebModels.WebsiteModels;
using Microsoft.AspNet.Identity;
using Customer = EPMS.Models.DashboardModels.Customer;
using Order = EPMS.Models.DashboardModels.Order;

namespace EPMS.Web.Areas.Inventory.Controllers
{
    [Authorize]
    //[SiteAuthorize(PermissionKey = "IS", IsModule = true)]
    public class RFIController : BaseController
    {
        private readonly IRFIService rfiService;
        private readonly IOrdersService ordersService;

        public RFIController(IRFIService rfiService, IOrdersService ordersService)
        {
            this.rfiService = rfiService;
            this.ordersService = ordersService;
        }

        // GET: Inventory/RFI
        [SiteAuthorize(PermissionKey = "RFIIndex")]
        public ActionResult Index()
        {
            RfiSearchRequest searchRequest = Session["PageMetaData"] as RfiSearchRequest;
            ViewBag.UserRole = Session["RoleName"].ToString().ToLower();
            Session["PageMetaData"] = null;

            RfiListViewModel viewModel = new RfiListViewModel
            {
                SearchRequest = searchRequest ?? new RfiSearchRequest()
            };

            ViewBag.MessageVM = TempData["message"] as MessageViewModel;
            return View(viewModel);
        }
        [HttpPost]
        public ActionResult Index(RfiSearchRequest searchRequest)
        {
            searchRequest.SearchString = Request["search"];
            RfiListViewModel viewModel = new RfiListViewModel();
            ViewBag.UserRole = Session["RoleName"].ToString().ToLower();
            searchRequest.Requester = (UserRole)Convert.ToInt32(Session["RoleKey"].ToString()) == UserRole.Employee ? Session["UserID"].ToString() : "Admin";
            var requestResponse = rfiService.LoadAllRfis(searchRequest);
            var data = requestResponse.Rfis.Select(x => x.CreateRfiServerToClient());
            var responseData = data as IList<WebModels.WebsiteModels.RFI> ?? data.ToList();
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
                viewModel.aaData = Enumerable.Empty<WebModels.WebsiteModels.RFI>();
                viewModel.iTotalRecords = requestResponse.TotalCount;
                viewModel.iTotalDisplayRecords = requestResponse.TotalCount;
                viewModel.sEcho = searchRequest.sEcho;
                //viewModel.sLimit = searchRequest.iDisplayLength;
            }
            // Keep Search Request in Session
            Session["PageMetaData"] = searchRequest;
            return Json(viewModel, JsonRequestBehavior.AllowGet);
        }

        // GET: Inventory/RFI/Details/5
        [SiteAuthorize(PermissionKey = "RFIDetails")]
        public ActionResult Details(int id, string from)
        {
            var rfiresponse = rfiService.LoadRfiResponseData(id, false, from);
            RFIViewModel rfiViewModel = new RFIViewModel();
            if (rfiresponse.Rfi != null)
            {
                rfiViewModel.Rfi = rfiresponse.Rfi.CreateRfiServerToClient();
                if (EPMS.WebModels.Resources.Shared.Common.TextDirection == "ltr")
                {
                    rfiViewModel.Rfi.RequesterName = rfiresponse.RequesterNameE;
                    rfiViewModel.Rfi.CustomerName = rfiresponse.CustomerNameE;
                    rfiViewModel.Rfi.ManagerName = rfiresponse.ManagerNameE;
                }
                else
                {
                    rfiViewModel.Rfi.RequesterName = rfiresponse.RequesterNameA;
                    rfiViewModel.Rfi.CustomerName = rfiresponse.CustomerNameA;
                    rfiViewModel.Rfi.ManagerName = rfiresponse.ManagerNameA;
                }
                rfiViewModel.Rfi.EmpJobId = rfiresponse.EmpJobId;
                rfiViewModel.Rfi.OrderNo = rfiresponse.OrderNo;
                rfiViewModel.RfiItem = rfiresponse.RfiItem.Select(x => x.CreateRfiItemDetailsServerToClient()).ToList();
            }
            else
            {
                rfiViewModel.Rfi = new WebModels.WebsiteModels.RFI
                {
                    RequesterName = Session["FullName"].ToString()
                };
                rfiViewModel.RfiItem = new List<WebModels.WebsiteModels.RFIItem>();
            }
            rfiViewModel.ItemVariationDropDownList = rfiresponse.ItemVariationDropDownList;
            ViewBag.From = from;
            return View(rfiViewModel);
        }
        [HttpPost]
        [ValidateInput(false)]//this is due to CK Editor
        public ActionResult Details(RFIViewModel rfiViewModel)
        {
            try
            {
                rfiViewModel.Rfi.RecUpdatedBy = User.Identity.GetUserId();
                rfiViewModel.Rfi.RecUpdatedDate = DateTime.Now;
                rfiViewModel.Rfi.ManagerId = User.Identity.GetUserId();

                TempData["message"] = new MessageViewModel
                {
                    Message = EPMS.WebModels.Resources.RFI.RFI.RFIReplied,
                    IsUpdated = true
                };

                var rfiToBeSaved = rfiViewModel.CreateRfiDetailsClientToServer();
                if (rfiService.UpdateRFI(rfiToBeSaved))
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

        // GET: Inventory/RFI/Create
        [SiteAuthorize(PermissionKey = "RFICreate")]
        public ActionResult Create(long? id)
        {
            string direction = EPMS.WebModels.Resources.Shared.Common.TextDirection;
            bool loadCustomersAndOrders = CheckHasCustomerModule();
            var rfiresponse = rfiService.LoadRfiResponseData(id, loadCustomersAndOrders, "");
            RFIViewModel rfiViewModel = new RFIViewModel();
            if (rfiresponse.Rfi != null)
            {
                rfiViewModel.Rfi = rfiresponse.Rfi.CreateRfiServerToClient();
                rfiViewModel.Rfi.RequesterName = direction == "ltr" ? rfiresponse.RequesterNameE : rfiresponse.RequesterNameA;
                rfiViewModel.RfiItem = rfiresponse.RfiItem.Select(x => x.CreateRfiItemServerToClient()).ToList();
            }
            else
            {
                rfiViewModel.Rfi = new WebModels.WebsiteModels.RFI
                {
                    FormNumber = Utility.GenerateFormNumber("RF", rfiresponse.LastFormNumber),
                    RequesterName = direction == "ltr" ? Session["UserFullName"].ToString() : Session["UserFullNameA"].ToString()
                };
                rfiViewModel.RfiItem = new List<WebModels.WebsiteModels.RFIItem>();
            }
            if (loadCustomersAndOrders)
            {
                rfiViewModel.Customers = rfiresponse.Customers.Any() ? rfiresponse.Customers.Select(x => x.CreateForDashboard()) : new List<Customer>();
                rfiViewModel.Orders = rfiresponse.Orders.Any() ? rfiresponse.Orders.Select(x => x.CreateForDashboard()) : new List<Order>();
                //set customerId
                if (rfiViewModel.Rfi.OrderId > 0)
                    rfiViewModel.Rfi.CustomerId = rfiViewModel.Orders.FirstOrDefault(x => x.OrderId == rfiViewModel.Rfi.OrderId).CustomerId;
            }
            rfiViewModel.ItemVariationDropDownList = rfiresponse.ItemVariationDropDownList;
            ViewBag.IsIncludeNewJsTree = true;
            return View(rfiViewModel);
        }

        // POST: Inventory/RFI/Create
        [HttpPost]
        [ValidateInput(false)]//this is due to CK Editor
        public ActionResult Create(RFIViewModel rfiViewModel)
        {
            try
            {
                if (rfiViewModel.Rfi.RFIId > 0)
                {
                    rfiViewModel.Rfi.RecUpdatedBy = User.Identity.GetUserId();
                    rfiViewModel.Rfi.RecUpdatedDate = DateTime.Now;

                    TempData["message"] = new MessageViewModel
                    {
                        Message = EPMS.WebModels.Resources.RFI.RFI.RFIUpdated,
                        IsUpdated = true
                    };
                }
                else
                {
                    rfiViewModel.Rfi.RecCreatedBy = User.Identity.GetUserId();
                    rfiViewModel.Rfi.RecCreatedDate = DateTime.Now;

                    rfiViewModel.Rfi.RecUpdatedBy = User.Identity.GetUserId();
                    rfiViewModel.Rfi.RecUpdatedDate = DateTime.Now;
                    TempData["message"] = new MessageViewModel { Message = EPMS.WebModels.Resources.RFI.RFI.RFICreated, IsSaved = true };
                }

                var rfiToBeSaved = rfiViewModel.CreateRfiClientToServer();
                if (rfiService.SaveRFI(rfiToBeSaved))
                {
                    //success
                    return RedirectToAction("Index");
                }
                //failed to save
                return View(rfiViewModel);
            }
            catch
            {
                return View();
            }
        }

        [SiteAuthorize(PermissionKey = "RFIHistory")]
        public ActionResult History(long? id)
        {
            RfiHistoryResponse response = rfiService.GetRfiHistoryData(id);
            RFIHistoryViewModel viewModel = new RFIHistoryViewModel();
            viewModel.Rfis = response.Rfis.Any() ? response.Rfis.Select(x => x.CreateRfiServerToClient()).ToList() : new List<WebModels.WebsiteModels.RFI>();
            viewModel.RfiItems = response.RfiItems != null ? response.RfiItems.Select(x => x.CreateRfiItemDetailsServerToClient()).ToList() : new List<WebModels.WebsiteModels.RFIItem>();
            viewModel.RecentRfi = (response.RecentRfi != null && response.RecentRfi.RFIId > 0) ? response.RecentRfi.CreateRfiServerToClient() : new WebModels.WebsiteModels.RFI();

            if (viewModel.RecentRfi != null)
            {
                viewModel.RecentRfi.RequesterName = response.RequesterNameEn;
                viewModel.RecentRfi.RequesterNameAr = response.RequesterNameAr;
                viewModel.RecentRfi.ManagerName = response.ManagerNameEn;
                viewModel.RecentRfi.ManagerNameAr = response.ManagerNameAr;
            }
            return View(viewModel);
        }
        [HttpPost]
        [ValidateInput(false)]//this is due to CK Editor
        public ActionResult History(RFIHistoryViewModel viewModel)
        {
            try
            {
                viewModel.RecentRfi.RecUpdatedBy = User.Identity.GetUserId();
                viewModel.RecentRfi.RecUpdatedDate = DateTime.Now;
                viewModel.RecentRfi.ManagerId = User.Identity.GetUserId();

                TempData["message"] = new MessageViewModel
                {
                    Message = EPMS.WebModels.Resources.RFI.RFI.RFIReplied,
                    IsUpdated = true
                };

                var rfiToBeSaved = viewModel.RecentRfi.CreateRfiDetailsClientToServer();
                if (rfiService.UpdateRFI(rfiToBeSaved))
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

        private bool CheckHasCustomerModule()
        {
            // check license
            var licenseKeyEncrypted = ConfigurationManager.AppSettings["LicenseKey"].ToString(CultureInfo.InvariantCulture);
            string LicenseKey = WebBase.EncryptDecrypt.StringCipher.Decrypt(licenseKeyEncrypted, "123");
            var splitLicenseKey = LicenseKey.Split('|');
            string[] Modules = splitLicenseKey[4].Split(';');
            if (Modules.Contains("CS") || Modules.Contains("Customer Service"))
            {
                ViewBag.HasCustomerModule = true;
                return true;
            }
            else
            {
                ViewBag.HasCustomerModule = false;
                return false;
            }
        }

        [HttpGet]
        public JsonResult GetOrderItems(long orderId)
        {
            var order = ordersService.GetOrderByOrderId(orderId);
            if (order != null && order.Quotation != null)
            {
                if (order.Quotation.QuotationItemDetails.Any())
                {
                    return Json(order.Quotation.QuotationItemDetails.Select(x=>x.CreateForRfi()), JsonRequestBehavior.AllowGet);
                }
            }
            return Json(new QuotationItemDetail(), JsonRequestBehavior.AllowGet);
        }
    }
}
