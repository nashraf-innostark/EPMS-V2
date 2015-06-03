using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using EPMS.Interfaces.IServices;
using EPMS.Models.ResponseModels;
using EPMS.Web.Controllers;
using EPMS.Web.ModelMappers;
using EPMS.Web.ModelMappers.Inventory.RFI;
using EPMS.Web.Models;
using EPMS.Web.ViewModels.IRF;
using Employee = EPMS.Web.Resources.HR.Employee;

namespace EPMS.Web.Areas.Inventory.Controllers
{
    public class ItemReleaseController : BaseController
    {
        #region Private

        private readonly IItemReleaseService itemReleaseFormService;
        private readonly IRFIService RfiService;
        private readonly IOrdersService ordersService;
        private readonly IAspNetUserService userService;

        #endregion

        #region Constructor
        public ItemReleaseController(IItemReleaseService itemReleaseFormService, IRFIService rfiService, IOrdersService ordersService, IAspNetUserService userService)
        {
            this.itemReleaseFormService = itemReleaseFormService;
            RfiService = rfiService;
            this.ordersService = ordersService;
            this.userService = userService;
        }

        #endregion

        #region Public
        // GET: Inventory/IRF
        public ActionResult Index()
        {
            ItemReleaseFormListViewModel viewModel = new ItemReleaseFormListViewModel();
            return View(viewModel);
        }
        /// <summary>
        /// 
        /// </summary>
        public ActionResult Create(long? id)
        {
            ItemReleaseFormCreateViewModel viewModel = new ItemReleaseFormCreateViewModel();
            IRFCreateResponse response;
            if (id != null)
            {
                response = itemReleaseFormService.GetCreateResponse((long) id);
                viewModel.ItemRelease = response.ItemRelease.CreateFromServerToClient();
            }
            else
            {
                response = itemReleaseFormService.GetCreateResponse(0);
                viewModel.ItemRelease = new ItemRelease();
                if (Session["RoleName"] != null)
                {
                    if (Session["RoleName"].ToString() != "Admin")
                    {
                        var employee = userService.FindById(Session["UserID"].ToString()).Employee;
                        var direction = EPMS.Web.Resources.Shared.Common.TextDirection;
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
            }
            //Session["RoleName"];
            viewModel.Customers = response.Customers.Select(x => x.CreateForDashboard()).ToList();
            viewModel.ItemVariationDropDownList = response.ItemVariationDropDownList;
            return View(viewModel);
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