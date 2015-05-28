using System;
using System.Linq;
using System.Web.Mvc;
using EPMS.Interfaces.IServices;
using EPMS.Models.ModelMapers.NotificationMapper;
using EPMS.Models.RequestModels;
using EPMS.Web.Controllers;
using EPMS.Web.ModelMappers;
using EPMS.Web.Models;
using EPMS.Web.ViewModels.InventoryWarehouse;

namespace EPMS.Web.Areas.Inventory.Controllers
{
    public class InventoryWarehouseController : BaseController
    {
        #region Private

        private readonly IWarehouseService warehouseService;

        #endregion

        #region Constructor
        public InventoryWarehouseController(IWarehouseService warehouseService)
        {
            this.warehouseService = warehouseService;
        }

        #endregion

        #region Public

        #region List View
        // GET: Inventory/InvontryWarehouse
        public ActionResult Index()
        {
            InventoryWarehouseListViewModel viewModel = new InventoryWarehouseListViewModel
            {
                Warehouses = warehouseService.GetAll().Select(x=>x.CreateFromServerToClient())
            };
            return View(viewModel);
        }
        #endregion

        #region Add / Update
        public ActionResult Create(long? id)
        {
            InventoryWarehouseCreateViewModel viewModel = new InventoryWarehouseCreateViewModel();
            if (id == null)
            {
                WarehouseRequest request = warehouseService.GetWarehouseRequest(0);
                viewModel.Warehouse = new Warehouse();
                viewModel.Warehouses = request.Warehouses.Select(x => x.CreateFromServerToClient());
                viewModel.Employees = request.Employees.Select(x => x.CreateForEmployeeDDL());
            }
            else
            {
                WarehouseRequest request = warehouseService.GetWarehouseRequest(Convert.ToInt64(id));
                viewModel.Warehouse = request.Warehouse.CreateFromServerToClient();
                viewModel.Warehouses = request.Warehouses.Select(x => x.CreateFromServerToClient());
                viewModel.Employees = request.Employees.Select(x => x.CreateForEmployeeDDL());
            }
            return View(viewModel);
        }
        #endregion

        #region Get Warehouse Number
        #endregion

        #endregion
    }
}