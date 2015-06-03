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

namespace EPMS.Web.Areas.Inventory.Controllers
{
    public class ItemReleaseFormController : BaseController
    {
        #region Private

        private readonly IItemReleaseFormService itemReleaseFormService;
        private readonly IRFIService RfiService;
        private readonly IOrdersService ordersService;

        #endregion

        #region Constructor
        public ItemReleaseFormController(IItemReleaseFormService itemReleaseFormService, IRFIService rfiService, IOrdersService ordersService)
        {
            this.itemReleaseFormService = itemReleaseFormService;
            RfiService = rfiService;
            this.ordersService = ordersService;
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
            IRFCreateResponse response = id != null ? itemReleaseFormService.GetCreateResponse((long)id) : itemReleaseFormService.GetCreateResponse(0);
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