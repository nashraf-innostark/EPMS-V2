using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;
using EPMS.Interfaces.IServices;
using EPMS.WebModels.ModelMappers;
using EPMS.Models.RequestModels;
using EPMS.Models.ResponseModels;
using EPMS.WebModels.ViewModels.Orders;
using EPMS.Web.Controllers;
using EPMS.WebBase.EncryptDecrypt;
using EPMS.WebModels.ViewModels.Common;
using EPMS.WebBase.Mvc;
using EPMS.WebModels.ViewModels.Quotation;
using EPMS.WebModels.WebsiteModels;
using Microsoft.AspNet.Identity;
using Order = EPMS.WebModels.WebsiteModels.Order;

namespace EPMS.Web.Areas.CMS.Controllers
{
    [Authorize]
    [SiteAuthorize(PermissionKey = "CS", IsModule = true)]
    public class OrdersController : BaseController
    {
        #region Private
        private readonly IOrdersService ordersService;
        private readonly IQuotationService quotationService;
        #endregion

        #region Constructor
        public OrdersController(IOrdersService ordersService, IQuotationService quotationService)
        {
            this.ordersService = ordersService;
            this.quotationService = quotationService;
        }

        #endregion

        #region Public

        #region ListView

        // GET: HR/Orders
        [SiteAuthorize(PermissionKey = "OrdersIndex")]
        public ActionResult Index()
        {
            OrdersListViewModel viewModel = new OrdersListViewModel();
            ViewBag.MessageVM = TempData["message"] as MessageViewModel;
            return View(viewModel);
        }

        /// <summary>
        /// Get List of Orders for ListView
        /// </summary>
        /// <param name="searchRequest"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Index(OrdersSearchRequest searchRequest)
        {
            searchRequest.UserId = Guid.Parse(User.Identity.GetUserId());
            searchRequest.SearchString = Request["search"];
            OrdersResponse ordersList = null;
            IEnumerable<Order> orders = null;
            string[] userPermissionsSet = (string[])Session["UserPermissionSet"];
            if (!userPermissionsSet.Contains("OrderCreate"))
            {
                searchRequest.CustomerId = 0;
                ordersList = ordersService.GetAllOrders(searchRequest);
                orders = ordersList.Orders.Select(o => o.CreateFromServerToClientLv());
            }
            if (userPermissionsSet.Contains("OrderCreate"))
            {
                searchRequest.CustomerId = Convert.ToInt64(Session["CustomerID"]);
                ordersList = ordersService.GetAllOrders(searchRequest);
                orders = ordersList.Orders.Select(o => o.CreateFromServerToClientLv());
            }
            if (ordersList == null) return View(new OrdersListViewModel());
            OrdersListViewModel viewModel = new OrdersListViewModel
            {
                aaData = orders,
                iTotalRecords = Convert.ToInt32(ordersList.TotalRecords),
                iTotalDisplayRecords = Convert.ToInt32(ordersList.TotalDisplayRecords),
                SearchRequest = searchRequest
            };
            return Json(viewModel, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Create/Update

        /// <summary>
        /// Get Order by Id or return Create new Order View
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [SiteAuthorize(PermissionKey = "OrderCreate")]
        public ActionResult Create(long? id)
        {
            var direction = WebModels.Resources.Shared.Common.TextDirection;
            long quotationId = 0;
            if (id != null)
            {
                quotationId = (long)id;
            }
            if (Request.UrlReferrer != null)
            {
                ViewBag.backUrl = Request.UrlReferrer;
            }
            else
            {
                ViewBag.backUrl = "";
            }
            QuotationCreateViewModel viewModel = new QuotationCreateViewModel();
            QuotationResponse quotResponse = quotationService.GetQuotationResponse(quotationId);
            ViewBag.Customers = quotResponse.Customers.Any() ?
                        quotResponse.Customers.Select(x => x.CreateForDropDown()) : new List<CustomerDropDown>();
            ViewBag.IsIncludeNewJsTree = true;
            ViewBag.ShowExcelImport = CheckInventoryModule() != true;
            ViewBag.FromClient = false;
            if (id == null)
            {
                viewModel.ItemVariationDropDownList = quotResponse.ItemVariationDropDownList;
                viewModel.CreatedByName = GetCreatedBy(direction, null);
                return View(viewModel);
            }
            viewModel = quotResponse.Quotation.CreateFromServerToClient();
            viewModel.ItemVariationDropDownList = quotResponse.ItemVariationDropDownList;
            viewModel.CreatedByName = GetCreatedBy(direction, quotResponse.Quotation);
            viewModel.OldItemDetailsCount = viewModel.QuotationItemDetails.Count;
            ViewBag.Rfqs = quotResponse.Rfqs.Any() ?
                        quotResponse.Rfqs.Select(x => x.CreateForDropDown()) : new List<RfqDropDown>();
            return View(viewModel);
        }

        /// <summary>
        /// Add or Update Order
        /// </summary>
        /// <param name="viewModel"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateInput(false)]
        [SiteAuthorize(PermissionKey = "OrderCreate")]
        public ActionResult Create(QuotationCreateViewModel viewModel)
        {
            QuotationResponse response = new QuotationResponse();
            // Update case
            if (viewModel.QuotationId > 0)
            {
            }
            else
            {
                // Add case
                // Save Quotation
                viewModel.FromOrder = true;
                viewModel.RecCreatedBy = User.Identity.GetUserId();
                viewModel.RecCreatedDate = DateTime.Now.ToString("dd/MM/yyyy", new CultureInfo("en"));
                viewModel.RecLastUpdatedBy = User.Identity.GetUserId();
                viewModel.RecLastUpdatedDate = DateTime.Now;
                foreach (var detail in viewModel.QuotationItemDetails)
                {
                    detail.RecCreatedBy = User.Identity.GetUserId();
                    detail.RecCreatedDate = DateTime.Now.ToString("dd/MM/yyyy", new CultureInfo("en"));
                    detail.RecLastUpdatedBy = User.Identity.GetUserId();
                    detail.RecLastUpdatedDate = DateTime.Now;
                }
                var quotationToAdd = viewModel.CreateFromClientToServer();
                response = quotationService.AddQuotation(quotationToAdd);
                if (response.Status)
                {
                    TempData["message"] = new MessageViewModel
                    {
                        Message = WebModels.Resources.CMS.Order.Added,
                        IsSaved = true
                    };
                    return RedirectToAction("Index");
                }
            }
            return RedirectToAction("Index");
        }

        #endregion

        #region Detail

        [SiteAuthorize(PermissionKey = "OrderDetails")]
        public ActionResult Detail(long? id)
        {
            var direction = WebModels.Resources.Shared.Common.TextDirection;
            QuotationDetailViewModel viewModel = new QuotationDetailViewModel();
            long quotatioId = 0;
            if (id != null)
            {
                quotatioId = (long)id;
            }
            QuotationDetailResponse response = quotationService.GetQuotationDetail(quotatioId);
            viewModel.Profile = response.Profile != null ? response.Profile.CreateFromServerToClientForQuotation() : new CompanyProfile();
            if (id != null)
            {
                viewModel.Quotation = response.Quotation != null ? response.Quotation.CreateFromServerToClientLv() : new WebModels.WebsiteModels.Quotation();
                viewModel.EmployeeName = GetCreatedBy(direction, response.Quotation);
            }
            ViewBag.LogoPath = ConfigurationManager.AppSettings["CompanyLogo"] + viewModel.Profile.CompanyLogoPath;
            return View(viewModel);
        }
        #endregion

        #region CheckInventoryModule

        bool CheckInventoryModule()
        {
            string licenseKeyEncrypted = ConfigurationManager.AppSettings["LicenseKey"].ToString(CultureInfo.InvariantCulture);
            string licenseKey = StringCipher.Decrypt(licenseKeyEncrypted, "123"); //DesertStarts
            string[] splitLicenseKey = licenseKey.Split('|');
            string modules = splitLicenseKey[4];
            string[] splitModules = modules.Split(';');
            if (splitModules.Contains("IS"))
            {
                return true;
            }
            return false;
        }

        #endregion

        #region Functions

        public string GetCreatedBy(string direction, EPMS.Models.DomainModels.Quotation quotation)
        {
            string createdBy = "";
            if (quotation != null)
            {
                var emp = quotation.AspNetUser.Employee;
                var cus = quotation.AspNetUser.Customer;
                if (emp != null)
                {
                    if (direction == "ltr" && Session["UserFullName"] != null)
                    {
                        createdBy = emp.EmployeeFirstNameE + " " + emp.EmployeeMiddleNameE + " " + emp.EmployeeLastNameE;
                    }
                    else if (direction == "rtl" && Session["UserFullNameA"] != null)
                    {
                        createdBy = emp.EmployeeFirstNameA + " " + emp.EmployeeMiddleNameA + " " + emp.EmployeeLastNameA;
                    }
                }
                else if (cus != null)
                {
                    createdBy = direction == "ltr" ? cus.CustomerNameE : cus.CustomerNameA;
                }
                else
                {
                    createdBy = "Admin";
                }
            }
            else
            {
                if (direction == "ltr" && Session["UserFullName"] != null)
                {
                    createdBy = Session["UserFullName"].ToString();
                }
                else if (direction == "rtl" && Session["UserFullNameA"] != null)
                {
                    createdBy = Session["UserFullNameA"].ToString();
                }
            }
            return createdBy;
        }
        #endregion

        #endregion
    }
}