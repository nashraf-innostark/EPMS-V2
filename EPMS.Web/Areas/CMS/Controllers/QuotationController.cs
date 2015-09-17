using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;
using EPMS.Interfaces.IServices;

using EPMS.Models.RequestModels;
using EPMS.WebModels.ModelMappers;
using EPMS.WebModels.ModelMappers.Website.ShoppingCart;
using EPMS.WebModels.ViewModels.Quotation;
using EPMS.Web.Controllers;
using EPMS.WebModels.ViewModels.Common;
using EPMS.WebBase.Mvc;
using EPMS.WebModels.ViewModels.WebsiteClient;
using EPMS.WebModels.WebsiteModels;
using Microsoft.AspNet.Identity;
using Order = EPMS.WebModels.WebsiteModels.Order;
using Quotation = EPMS.WebModels.Resources.CMS.Quotation;

namespace EPMS.Web.Areas.CMS.Controllers
{
    [Authorize]
    [SiteAuthorize(PermissionKey = "CS", IsModule = true)]
    public class QuotationController : BaseController
    {
        #region Private
        private readonly ICustomerService CustomerService;
        private readonly IAspNetUserService AspNetUserService;
        private readonly IOrdersService OrdersService;
        private readonly IQuotationService QuotationService;
        private readonly IQuotationItemService QuotationItemService;
        private readonly ICompanyProfileService ProfileService;
        private readonly IShoppingCartService cartService;
        #endregion

        #region Constructor

        public QuotationController(ICustomerService customerService, IAspNetUserService aspNetUserService, IOrdersService ordersService, IQuotationService quotationService, IQuotationItemService quotationItemService, ICompanyProfileService profileService, IShoppingCartService cartService)
        {
            CustomerService = customerService;
            AspNetUserService = aspNetUserService;
            OrdersService = ordersService;
            QuotationService = quotationService;
            QuotationItemService = quotationItemService;
            ProfileService = profileService;
            this.cartService = cartService;
        }

        #endregion

        #region Public

        #region ListView

// GET: CMS/Quotation
        [SiteAuthorize(PermissionKey = "QuotationIndex")]
        public ActionResult Index()
        {
            QuotationListViewModel viewModel = new QuotationListViewModel
            {
                SearchRequest = new QuotationSearchRequest(),
            };
            ViewBag.MessageVM = TempData["message"] as MessageViewModel;
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Index(QuotationSearchRequest searchRequest)
        {
            QuotationListViewModel viewModel = new QuotationListViewModel();
            string roleName = (string) Session["RoleName"];
            if (roleName == "Admin")
            {
                searchRequest.CustomerId = 0;
            }
            if (roleName == "Customer")
            {
                searchRequest.CustomerId = (long) Session["CustomerID"];
            }
            var quotationList = QuotationService.GetAllQuotation(searchRequest);
            viewModel.aaData = quotationList.Quotations.Select(x => x.CreateFromServerToClientLv());
            viewModel.iTotalRecords = quotationList.TotalCount;
            viewModel.iTotalDisplayRecords = quotationList.TotalCount;
            return Json(viewModel, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Add/Update

        [SiteAuthorize(PermissionKey = "QuotationsCreate")]
        public ActionResult Create(long? id, string from)
        {
            var direction = EPMS.WebModels.Resources.Shared.Common.TextDirection;
            var users = AspNetUserService.FindById(User.Identity.GetUserId());
            var createdByName = "";
            if (users.Employee != null && direction == "ltr")
            {
                createdByName = users.Employee.EmployeeFirstNameE + " " + users.Employee.EmployeeMiddleNameE + " " +
                                users.Employee.EmployeeLastNameE;
            }
            if (users.Employee != null && direction == "rtl")
            {
                createdByName = users.Employee.EmployeeFirstNameA + " " + users.Employee.EmployeeMiddleNameA + " " +
                                users.Employee.EmployeeLastNameA;
            }
            if (from == "Client")
            {
                string userId = User.Identity.GetUserId();
                var response = cartService.GetUserCart(userId);
                if (response.ShoppingCarts.Any())
                {
                    QuotationCreateViewModel model = new QuotationCreateViewModel
                    {
                        QuotationItemDetails = new List<QuotationItemDetail>()
                    };
                    var shoppingCart = response.ShoppingCarts.Select(x => x.CreateFromServerToClient());
                    foreach (var shoppingCartItem in shoppingCart.FirstOrDefault().ShoppingCartItems)
                    {
                        QuotationItemDetail item = new QuotationItemDetail
                        {
                            ItemDetails = direction == "ltr" ? shoppingCartItem.ItemNameEn : shoppingCartItem.ItemNameAr,
                            ItemQuantity = shoppingCartItem.Quantity,
                            UnitPrice = shoppingCartItem.UnitPrice,
                            TotalPrice = (shoppingCartItem.Quantity * shoppingCartItem.UnitPrice)
                        };
                        model.QuotationItemDetails.Add(item);
                    }
                    model.CustomerId = users.CustomerId ?? 0;
                    var customeres = CustomerService.GetAll();
                    ViewBag.Customers = customeres.Select(x => x.CreateFromServerToClient());
                    ViewBag.Orders =
                        OrdersService.GetOrdersByCustomerId(model.CustomerId).Select(x => x.CreateFromServerToClient());
                    ViewBag.ShowExcelImport = CheckInventoryModule() != true;
                    model.CreatedByName = createdByName;
                    model.CreatedByEmployee = users.EmployeeId ?? 0;
                    model.PageTitle = Quotation.CreateNew;
                    model.BtnText = Quotation.CreateQoute;

                    return View(model);
                }
            }
            QuotationCreateViewModel viewModel = new QuotationCreateViewModel();
            var customers = CustomerService.GetAll();
            ViewBag.Customers = customers.Select(x => x.CreateFromServerToClient());
            ViewBag.ShowExcelImport = CheckInventoryModule() != true;
            
            if (id == null)
            {
                viewModel.CreatedByName = createdByName;
                viewModel.CreatedByEmployee = users.EmployeeId ?? 0;
                viewModel.PageTitle = Quotation.CreateNew;
                viewModel.BtnText = Quotation.CreateQoute;
                return View(viewModel);
            }
            viewModel = QuotationService.FindQuotationById((long) id).CreateFromServerToClient();
            viewModel.CreatedByName = createdByName;
            viewModel.CreatedByEmployee = users.EmployeeId ?? 0;
            viewModel.PageTitle = Quotation.UpdateQuotation;
            viewModel.BtnText = Quotation.UpdateQuotation;
            viewModel.OldItemDetailsCount = viewModel.QuotationItemDetails.Count;
            ViewBag.Orders =
                OrdersService.GetOrdersByCustomerId(viewModel.CustomerId).Select(x => x.CreateFromServerToClient());
            return View(viewModel);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(QuotationCreateViewModel viewModel)
        {
            // Update case
            if (viewModel.QuotationId > 0)
            {
                viewModel.RecUpdatedBy = User.Identity.GetUserId();
                viewModel.RecUpdatedDt = DateTime.Now;
                var quotationToUpdate = viewModel.CreateFromClientToServer();
                if (QuotationService.UpdateQuotation(quotationToUpdate))
                {
                    List<bool> isUpdated = new List<bool>();
                    List<bool> isAdded = new List<bool>();
                    foreach (var itemDetail in viewModel.QuotationItemDetails)
                    {
                        if (itemDetail.ItemId > 0)
                        {
                            // Update item details
                            itemDetail.QuotationId = viewModel.QuotationId;
                            itemDetail.RecUpdatedBy = User.Identity.GetUserId();
                            itemDetail.RecUpdatedDt = DateTime.Now;
                            var itemDetailToUpdate = itemDetail.CreateFromClientToServer();
                            isUpdated.Add(QuotationItemService.UpdateQuotationItem(itemDetailToUpdate));
                        }
                        else
                        {
                            if (!string.IsNullOrEmpty(itemDetail.ItemDetails) && itemDetail.ItemQuantity != 0 &&
                                itemDetail.UnitPrice != 0)
                            {
                                // Add item details
                                itemDetail.QuotationId = viewModel.QuotationId;
                                itemDetail.RecCreatedBy = User.Identity.GetUserId();
                                itemDetail.RecCreatedDt = DateTime.Now;
                                var itemDetailToAdd = itemDetail.CreateFromClientToServer();
                                isAdded.Add(QuotationItemService.AddQuotationItem(itemDetailToAdd));
                            }
                            else if (string.IsNullOrEmpty(itemDetail.ItemDetails) && itemDetail.ItemQuantity == 0 &&
                                     itemDetail.UnitPrice == 0)
                            {
                                isAdded.Add(true);
                            }
                        }
                    }
                    if (!isUpdated.Contains(false) && !isAdded.Contains(false))
                    {
                        if (isUpdated.Count + isAdded.Count == viewModel.QuotationItemDetails.Count)
                        {
                            TempData["message"] = new MessageViewModel
                            {
                                Message = Quotation.UpdateMessage,
                                IsUpdated = true
                            };
                            return RedirectToAction("Index");
                        }
                    }
                }
                else
                {
                    var customers = CustomerService.GetAll();
                    ViewBag.Customers = customers.Select(x => x.CreateFromServerToClient());
                    ViewBag.Orders =
                        OrdersService.GetOrdersByCustomerId(viewModel.CustomerId)
                            .Select(x => x.CreateFromServerToClient());
                    TempData["message"] = new MessageViewModel
                    {
                        Message = Quotation.ErrorMessage,
                        IsUpdated = true
                    };
                    ViewBag.MessageVM = TempData["message"] as MessageViewModel;
                    return View(viewModel);
                }
            }
            // Add case
            // Save Quotation
            viewModel.RecCreatedBy = User.Identity.GetUserId();
            viewModel.RecCreatedDt = DateTime.Now;
            var quotationToAdd = viewModel.CreateFromClientToServer();
            var quotaionId = QuotationService.AddQuotation(quotationToAdd);
            bool addStatus = false;
            if (quotaionId > 0)
            {
                //Save Item Details
                foreach (var itemDetail in viewModel.QuotationItemDetails)
                {
                    bool isItemEDetailEmpty = !string.IsNullOrEmpty(itemDetail.ItemDetails) ||
                                              itemDetail.ItemQuantity == 0 || itemDetail.UnitPrice == 0;
                    if (!isItemEDetailEmpty)
                    {
                        itemDetail.QuotationId = quotaionId;
                        itemDetail.RecCreatedBy = User.Identity.GetUserId();
                        itemDetail.RecCreatedDt = DateTime.Now;
                        var itemDetailToAdd = itemDetail.CreateFromClientToServer();
                        addStatus = QuotationItemService.AddQuotationItem(itemDetailToAdd);
                    }
                    if (viewModel.QuotationItemDetails.Count > 0 && isItemEDetailEmpty)
                    {
                        addStatus = true;
                    }
                }
            }
            if (addStatus && quotaionId > 0)
            {
                TempData["message"] = new MessageViewModel
                {
                    Message = Quotation.AddMessage,
                    IsSaved = true
                };
                return RedirectToAction("Index");
            }
            // Error occur
            TempData["message"] = new MessageViewModel
            {
                Message = Quotation.ErrorMessage,
                IsError = true
            };
            return View(viewModel);
        }

        #endregion

        #region Get Customer Orders

        [HttpGet]
        public JsonResult GetCustomerOrders(long customerId)
        {
            var orders = OrdersService.GetOrdersByCustomerId(customerId).Select(x => x.CreateFromServerToClient());
            return Json(orders, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Detail for Customer

        [SiteAuthorize(PermissionKey = "QuotationDetail")]
        public ActionResult Detail(long? id)
        {
            QuotationDetailViewModel viewModel = new QuotationDetailViewModel();
            if (id != null)
            {
                viewModel.Profile = ProfileService.GetDetail().CreateFromServerToClientForQuotation();
                viewModel.Quotation = QuotationService.FindQuotationById((long) id).CreateFromServerToClientLv();
                // Get Order from Order Number
                viewModel.Order =
                    OrdersService.GetOrderByOrderId(viewModel.Quotation.OrderId).CreateFromServerToClient();
                ViewBag.LogoPath = ConfigurationManager.AppSettings["CompanyLogo"] + viewModel.Profile.CompanyLogoPath;
                return View(viewModel);
            }
            return View(viewModel);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Detail(QuotationDetailViewModel viewModel)
        {
            // Update Case
            Order order = OrdersService.GetOrderByOrderId(viewModel.Order.OrderId).CreateFromServerToClient();
            order.OrderStatus = viewModel.Order.OrderStatus;
            order.RecLastUpdatedBy = User.Identity.GetUserId();
            order.RecLastUpdatedDt = DateTime.Now;
            var orderToUpdate = order.CreateFromClientToServer();
            if (OrdersService.UpdateOrder(orderToUpdate))
            {
                if (viewModel.Order.OrderStatus == 3)
                {
                    TempData["message"] = new MessageViewModel
                    {
                        Message = EPMS.WebModels.Resources.CMS.Order.Canceled,
                        IsSaved = true
                    };
                }
                if (viewModel.Order.OrderStatus == 4)
                {
                    TempData["message"] = new MessageViewModel
                    {
                        Message = EPMS.WebModels.Resources.CMS.Order.Updated,
                        IsSaved = true
                    };
                }
                return RedirectToAction("Index", "Orders");
            }
            return null;
        }

        #endregion

        #region Delete Quotaion

        [SiteAuthorize(PermissionKey = "QuotationsDelete")]
        public ActionResult Delete(int itemDetailId)
        {
            var itemDetailToBeDeleted = QuotationItemService.FindQuotationById(itemDetailId);
            try
            {
                QuotationItemService.DeleteQuotationItem(itemDetailToBeDeleted);
                return Json(new
                {
                    Status = "Success"
                }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception exp)
            {
                return Json(
                    new
                    {
                        Status = "Error"
                    }, JsonRequestBehavior.AllowGet);
            }
        }

        #endregion

        #region CheckInventoryModule

        bool CheckInventoryModule()
        {
            string licenseKeyEncrypted = ConfigurationManager.AppSettings["LicenseKey"].ToString(CultureInfo.InvariantCulture);
            string licenseKey = EncryptDecrypt.StringCipher.Decrypt(licenseKeyEncrypted, "123"); //DesertStarts
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

        #endregion
    }
}