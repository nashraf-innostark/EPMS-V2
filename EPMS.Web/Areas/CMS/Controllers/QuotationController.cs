using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using EPMS.Implementation.Services;
using EPMS.Interfaces.IServices;
using EPMS.Models.RequestModels;
using EPMS.Web.ModelMappers;
using EPMS.Web.ViewModels.Common;
using EPMS.Web.ViewModels.Quotation;
using EPMS.WebBase.Mvc;
using Microsoft.AspNet.Identity;

namespace EPMS.Web.Areas.CMS.Controllers
{
    [Authorize]
    public class QuotationController : Controller
    {
        #region Private
        private readonly ICustomerService CustomerService;
        private readonly IAspNetUserService AspNetUserService;
        private readonly IOrdersService OrdersService;
        private readonly IQuotationService QuotationService;
        private readonly IQuotationItemService QuotationItemService;
        #endregion

        #region Constructor

        public QuotationController(ICustomerService customerService, IAspNetUserService aspNetUserService, IOrdersService ordersService, IQuotationService quotationService, IQuotationItemService quotationItemService)
        {
            CustomerService = customerService;
            AspNetUserService = aspNetUserService;
            OrdersService = ordersService;
            QuotationService = quotationService;
            QuotationItemService = quotationItemService;
        }

        #endregion

        #region Public
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
            var quotationList = QuotationService.GetAllQuotation(searchRequest);
            viewModel.aaData = quotationList.Quotations.Select(x => x.CreateFromServerToClientLv());
            viewModel.iTotalRecords = quotationList.TotalCount;
            viewModel.iTotalDisplayRecords = quotationList.TotalCount;
            return Json(viewModel, JsonRequestBehavior.AllowGet);
        }

        [SiteAuthorize(PermissionKey = "QuotationsCreate")]
        public ActionResult Create(long? id)
        {
            QuotationCreateViewModel viewModel = new QuotationCreateViewModel();
            var customers = CustomerService.GetAll();
            ViewBag.Customers = customers.Select(x => x.CreateFromServerToClient());
            var users = AspNetUserService.FindById(User.Identity.GetUserId());
            var direction = Resources.Shared.Common.TextDirection;
            var createdByName = "";
            if (users.Employee != null && direction == "ltr")
            {
                createdByName = users.Employee.EmployeeNameE;
            }
            if (users.Employee != null && direction == "rtl")
            {
                createdByName = users.Employee.EmployeeNameA;
            }
            if (id == null)
            {
                viewModel.CreatedByName = createdByName;
                viewModel.CreatedByEmployee = users.EmployeeId ?? 0;
                return View(viewModel);
            }
            viewModel = QuotationService.FindQuotationById((long)id).CreateFromServerToClient();
            viewModel.CreatedByName = createdByName;
            viewModel.CreatedByEmployee = users.EmployeeId ?? 0;
            viewModel.OldItemDetailsCount = viewModel.QuotationItemDetails.Count;
            ViewBag.Orders = OrdersService.GetOrdersByCustomerId(viewModel.CustomerId).Select(x => x.CreateFromServerToClient());
            return View(viewModel);
        }

        [HttpPost]
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
                            // Add item details
                            itemDetail.QuotationId = viewModel.QuotationId;
                            itemDetail.RecCreatedBy = User.Identity.GetUserId();
                            itemDetail.RecCreatedDt = DateTime.Now;
                            var itemDetailToAdd = itemDetail.CreateFromClientToServer();
                            isAdded.Add(QuotationItemService.AddQuotationItem(itemDetailToAdd));
                        }
                    }
                    if (isUpdated.Count + isAdded.Count == viewModel.QuotationItemDetails.Count)
                    {
                        TempData["message"] = new MessageViewModel
                        {
                            Message = Resources.CMS.Quotation.UpdateMessage,
                            IsUpdated = true
                        };
                        return RedirectToAction("Index");
                    }
                }
                else
                {
                    TempData["message"] = new MessageViewModel
                    {
                        Message = Resources.CMS.Quotation.UpdateMessage,
                        IsUpdated = true
                    };
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
                    itemDetail.QuotationId = quotaionId;
                    itemDetail.RecCreatedBy = User.Identity.GetUserId();
                    itemDetail.RecCreatedDt = DateTime.Now;
                    var itemDetailToAdd = itemDetail.CreateFromClientToServer();
                    addStatus = QuotationItemService.AddQuotationItem(itemDetailToAdd);
                }
            }
            if (addStatus && quotaionId > 0)
            {
                TempData["message"] = new MessageViewModel
                {
                    Message = Resources.CMS.Quotation.AddMessage,
                    IsSaved = true
                };
                return RedirectToAction("Index");
            }
            // Error occur
            TempData["message"] = new MessageViewModel
            {
                Message = Resources.CMS.Quotation.ErrorMessage,
                IsError = true
            };
            return View(viewModel);
        }

        [HttpGet]
        public JsonResult GetCustomerOrders(long customerId)
        {
            var orders = OrdersService.GetOrdersByCustomerId(customerId).Select(x => x.CreateFromServerToClient());
            return Json(orders, JsonRequestBehavior.AllowGet);
        }
        #endregion
    }
}