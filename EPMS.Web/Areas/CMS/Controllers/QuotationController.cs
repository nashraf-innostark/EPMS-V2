using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;
using EPMS.Interfaces.IServices;
using EPMS.Models.RequestModels;
using EPMS.Models.ResponseModels;
using EPMS.Web.EncryptDecrypt;
using EPMS.WebModels.ModelMappers;
using EPMS.WebModels.ModelMappers.Website.ShoppingCart;
using EPMS.WebModels.ViewModels.Quotation;
using EPMS.Web.Controllers;
using EPMS.WebModels.ViewModels.Common;
using EPMS.WebBase.Mvc;
using EPMS.WebModels.ViewModels.RFQ;
using EPMS.WebModels.WebsiteModels;
using EPMS.WebModels.WebsiteModels.Common;
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
        private readonly IRFQService rfqService;
        private readonly IQuotationItemService QuotationItemService;
        private readonly ICompanyProfileService ProfileService;
        private readonly IShoppingCartService cartService;
        private readonly ICompanyProfileService companyProfileService;
        #endregion

        #region Constructor

        public QuotationController(ICustomerService customerService, IAspNetUserService aspNetUserService, IOrdersService ordersService, IQuotationService quotationService, IQuotationItemService quotationItemService, ICompanyProfileService profileService, IShoppingCartService cartService, IRFQService rfqService, ICompanyProfileService companyProfileService)
        {
            CustomerService = customerService;
            AspNetUserService = aspNetUserService;
            OrdersService = ordersService;
            QuotationService = quotationService;
            QuotationItemService = quotationItemService;
            ProfileService = profileService;
            this.cartService = cartService;
            this.rfqService = rfqService;
            this.companyProfileService = companyProfileService;
        }

        #endregion

        #region Public

        #region ListView Quotation

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
            searchRequest.RoleName = (string) Session["RoleName"];
            string roleName = (string) Session["RoleName"];
            string[] userPermissionsSet = (string[])Session["UserPermissionSet"];
            //switch (searchRequest.RoleName)
            //{
            //    case "Customer":
            //        searchRequest.CustomerId = (long) Session["CustomerID"];
            //        break;
            //    case "Employee":
            //        searchRequest.EmployeeId = Session["UserID"].ToString();
            //        break;
            //    default:
            //        searchRequest.CustomerId = 0;
            //        break;
            //}
            
            if (searchRequest.RoleName == "Customer")
            {
                searchRequest.AllowedAll = false;
                searchRequest.CustomerId = (long)Session["CustomerID"];
            }
            else
            {
                if (userPermissionsSet.Contains("ListviewAllQuotations"))
                {
                    searchRequest.AllowedAll = true;
                }
                else
                {
                    searchRequest.AllowedAll = false;
                    searchRequest.UserId = Session["UserID"].ToString();
                }
            }
            var quotationList = QuotationService.GetAllQuotation(searchRequest);
            viewModel.aaData = quotationList.Quotations.Select(x => x.CreateFromServerToClientLv()).OrderBy(x=>x.QuotationId);
            viewModel.iTotalRecords = quotationList.TotalCount;
            viewModel.iTotalDisplayRecords = quotationList.TotalCount;
            return Json(viewModel, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region ListView RFQ
        [SiteAuthorize(PermissionKey = "RFQIndex")]
        public ActionResult RFQIndex()
        {
            RFQListViewModel viewModel = new RFQListViewModel
            {
                Rfqs = rfqService.GetAllRfqs().Select(x => x.CreateFromServerToClient()),
            };
            ViewBag.MessageVM = TempData["message"] as MessageViewModel;
            return View(viewModel);
        }
        #endregion

        #region Add/Update Quotation

        [SiteAuthorize(PermissionKey = "QuotationsCreate")]
        public ActionResult Create(long? id, string from, long? rfqId)
        {
            var direction = WebModels.Resources.Shared.Common.TextDirection;
            long quotationId = 0;
            if (id != null)
            {
                quotationId = (long)id;
            }
            string createdByName = GetCreatedBy(direction);
            long employeeId = Session["EmployeeID"] != null ? Convert.ToInt64(Session["EmployeeID"]) : 0;
            if (from == "Client")
            {
                QuotationCreateViewModel model = new QuotationCreateViewModel
                {
                    CustomerId = Session["CustomerID"] != null ? Convert.ToInt64(Session["CustomerID"]) : 0
                };
                QuotationResponse response = QuotationService.GetQuotationResponseForRfq(model.CustomerId, (long)rfqId);
                
                if (response.Rfq != null)
                {
                    model.QuotationItemDetails = new List<QuotationItemDetail>();
                    var rfqItems = response.Rfq.RFQItems.Select(x=>x.CreateFromServerToClient());
                    foreach (var rfqItem in rfqItems)
                    {
                        QuotationItemDetail item = new QuotationItemDetail
                        {
                            ItemDetails = rfqItem.ItemDetails,
                            ItemQuantity = rfqItem.ItemQuantity,
                            UnitPrice = rfqItem.UnitPrice,
                            TotalPrice = (rfqItem.ItemQuantity * rfqItem.UnitPrice)
                        };
                        model.QuotationItemDetails.Add(item);
                    }
                    model.QuotationDiscount = response.Rfq.Discount;
                    ViewBag.Customers = response.Customers.Any() ?
                        response.Customers.Select(x => x.CreateFromServerToClient()) : new List<Customer>();
                    ViewBag.Orders = new List<Order>();
                    ViewBag.ShowExcelImport = CheckInventoryModule() != true;
                    model.CreatedByName = createdByName;
                    model.CreatedByEmployee = employeeId;
                    model.PageTitle = Quotation.CreateNew;
                    model.BtnText = Quotation.CreateQoute;
                    ViewBag.FromClient = true;
                }
                return View(model);
            }
            QuotationCreateViewModel viewModel = new QuotationCreateViewModel();
            QuotationResponse quotResponse = QuotationService.GetQuotationResponse(quotationId, 0, from);
            ViewBag.Customers = quotResponse.Customers.Any() ?
                        quotResponse.Customers.Select(x => x.CreateFromServerToClient()) : new List<Customer>();
            ViewBag.ShowExcelImport = CheckInventoryModule() != true;
            ViewBag.FromClient = false;
            if (id == null)
            {
                viewModel.CreatedByName = createdByName;
                viewModel.CreatedByEmployee = employeeId;
                viewModel.PageTitle = Quotation.CreateNew;
                viewModel.BtnText = Quotation.CreateQoute;
                return View(viewModel);
            }
            viewModel = quotResponse.Quotation.CreateFromServerToClient();
            viewModel.CreatedByName = createdByName;
            viewModel.CreatedByEmployee = employeeId;
            viewModel.PageTitle = Quotation.UpdateQuotation;
            viewModel.BtnText = Quotation.UpdateQuotation;
            viewModel.OldItemDetailsCount = viewModel.QuotationItemDetails.Count;
            ViewBag.Orders = quotResponse.Orders.Any() ?
                        quotResponse.Orders.Select(x => x.CreateFromServerToClient()) : new List<Order>();
            return View(viewModel);
        }

        [HttpPost]
        [ValidateInput(false)]
        [SiteAuthorize(PermissionKey = "QuotationsCreate")]
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
            viewModel.RecUpdatedBy = User.Identity.GetUserId();
            viewModel.RecUpdatedDt = DateTime.Now;
            var quotationToAdd = viewModel.CreateFromClientToServer();
            var quotaionId = QuotationService.AddQuotation(quotationToAdd);
            //bool addStatus = false;
            //if (quotaionId > 0)
            //{
            //    //Save Item Details
            //    foreach (var itemDetail in viewModel.QuotationItemDetails)
            //    {
            //        bool isItemEDetailEmpty = !string.IsNullOrEmpty(itemDetail.ItemDetails) ||
            //                                  itemDetail.ItemQuantity == 0 || itemDetail.UnitPrice == 0;
            //        if (!isItemEDetailEmpty)
            //        {
            //            itemDetail.QuotationId = quotaionId;
            //            itemDetail.RecCreatedBy = User.Identity.GetUserId();
            //            itemDetail.RecCreatedDt = DateTime.Now;
            //            var itemDetailToAdd = itemDetail.CreateFromClientToServer();
            //            addStatus = QuotationItemService.AddQuotationItem(itemDetailToAdd);
            //        }
            //        if (viewModel.QuotationItemDetails.Count > 0 && isItemEDetailEmpty)
            //        {
            //            addStatus = true;
            //        }
            //    }
            //}
            //if (addStatus && quotaionId > 0)
            if (quotaionId > 0)
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

        #region Add/Update RFQ
        [SiteAuthorize(PermissionKey = "RFQCreate")]
        public ActionResult RFQ(long? id, string from)
        {
            var direction = WebModels.Resources.Shared.Common.TextDirection;
            long customerId = Session["CustomerID"] != null ? Convert.ToInt64(Session["CustomerID"]) : 0;
            RFQCreateViewModel model = new RFQCreateViewModel();
            if (from == "Client")
            {
                model.FromClient = true;
            }
            if (id != null)
            {
                long quotationId = (long)id;
                RFQResponse rfqResponse = rfqService.GetRfqResponse(quotationId, customerId, from);
                if (rfqResponse.Rfq != null)
                {
                    model.Rfq = rfqResponse.Rfq.CreateFromServerToClient();
                }
                model.Rfq.CustomerId = customerId;
                return View(model);
            }
            model.Rfq.CustomerId = customerId;
            if (from == "Client")
            {
                string userId = User.Identity.GetUserId();
                var response = cartService.FindByUserCartId(userId);
                if (response != null)
                {
                    var shoppingCart = response.CreateFromServerToClient();
                    foreach (var shoppingCartItem in shoppingCart.ShoppingCartItems)
                    {
                        RFQItem item = new RFQItem
                        {
                            ItemDetails = direction == "ltr" ? shoppingCartItem.ItemNameEn : shoppingCartItem.ItemNameAr,
                            ItemQuantity = shoppingCartItem.Quantity,
                            UnitPrice = shoppingCartItem.UnitPrice,
                            TotalPrice = (shoppingCartItem.Quantity * shoppingCartItem.UnitPrice)
                        };
                        model.Rfq.RFQItems.Add(item);
                    }
                }
            }
            return View(model);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult RFQ(RFQCreateViewModel model)
        {
            if (model.FromClient)
            {
                if (model.Rfq.RFQId > 0)
                {
                    model.Rfq.RecLastUpdatedBy = User.Identity.GetUserId();
                    model.Rfq.RecLastUpdatedDate = DateTime.Now;
                    foreach (var rfqItem in model.Rfq.RFQItems)
                    {
                        rfqItem.RecLastUpdatedBy = User.Identity.GetUserId();
                        rfqItem.RecLastUpdatedDate = DateTime.Now;
                    }
                    model.Rfq.Status = (int)RFQStatus.QoutationCreated;
                    var rfqToUpdate = model.Rfq.CreateFromClientToServer();
                    if (rfqToUpdate.CustomerId == 0)
                    {
                        rfqToUpdate.CustomerId = null;
                    }
                    if (rfqService.UpdateRfq(rfqToUpdate))
                    {
                        TempData["message"] = new MessageViewModel
                        {
                            Message = WebModels.Resources.CMS.RFQ.UpdateMessage,
                            IsUpdated = true
                        };
                        return RedirectToAction("RFQIndex");
                    }
                }
                else
                {
                    model.Rfq.RecCreatedBy = User.Identity.GetUserId();
                    model.Rfq.RecCreatedDate = DateTime.Now;
                    model.Rfq.RecLastUpdatedBy = User.Identity.GetUserId();
                    model.Rfq.RecLastUpdatedDate = DateTime.Now;
                    foreach (var rfqItem in model.Rfq.RFQItems)
                    {
                        rfqItem.RecCreatedBy = User.Identity.GetUserId();
                        rfqItem.RecCreatedDate = DateTime.Now;
                        rfqItem.RecLastUpdatedBy = User.Identity.GetUserId();
                        rfqItem.RecLastUpdatedDate = DateTime.Now;
                    }
                    model.Rfq.Status = (int)RFQStatus.QoutationCreated;
                    var rfqToAdd = model.Rfq.CreateFromClientToServer();
                    if (rfqToAdd.CustomerId == 0)
                    {
                        rfqToAdd.CustomerId = null;
                    }
                    if (rfqService.AddRfq(rfqToAdd))
                    {
                        TempData["message"] = new MessageViewModel
                        {
                            Message = WebModels.Resources.CMS.RFQ.AddMessage,
                            IsUpdated = true
                        };
                        return RedirectToAction("RFQIndex");
                    }
                }
            }
            else
            {
                if (model.Rfq.RFQId > 0)
                {
                    model.Rfq.RecLastUpdatedBy = User.Identity.GetUserId();
                    model.Rfq.RecLastUpdatedDate = DateTime.Now;
                    model.Rfq.Status = (int)RFQStatus.QoutationCreated;
                    var rfqToUpdate = model.Rfq.CreateFromClientToServer();
                    if (rfqToUpdate.CustomerId == 0)
                    {
                        rfqToUpdate.CustomerId = null;
                    }
                    if (rfqService.UpdateRfq(rfqToUpdate))
                    {
                        TempData["message"] = new MessageViewModel
                        {
                            Message = WebModels.Resources.CMS.RFQ.UpdateMessage,
                            IsUpdated = true
                        };
                        return RedirectToAction("RFQIndex");
                    }
                }
                else
                {
                    model.Rfq.RecCreatedBy = User.Identity.GetUserId();
                    model.Rfq.RecCreatedDate = DateTime.Now;
                    model.Rfq.RecLastUpdatedBy = User.Identity.GetUserId();
                    model.Rfq.RecLastUpdatedDate = DateTime.Now;
                    model.Rfq.Status = (int)RFQStatus.QoutationCreated;
                    var rfqToAdd = model.Rfq.CreateFromClientToServer();
                    if (rfqToAdd.CustomerId == 0)
                    {
                        rfqToAdd.CustomerId = null;
                    }
                    if (rfqService.AddRfq(rfqToAdd))
                    {
                        TempData["message"] = new MessageViewModel
                        {
                            Message = WebModels.Resources.CMS.RFQ.AddMessage,
                            IsUpdated = true
                        };
                        return RedirectToAction("RFQIndex");
                    }
                }
            }
            return View(model);
        }

        #endregion

        #region RFQ Details

        public ActionResult RFQDetail(long? id, string from)
        {
            var direction = WebModels.Resources.Shared.Common.TextDirection;
            long customerId = Session["CustomerID"] != null ? Convert.ToInt64(Session["CustomerID"]) : 0;
            RFQDetailViewModel model = new RFQDetailViewModel();
            long quotationId = 0;
            if (id != null)
            {
                quotationId = (long)id;
                RFQResponse rfqResponse = rfqService.GetRfqResponse(quotationId, customerId, from);
                if (rfqResponse.Rfq != null)
                {
                    model.Rfq = rfqResponse.Rfq.CreateFromServerToClient();
                }
                model.Profile = rfqResponse.Profile.CreateFromServerToClientForQuotation();
                model.Rfq.CustomerId = customerId;
                ViewBag.LogoPath = ConfigurationManager.AppSettings["CompanyLogo"] + model.Profile.CompanyLogoPath;
                return View(model);
            }
            model.Rfq.CustomerId = customerId;
            if (from == "Client")
            {
                string userId = User.Identity.GetUserId();
                var response = cartService.FindByUserCartId(userId);
                if (response != null)
                {
                    var shoppingCart = response.CreateFromServerToClient();
                    foreach (var shoppingCartItem in shoppingCart.ShoppingCartItems)
                    {
                        RFQItem item = new RFQItem
                        {
                            ItemDetails = direction == "ltr" ? shoppingCartItem.ItemNameEn : shoppingCartItem.ItemNameAr,
                            ItemQuantity = shoppingCartItem.Quantity,
                            UnitPrice = shoppingCartItem.UnitPrice,
                            TotalPrice = (shoppingCartItem.Quantity * shoppingCartItem.UnitPrice)
                        };
                        model.Rfq.RFQItems.Add(item);
                    }
                    model.Profile = companyProfileService.GetDetail().CreateFromServerToClientForQuotation();
                    model.Rfq.CustomerId = customerId;
                    ViewBag.LogoPath = ConfigurationManager.AppSettings["CompanyLogo"] + model.Profile.CompanyLogoPath;
                }
            }
            return View(model);
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
                viewModel.Quotation = QuotationService.FindQuotationById((long)id).CreateFromServerToClientLv();
                // Get Order from Order Number
                if (viewModel.Quotation.OrderId>0)
                {
                    viewModel.Order =
                    OrdersService.GetOrderByOrderId(viewModel.Quotation.OrderId).CreateFromServerToClient();
                }
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
                        Message = WebModels.Resources.CMS.Order.Canceled,
                        IsSaved = true
                    };
                }
                if (viewModel.Order.OrderStatus == 4)
                {
                    TempData["message"] = new MessageViewModel
                    {
                        Message = WebModels.Resources.CMS.Order.Updated,
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

        #region Delete RFQ Item

        [SiteAuthorize(PermissionKey = "RFQDelete")]
        public ActionResult DeleteRfqItem(long itemId)
        {
            try
            {
                rfqService.DeleteRfqItem(itemId);
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

        #region functions

        public string GetCreatedBy(string direction)
        {
            string createdBy = "";
            if (direction == "ltr" && Session["UserFullName"] != null)
            {
                createdBy = Session["UserFullName"].ToString();
            }
            if (direction == "rtl" && Session["UserFullNameA"] != null)
            {
                createdBy = Session["UserFullNameA"].ToString();
            }
            return createdBy;
        }
        #endregion

        #endregion
    }
}