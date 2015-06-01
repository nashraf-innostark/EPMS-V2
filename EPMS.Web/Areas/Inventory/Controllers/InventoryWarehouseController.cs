using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using EPMS.Interfaces.IServices;
using EPMS.Models.ModelMapers.NotificationMapper;
using EPMS.Models.RequestModels;
using EPMS.Web.Controllers;
using EPMS.Web.ModelMappers;
using EPMS.Web.Models;
using EPMS.Web.Models.Common;
using EPMS.Web.ViewModels.Common;
using EPMS.Web.ViewModels.InventoryWarehouse;
using EPMS.WebBase.Mvc;
using Microsoft.AspNet.Identity;

namespace EPMS.Web.Areas.Inventory.Controllers
{
    public class InventoryWarehouseController : BaseController
    {
        #region Private

        private readonly IWarehouseService warehouseService;
        private readonly IWarehouseDetailService detailService;
        private readonly IEmployeeService employeeService;

        #endregion

        #region Constructor
        public InventoryWarehouseController(IWarehouseService warehouseService, IEmployeeService employeeService, IWarehouseDetailService detailService)
        {
            this.warehouseService = warehouseService;
            this.employeeService = employeeService;
            this.detailService = detailService;
        }

        #endregion

        #region Public

        #region List View
        // GET: Inventory/InvontryWarehouse
        [SiteAuthorize(PermissionKey = "InventoryWarehouseIndex")]
        public ActionResult Index()
        {
            var warehouses = warehouseService.GetAll();
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

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(InventoryWarehouseCreateViewModel model)
        {
            try
            {
                if (model.Warehouse.WarehouseId > 0)
                {
                    // Update
                    model.Warehouse.RecLastUpdatedBy = User.Identity.GetUserId();
                    model.Warehouse.RecLastUpdatedDt = DateTime.Now;
                    var warehouseToUpdate = model.Warehouse.CreateFromClientToServer();
                    if (warehouseService.Updatewarehouse(warehouseToUpdate))
                    {
                        TempData["message"] = new MessageViewModel
                        {
                            Message = "Warehouse Updated Successfully",
                            IsUpdated = true
                        };
                        return RedirectToAction("Index");
                    }
                }
                else
                {
                    // Add
                    model.Warehouse.RecCreatedBy = User.Identity.GetUserId();
                    model.Warehouse.RecCreatedDt = DateTime.Now;
                    model.Warehouse.RecLastUpdatedBy = User.Identity.GetUserId();
                    model.Warehouse.RecLastUpdatedDt = DateTime.Now;
                    var warehouseToAdd = model.Warehouse.CreateFromClientToServer();
                    if (warehouseService.AddWarehouse(warehouseToAdd))
                    {
                        TempData["message"] = new MessageViewModel
                        {
                            Message = "Warehouse Updated Successfully",
                            IsUpdated = true
                        };
                        return RedirectToAction("Index");
                    }
                }
            }
            catch (Exception)
            {
                throw new ArgumentException("Error");
            }
            model.Employees = employeeService.GetAll().Select(x => x.CreateForEmployeeDDL());
            return View(model);
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

        #region Get Warehouse Number
        /// <summary>
        /// Get Warehouse Details
        /// </summary>
        [HttpGet]
        public JsonResult GetWarehouseDetails(long id)
        {
            var warehouse = warehouseService.FindWarehouseById(id);
            IList<JsTree> details = warehouse.WarehouseDetails.Select(x => x.CreateForJsTree()).ToList();
            return Json(details, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Save Inventory Section
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult SaveWarehouseDetails(int parentId, int warehouseId, int nodeId, string nameEn, string nameAr, int nodeLevel, string createdBy, string createdDt)
        {
            WarehouseDetail model = new WarehouseDetail();
            try
            {
                if (nodeId > 0)
                {
                    // Update
                    model.WarehouseId = warehouseId;
                    model.WarehouseDetailId = nodeId;
                    model.NameEn = nameEn;
                    model.NameAr = nameAr;
                    model.NodeLevel = (short?) nodeLevel;
                    model.RecCreatedBy = createdBy;
                    model.RecCreatedDt = createdDt;
                    if (parentId > 0)
                    {
                        model.ParentId = parentId;
                    }
                    else
                    {
                        model.ParentId = null;
                    }
                    model.RecLastUpdatedBy = User.Identity.GetUserId();
                    model.RecLastUpdatedDt = DateTime.Now;
                    var nodeToUpdate = model.CreateFromClientToServer();
                    if (detailService.UpdateWarehouseDetail(nodeToUpdate))
                    {
                        const string responseMessage = "Updated";
                        return Json(responseMessage, JsonRequestBehavior.AllowGet);
                    }
                }
                else
                {
                    // Add
                    model.WarehouseId = warehouseId;
                    model.WarehouseDetailId = nodeId;
                    model.NameEn = nameEn;
                    model.NameAr = nameAr;
                    model.NodeLevel = (short?)nodeLevel;
                    model.RecCreatedBy = User.Identity.GetUserId();
                    model.RecCreatedDt = DateTime.Now.ToShortDateString();
                    model.RecLastUpdatedBy = User.Identity.GetUserId();
                    model.RecLastUpdatedDt = DateTime.Now;
                    if (parentId > 0)
                    {
                        model.ParentId = parentId;
                    }
                    else
                    {
                        model.ParentId = null;
                    }
                    var newNodeToAdd = model.CreateFromClientToServer();
                    if (detailService.AddWarehouseDetail(newNodeToAdd))
                    {
                        const string responseMessage = "Saved";
                        return Json(responseMessage, JsonRequestBehavior.AllowGet);
                    }
                }
            }
            catch (Exception exception)
            {
                return Json(exception.Message, JsonRequestBehavior.AllowGet);
            }
            return Json(null, JsonRequestBehavior.AllowGet);
        }

        #endregion
        
        #endregion
    }
}