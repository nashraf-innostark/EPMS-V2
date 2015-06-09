using System.Collections.Generic;
using System.Web.Mvc;
using EPMS.Interfaces.IServices;
using EPMS.Web.Controllers;
using EPMS.Web.Models;
using EPMS.Web.ViewModels.PurchaseOrder;
using EPMS.WebBase.Mvc;

namespace EPMS.Web.Areas.Inventory.Controllers
{
    public class PurchaseOrderController : BaseController
    {
        private readonly IPurchaseOrderService orderService;

        #region Construcor
        public PurchaseOrderController(IPurchaseOrderService orderService)
        {
            this.orderService = orderService;
        }

        #endregion
        /// <summary>
        /// GET: Inventory/PurchaseOrder
        /// </summary>
        public ActionResult Index()
        {
            PurchaseOrderListViewModel viewModel = new PurchaseOrderListViewModel();
            return View(viewModel);
        }

        /// <summary>
        /// Create: Inventory/PurchaseOrder
        /// </summary>
        //[SiteAuthorize(PermissionKey = "TIRCreate,TIRDetail")]
        public ActionResult Create(long? id)
        {
            var response = orderService.GetPoResponseData(id);
            PurchaseOrderCreateViewModel viewModel = new PurchaseOrderCreateViewModel();
            if (response.Order != null)
            {
                
            }
            else
            {
                viewModel.Order = new PurchaseOrder();
                viewModel.Items = new List<PurchaseOrderItem>();
            }
            viewModel.ItemVariationDropDownList = response.ItemVariationDropDownList;
            return View(viewModel);
        }
    }
}