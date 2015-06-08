using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using EPMS.Interfaces.IServices;
using EPMS.Models.RequestModels;
using EPMS.Models.ResponseModels;
using EPMS.Web.Controllers;
using EPMS.Web.ModelMappers;
using EPMS.Web.ModelMappers.Inventory.RFI;
using EPMS.Web.Models;
using EPMS.Web.ViewModels.Common;
using EPMS.Web.ViewModels.IRF;
using EPMS.WebBase.Mvc;
using Microsoft.AspNet.Identity;
using Employee = EPMS.Web.Resources.HR.Employee;

namespace EPMS.Web.Areas.Inventory.Controllers
{
    public class ItemReleaseController : BaseController
    {
        #region Private

        private readonly IItemReleaseService itemReleaseService;
        private readonly IRFIService RfiService;
        private readonly IOrdersService ordersService;
        private readonly IAspNetUserService userService;

        #endregion

        #region Constructor
        public ItemReleaseController(IItemReleaseService itemReleaseService, IRFIService rfiService, IOrdersService ordersService, IAspNetUserService userService)
        {
            this.itemReleaseService = itemReleaseService;
            RfiService = rfiService;
            this.ordersService = ordersService;
            this.userService = userService;
        }

        #endregion

        #region Public
        // GET: Inventory/IRF
        [SiteAuthorize(PermissionKey = "ItemReleaseIndex")]
        public ActionResult Index()
        {
            ViewBag.UserRole = Session["RoleName"].ToString().ToLower();
            ItemReleaseFormListViewModel viewModel = new ItemReleaseFormListViewModel
            {
                SearchRequest = new ItemReleaseSearchRequest()
            };
            ViewBag.MessageVM = TempData["message"] as MessageViewModel;
            return View(viewModel);
        }
        /// <summary>
        /// For data table DB paging
        /// </summary>
        [HttpPost]
        public ActionResult Index(ItemReleaseSearchRequest searchRequest)
        {
            searchRequest.SearchString = Request["search"];
            searchRequest.Requester = Session["RoleName"].ToString().ToLower();
            ItemReleaseResponse response = itemReleaseService.GetAllItemRelease(searchRequest);
            IEnumerable<Models.ItemRelease> itemReleaseList =
                response.ItemReleases.Select(x => x.CreateFromServerToClient());
            ItemReleaseFormListViewModel viewModel = new ItemReleaseFormListViewModel()
            {
                aaData = itemReleaseList,
                iTotalRecords = Convert.ToInt32(response.TotalRecords),
                iTotalDisplayRecords = Convert.ToInt32(response.TotalDisplayRecords),
                sEcho = searchRequest.sEcho
            };
            return Json(viewModel, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// GET: Inventory/IRF
        /// </summary>
        [SiteAuthorize(PermissionKey = "ItemReleaseCreate,ItemReleaseDetail")]
        public ActionResult Create(long? id)
        {
            ItemReleaseFormCreateViewModel viewModel = new ItemReleaseFormCreateViewModel();
            IRFCreateResponse response;
            if (id != null)
            {
                response = itemReleaseService.GetCreateResponse((long) id);
                viewModel.ItemRelease = response.ItemRelease.CreateFromServerToClient();
                viewModel.ItemReleaseDetails = response.ItemRelease.ItemReleaseDetails.Select(x=>x.CreateFromServerToClient()).ToList();
                viewModel.Rfis = response.Rfis.Select(x => x.CreateRfiServerToClientForDropdown()).ToList();
            }
            else
            {
                response = itemReleaseService.GetCreateResponse(0);
                viewModel.ItemRelease = new ItemRelease();
                viewModel.ItemReleaseDetails = new List<ItemReleaseDetail>();
                if (Session["RoleName"] != null)
                {
                    if (Session["RoleName"].ToString() != "Admin")
                    {
                        var employee = userService.FindById(Session["UserID"].ToString()).Employee;
                        var direction = Resources.Shared.Common.TextDirection;
                        if (direction == "ltr")
                        {
                            viewModel.ItemRelease.CreatedBy = employee.EmployeeFirstNameE + " " + employee.EmployeeMiddleNameE + " " + employee.EmployeeLastNameE;
                        }
                        else
                        {
                            viewModel.ItemRelease.CreatedBy = employee.EmployeeFirstNameA + " " + employee.EmployeeMiddleNameA + " " + employee.EmployeeLastNameA;
                        }
                    }
                    else
                    {
                        viewModel.ItemRelease.CreatedBy = "Admin";
                    }
                }
                viewModel.ItemRelease.FormNumber = "010101";
            }
            //Session["RoleName"];
            viewModel.Customers = response.Customers.Select(x => x.CreateFromServerToClient()).ToList();
            viewModel.ItemVariationDropDownList = response.ItemVariationDropDownList;
            return View(viewModel);
        }

        // POST: Inventory/ItemRelease/Create
        [HttpPost]
        [ValidateInput(false)]//this is due to CK Editor
        public ActionResult Create(ItemReleaseFormCreateViewModel viewModel)
        {
            if (viewModel.ItemRelease.ItemReleaseId > 0)
            {
                // Update
                viewModel.ItemRelease.RecUpdatedBy = User.Identity.GetUserId();
                viewModel.ItemRelease.RecUpdatedDate = DateTime.Now;
                var itemReleaseToUpdate = viewModel.ItemRelease.CreateFromClientToServer();
                foreach (var itemReleaseDetail in viewModel.ItemReleaseDetails)
                {
                    if (itemReleaseDetail.IRFDetailId > 0)
                    {
                        itemReleaseDetail.RecUpdatedBy = User.Identity.GetUserId();
                        itemReleaseDetail.RecUpdatedDate = DateTime.Now;
                    }
                    else
                    {
                        itemReleaseDetail.RecCreatedBy = User.Identity.GetUserId();
                        itemReleaseDetail.RecCreatedDate = DateTime.Now;
                        itemReleaseDetail.RecUpdatedBy = User.Identity.GetUserId();
                        itemReleaseDetail.RecUpdatedDate = DateTime.Now;
                    }
                }
                var itemReleaseDetailsToUpdate = viewModel.ItemReleaseDetails.Select(x => x.CreateFromClientToServer()).ToList();
                if (itemReleaseService.UpdateItemRelease(itemReleaseToUpdate, itemReleaseDetailsToUpdate))
                {
                    TempData["message"] = new MessageViewModel
                    {
                        Message = "Updated",
                        IsUpdated = true
                    };
                    return RedirectToAction("Index");
                }
            }
            else
            {
                // Add
                viewModel.ItemRelease.RecCreatedBy = User.Identity.GetUserId();
                viewModel.ItemRelease.RecCreatedDate = DateTime.Now;
                viewModel.ItemRelease.RecUpdatedBy = User.Identity.GetUserId();
                viewModel.ItemRelease.RecUpdatedDate = DateTime.Now;
                var itemReleaseToAdd = viewModel.ItemRelease.CreateFromClientToServer();
                foreach (var itemReleaseDetail in viewModel.ItemReleaseDetails)
                {
                    itemReleaseDetail.RecCreatedBy = User.Identity.GetUserId();
                    itemReleaseDetail.RecCreatedDate = DateTime.Now;
                    itemReleaseDetail.RecUpdatedBy = User.Identity.GetUserId();
                    itemReleaseDetail.RecUpdatedDate = DateTime.Now;
                }
                var itemReleaseDetailsToAdd = viewModel.ItemReleaseDetails.Select(x=>x.CreateFromClientToServer()).ToList();
                if (itemReleaseService.AddItemRelease(itemReleaseToAdd,itemReleaseDetailsToAdd))
                {
                    TempData["message"] = new MessageViewModel
                    {
                        Message = "Added",
                        IsUpdated = true
                    };
                    return RedirectToAction("Index");
                }
            }
            return View();
        }

        #endregion

        #region Get Customer RFIs

        [HttpGet]
        public JsonResult GetCustomerRfis(long customerId)
        {
            var customerOrders = ordersService.GetOrdersByCustomerIdWithRfis(customerId);
            IList<RFI> customerRfis = new List<RFI>();
            foreach (var customerOrder in customerOrders)
            {
                if (customerOrder.RFIs.Any())
                {
                    foreach (var rfI in customerOrder.RFIs)
                    {
                        customerRfis.Add(rfI.CreateRfiServerToClientForDropdown());
                    }
                }
            }
            return Json(customerRfis, JsonRequestBehavior.AllowGet);
        }
        #endregion
    }
}