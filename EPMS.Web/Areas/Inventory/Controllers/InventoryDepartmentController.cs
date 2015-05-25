using System;
using System.Linq;
using System.Web.Mvc;
using EPMS.Interfaces.IServices;
using EPMS.Models.RequestModels;
using EPMS.Web.Controllers;
using EPMS.Web.Models;
using EPMS.Web.ViewModels.Common;
using EPMS.Web.ViewModels.InventoryDepartment;
using EPMS.Web.ModelMappers;
using Microsoft.AspNet.Identity;

namespace EPMS.Web.Areas.Inventory.Controllers
{
    public class InventoryDepartmentController : BaseController
    {
        #region Private

        private readonly IInventoryDepartmentService departmentService;

        #endregion

        #region Constructor

        public InventoryDepartmentController(IInventoryDepartmentService departmentService)
        {
            this.departmentService = departmentService;
        }

        #endregion

        #region Public

        #region Index
        public ActionResult Index()
        {
            return View(new InventoryDepartmentViewModel
            {
                InventoryDepartments = departmentService.GetAll().Select(x => x.CreateFromServerToClient())
            });
        }
        #endregion

        #region Create

        public ActionResult Create(long? id)
        {
            InventoryDepartmentViewModel departmentViewModel = new InventoryDepartmentViewModel
            {
                InventoryDepartments = departmentService.GetAll().Select(dp => dp.CreateFromServerToClient()).ToList()
            };
            if (id != null)
            {
                departmentViewModel.InventoryDepartment = departmentService.FindInventoryDepartmentById((long)id).CreateFromServerToClient();
            }
            return View(departmentViewModel);
        }

        [HttpPost]
        public ActionResult Create(InventoryDepartmentViewModel departmentViewModel)
        {
            InventoryDepartmentRequest departmentToSave =
                departmentViewModel.InventoryDepartment.CreateFromClientToServer();
            departmentService.SaveDepartment(departmentToSave);
            {
                TempData["message"] = new MessageViewModel { Message = "Added", IsSaved = true };
                return RedirectToAction("Index");
            }
        }


        #endregion

        #region Save Inventory Department
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult SaveInventoryDepartment(int nodeId, int parent, string nameEn, string nameAr, string color, string description)
        {
            InventoryDepartment model = new InventoryDepartment();
            try
            {
                if (nodeId > 0)
                {
                    // Update
                    model.DepartmentId = nodeId;
                    if (parent > 0)
                    {
                        model.ParentId = parent;
                    }
                    else
                    {
                        model.ParentId = null;
                    }
                    model.DepartmentNameEn = nameEn;
                    model.DepartmentNameAr = nameAr;
                    model.DepartmentColor = color;
                    var descp = description.Replace("\n", "");
                    descp = descp.Replace("\t", "");
                    descp = descp.Replace("\r", "");
                    model.DepartmentDesc = descp;
                    model.RecCreatedBy = User.Identity.GetUserId();
                    model.RecCreatedDt = DateTime.Now;
                    model.RecLastUpdatedBy = User.Identity.GetUserId();
                    model.RecLastUpdatedDt = DateTime.Now;
                    var nodeToUpdate = model.CreateFromClientToServerModel();
                    if (departmentService.UpdateDepartment(nodeToUpdate))
                    {
                        var inventoryDepartments = departmentService.GetAll();
                        InventoryDepartmentViewModel viewModel = new InventoryDepartmentViewModel
                        {
                            InventoryDepartments = inventoryDepartments.Select(x => x.CreateFromServerToClient()).ToList()
                        };
                        return Json(viewModel, JsonRequestBehavior.AllowGet);
                    }
                }
                else
                {
                    // Add
                    if (parent > 0)
                    {
                        model.ParentId = parent;
                    }
                    else
                    {
                        model.ParentId = null;
                    }
                    model.DepartmentNameEn = nameEn;
                    model.DepartmentNameAr = nameAr;
                    model.DepartmentColor = color;
                    var descp = description.Replace("\n", "");
                    descp = descp.Replace("\t", "");
                    descp = descp.Replace("\r", "");
                    model.DepartmentDesc = descp;
                    model.RecCreatedBy = User.Identity.GetUserId();
                    model.RecCreatedDt = DateTime.Now;
                    model.RecLastUpdatedBy = User.Identity.GetUserId();
                    model.RecLastUpdatedDt = DateTime.Now;
                    var newNodeToAdd = model.CreateFromClientToServerModel();
                    if (departmentService.AddDepartment(newNodeToAdd))
                    {
                        var inventoryDepartments = departmentService.GetAll();
                        InventoryDepartmentViewModel viewModel = new InventoryDepartmentViewModel
                        {
                            InventoryDepartments = inventoryDepartments.Select(x => x.CreateFromServerToClient()).ToList()
                        };
                        return Json(viewModel, JsonRequestBehavior.AllowGet);
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