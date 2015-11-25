using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;
using EPMS.Interfaces.IServices;
using EPMS.Models.Common;
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

        private readonly IOrdersService OrdersService;
        private readonly IQuotationService QuotationService;
        private readonly IRFQService rfqService;
        private readonly IQuotationItemService QuotationItemService;
        private readonly ICompanyProfileService ProfileService;
        private readonly IShoppingCartService cartService;
        private readonly ICompanyProfileService companyProfileService;

        private bool CheckHasInventoryModule()
        {
            // check license
            var licenseKeyEncrypted = ConfigurationManager.AppSettings["LicenseKey"].ToString(CultureInfo.InvariantCulture);
            string licenseKey = StringCipher.Decrypt(licenseKeyEncrypted, "123");
            var splitLicenseKey = licenseKey.Split('|');
            string[] modules = splitLicenseKey[4].Split(';');
            if (modules.Contains("IS") || modules.Contains("Inventory System") || modules.Contains("نظام المخزون"))
            {
                return true;
            }
            return false;
        }
        #endregion

        #region Constructor

        public QuotationController(IOrdersService ordersService, IQuotationService quotationService, IQuotationItemService quotationItemService, ICompanyProfileService profileService, IShoppingCartService cartService, IRFQService rfqService, ICompanyProfileService companyProfileService)
        {
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
                SearchRequest = new QuotationSearchRequest
                {
                    RoleName = (string)Session["RoleName"]
                },
            };
            ViewBag.RoleName = viewModel.SearchRequest.RoleName;
            ViewBag.MessageVM = TempData["message"] as MessageViewModel;
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Index(QuotationSearchRequest searchRequest)
        {
            QuotationListViewModel viewModel = new QuotationListViewModel();
            searchRequest.RoleName = (string)Session["RoleName"];
            string[] userPermissionsSet = (string[])Session["UserPermissionSet"];
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
            viewModel.aaData = quotationList.Quotations.Select(x => x.CreateFromServerToClientLv()).OrderBy(x => x.QuotationId);
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
            string createdByName = GetCreatedBy(direction, null);
            if (from == "Client")
            {
                QuotationCreateViewModel model = new QuotationCreateViewModel();
                QuotationResponse response = QuotationService.GetRfqForQuotationResponse((long)rfqId);

                if (response.Rfq != null)
                {
                    model.QuotationItemDetails = new List<QuotationItemDetail>();
                    var rfqItems = response.Rfq.RFQItems.Select(x => x.CreateFromServerToClient());
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
                    model.CustomerId = response.Rfq.CustomerId ?? 0;
                    model.RFQId = response.Rfq.RFQId;
                    model.QuotationDiscount = response.Rfq.Discount;
                    ViewBag.Customers = response.Customers.Any() ?
                        response.Customers.Select(x => x.CreateForDropDown()) : new List<CustomerDropDown>();
                    ViewBag.Rfqs = response.Rfqs.Any() ?
                        response.Rfqs.Select(x => x.CreateForDropDown()) : new List<RfqDropDown>();
                    ViewBag.ShowExcelImport = CheckInventoryModule() != true;
                    model.CreatedByName = createdByName;
                    ViewBag.FromClient = true;
                }
                model.ItemVariationDropDownList = response.ItemVariationDropDownList;
                return View(model);
            }
            QuotationCreateViewModel viewModel = new QuotationCreateViewModel();
            QuotationResponse quotResponse = QuotationService.GetQuotationResponse(quotationId, 0, from);
            ViewBag.Customers = quotResponse.Customers.Any() ?
                        quotResponse.Customers.Select(x => x.CreateForDropDown()) : new List<CustomerDropDown>();
            ViewBag.IsIncludeNewJsTree = true;
            ViewBag.ShowExcelImport = CheckInventoryModule() != true;
            ViewBag.FromClient = false;
            if (id == null)
            {
                viewModel.ItemVariationDropDownList = quotResponse.ItemVariationDropDownList;
                viewModel.CreatedByName = createdByName;
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

        [HttpPost]
        [ValidateInput(false)]
        [SiteAuthorize(PermissionKey = "QuotationsCreate")]
        public ActionResult Create(QuotationCreateViewModel viewModel)
        {
            QuotationResponse response = new QuotationResponse();
            // Update case
            if (viewModel.QuotationId > 0)
            {
                viewModel.RecLastUpdatedBy = User.Identity.GetUserId();
                viewModel.RecLastUpdatedDate = DateTime.Now;
                foreach (var detail in viewModel.QuotationItemDetails)
                {
                    if (detail.ItemId == 0)
                    {
                        detail.QuotationId = viewModel.QuotationId;
                        detail.RecCreatedBy = User.Identity.GetUserId();
                        detail.RecCreatedDate = DateTime.Now;
                    }
                    detail.RecLastUpdatedBy = User.Identity.GetUserId();
                    detail.RecLastUpdatedDate = DateTime.Now;
                }
                var quotationToUpdate = viewModel.CreateFromClientToServer();
                response = QuotationService.UpdateQuotation(quotationToUpdate);
                if (response.Status)
                {
                    TempData["message"] = new MessageViewModel
                    {
                        Message = Quotation.UpdateMessage,
                        IsUpdated = true
                    };
                    return RedirectToAction("Index");
                }
            }
            else
            {
                // Add case
                // Save Quotation
                viewModel.RecCreatedBy = User.Identity.GetUserId();
                viewModel.RecCreatedDate = DateTime.Now;
                viewModel.RecLastUpdatedBy = User.Identity.GetUserId();
                viewModel.RecLastUpdatedDate = DateTime.Now;
                foreach (var detail in viewModel.QuotationItemDetails)
                {
                    detail.RecCreatedBy = User.Identity.GetUserId();
                    detail.RecCreatedDate = DateTime.Now;
                    detail.RecLastUpdatedBy = User.Identity.GetUserId();
                    detail.RecLastUpdatedDate = DateTime.Now;
                }
                var quotationToAdd = viewModel.CreateFromClientToServer();
                response = QuotationService.AddQuotation(quotationToAdd);
                if (response.Status)
                {
                    TempData["message"] = new MessageViewModel
                    {
                        Message = Quotation.AddMessage,
                        IsSaved = true
                    };
                    return RedirectToAction("Index");
                }
            }

            // Error occur
            ViewBag.Customers = response.Customers.Any() ?
                        response.Customers.Select(x => x.CreateForDropDown()) : new List<CustomerDropDown>();
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
            ViewBag.HasInventoryModule = CheckHasInventoryModule();
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
                    model.Rfq.Status = (int)RFQStatus.Pending;
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
                    model.Rfq.Status = (int)RFQStatus.Pending;
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
                model.Profile = rfqResponse.Profile != null ? rfqResponse.Profile.CreateFromServerToClientForQuotation() : new CompanyProfile();
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
                    var cp = companyProfileService.GetDetail();
                    model.Profile = cp != null ? cp.CreateFromServerToClientForQuotation() : new CompanyProfile();
                    model.Rfq.CustomerId = customerId;
                    ViewBag.LogoPath = ConfigurationManager.AppSettings["CompanyLogo"] + model.Profile.CompanyLogoPath;
                }
            }
            return View(model);
        }

        #endregion

        #region Get Customer RFQs

        [HttpGet]
        public JsonResult GetCustomerRfqs(long customerId)
        {
            var rfqs = rfqService.GetPendingRfqsByCustomerId(customerId).ToList();
            RfqRfqItemsForQuotation response = new RfqRfqItemsForQuotation
            {
                Rfqs = rfqs.Select(x => x.CreateForDropDown()).ToList(),
                RfqItems = rfqs.SelectMany(x => x.RFQItems.Select(y => y.CreateFromServerToClient())).ToList()
            };
            return Json(response, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Detail for Customer

        [SiteAuthorize(PermissionKey = "QuotationDetail")]
        public ActionResult Detail(long? id)
        {
            var direction = WebModels.Resources.Shared.Common.TextDirection;
            QuotationDetailViewModel viewModel = new QuotationDetailViewModel();
            if (id != null)
            {
                var cp = ProfileService.GetDetail();
                viewModel.Profile = cp != null ? cp.CreateFromServerToClientForQuotation() : new CompanyProfile();
                var quotation = QuotationService.FindQuotationById((long) id);
                viewModel.Quotation = quotation.CreateFromServerToClientLv();
                viewModel.EmployeeName = GetCreatedBy(direction, quotation);
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
            Order order = new Order
            {
                CustomerId = viewModel.Quotation.CustomerId
            };
            var orders = OrdersService.GetAll().OrderBy(x => x.RecCreatedDt).ToList();
            if (Request.Form["PlaceOrder"] != null)
            {
                order.OrderNo = Utility.GetOrderNumber(orders);
            }
            if (Request.Form["CancelOrder"] != null)
            {
            }
            //Order order = OrdersService.GetOrderByOrderId(viewModel.Order.OrderId).CreateFromServerToClient();
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

        #region Functions

        public string GetCreatedBy(string direction, EPMS.Models.DomainModels.Quotation quotation)
        {
            string createdBy = "";
            if (quotation != null)
            {
                var emp = quotation.AspNetUser.Employee;
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