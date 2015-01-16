﻿using System;
using System.Linq;
using System.Web.Mvc;
using EPMS.Implementation.Services;
using EPMS.Interfaces.IServices;
using EPMS.Models.RequestModels;
using EPMS.Web.ModelMappers;
using EPMS.Web.ViewModels.Common;
using EPMS.Web.ViewModels.Quotation;
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
        public ActionResult Create(QuotationCreateViewModel model)
        {
            // Update case
            if (model.QuotationId > 0)
            {
                return RedirectToAction("Index");
            }
            // Add case
            // Save Quotation
            model.RecCreatedBy = User.Identity.GetUserId();
            model.RecCreatedDt = DateTime.Now;
            var quotationToAdd = model.CreateFromClientToServer();
            var quotaionId = QuotationService.AddQuotation(quotationToAdd);
            var addStatus = false;
            if (quotaionId > 0)
            {
                //Save Item Details
                foreach (var itemDetail in model.QuotationItemDetails)
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
                IsSaved = true
            };
            return View(model);
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