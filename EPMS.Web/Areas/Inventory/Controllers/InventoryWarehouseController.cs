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
using EPMS.WebBase.Mvc;

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
        [SiteAuthorize(PermissionKey = "InventoryWarehouseIndex")]
        public ActionResult Index()
        {
            var warehouses = warehouseService.GetAllWarehouses();
            InventoryWarehouseListViewModel viewModel = new InventoryWarehouseListViewModel
            {
                Warehouses = warehouses.Select(x=>x.CreateFromServerToClient())
            };
            return View(viewModel);
        }
        #endregion

        #region Add / Update

        [SiteAuthorize(PermissionKey = "InventoryWarehouseCreate,InventoryWarehouseDetail")]
        public ActionResult Create(long? id)
        {
            InventoryWarehouseCreateViewModel viewModel = new InventoryWarehouseCreateViewModel();
            if (id == null)
            {
                WarehouseRequest request = warehouseService.GetWarehouseRequest(0);
                viewModel.Warehouse = new Warehouse {WarehouseNumber = GenerateWarehouseNumber()};
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

        public string GenerateWarehouseNumber()
        {
            string lastWarehouseNumber = warehouseService.GetLastWarehouseNumber();
            string warehouseNumber = "";
            if (lastWarehouseNumber != "")
            {
                long number = Convert.ToInt64(lastWarehouseNumber.Substring(3));
                number += 1;
                string zeros = "";
                int len = number.ToString().Length;
                switch (len)
                {
                    case 1:
                        zeros = "00";
                        break;
                    case 2:
                        zeros = "0";
                        break;
                    case 3:
                        zeros = "";
                        break;
                }
                warehouseNumber = "War" + zeros + number;
            }
            return warehouseNumber;
        }
        #endregion

        #endregion
    }
}