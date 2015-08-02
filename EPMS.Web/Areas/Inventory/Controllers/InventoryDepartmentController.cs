using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using EPMS.Interfaces.IServices;
using EPMS.Models.RequestModels;
using EPMS.Web.Controllers;
using EPMS.Web.Models;
using EPMS.Web.Models.Common;
using EPMS.Web.ViewModels.Common;
using EPMS.Web.ViewModels.InventoryDepartment;
using EPMS.Web.ModelMappers;
using EPMS.Web.ViewModels.InventorySection;
using EPMS.WebBase.Mvc;
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
        [SiteAuthorize(PermissionKey = "InventoryDepartmentIndex")]
        public ActionResult Index(int? id)
        {
            var inventoryDepartments = departmentService.GetAll().ToList();
            InventoryDepartmentViewModel viewModel = new InventoryDepartmentViewModel();
            viewModel.InventoryDepartments = inventoryDepartments.Select(idp => idp.CreateFromServerToClient());
            viewModel.Departments =
                inventoryDepartments.Where(dp => dp.ParentId == null).Select(dp => dp.CreateFromServerToClientLv());
            viewModel.Sections =
                inventoryDepartments.Where(sec => sec.ParentDepartment != null && sec.ParentDepartment.ParentId == null)
                    .Select(sec => sec.CreateFromServerToClientLv());
            var sections = MakeSectionSubSections(viewModel.Sections);
            viewModel.Sections = sections;
            if (id != null)
            {
                switch (id)
                {
                    case 1:
                        TempData["message"] = new MessageViewModel
                        {
                            Message = Resources.Inventory.InventoryDepartment.DepartmentSaved,
                            IsUpdated = true
                        };
                        break;
                    case 2:
                        TempData["message"] = new MessageViewModel
                        {
                            Message = Resources.Inventory.InventoryDepartment.SectionSaved,
                            IsUpdated = true
                        };
                        break;
                    case 3:
                        TempData["message"] = new MessageViewModel
                        {
                            Message = Resources.Inventory.InventoryDepartment.RecordUpdated,
                            IsUpdated = true
                        };
                        break;
                    case 4:
                        TempData["message"] = new MessageViewModel
                        {
                            Message = Resources.Inventory.InventoryDepartment.IsDeleted,
                            IsUpdated = true
                        };
                        break;
                    case 5:
                        TempData["message"] = new MessageViewModel
                        {
                            Message = Resources.Inventory.InventoryDepartment.IsAssociated,
                            IsError = true
                        };
                        break;
                    case 6:
                        TempData["message"] = new MessageViewModel
                        {
                            Message = Resources.Inventory.InventoryDepartment.IsError,
                            IsError = true
                        };
                        break;
                }
            }
            ViewBag.MessageVM = TempData["message"] as MessageViewModel;
            return View(viewModel);
        }
        #endregion

        #region Create
        [SiteAuthorize(PermissionKey = "InventoryDepartmentCreate,InventoryDepartmentDetail,InventorySectionCreate")]
        public ActionResult Create(long? id)
        {
            InventoryDepartmentViewModel departmentViewModel = new InventoryDepartmentViewModel();
            departmentViewModel.InventoryDepartments =
                departmentService.GetAll().Select(dp => dp.CreateFromServerToClient()).ToList();
            if (id != null)
            {
                ViewBag.InventoryDepartmentId = Convert.ToInt64(id);
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

        #region Inventory Section
        public ActionResult Section(string id)
        {
            InventorySectionViewModel departmentViewModel = new InventorySectionViewModel();
            departmentViewModel.InventoryDepartments =
                departmentService.GetAll().Select(dp => dp.CreateFromServerToClient()).ToList();
            if (id != null)
            {
                ViewBag.InventorySectionId = Convert.ToInt64(id);
            }
            return View(departmentViewModel);
        }
        #endregion

        #region Save Inventory Department
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult SaveInventoryDepartment(int nodeId, int parent, string nameEn, string nameAr, string color, string description, string requestFrom)
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
                    descp = descp.Replace("\"", "'");
                    model.DepartmentDesc = descp;
                    model.RecCreatedBy = User.Identity.GetUserId();
                    model.RecCreatedDt = DateTime.Now;
                    model.RecLastUpdatedBy = User.Identity.GetUserId();
                    model.RecLastUpdatedDt = DateTime.Now;
                    var nodeToUpdate = model.CreateFromClientToServerModel();
                    if (departmentService.UpdateDepartment(nodeToUpdate))
                    {
                        const string responseMessage = "Updated";
                        return Json(responseMessage, JsonRequestBehavior.AllowGet);
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
                        const string responseMessage = "Departments Saved";
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

        #region Delete Inventory Department
        [HttpPost]
        public ActionResult DeleteInventoryDepartment(long id)
        {
            try
            {
                string deleteStatus = departmentService.DeleteInventoryDepartment(id);
                return Json(deleteStatus, JsonRequestBehavior.AllowGet);
            }
            catch (Exception exception)
            {
                return Json(exception.Message, JsonRequestBehavior.AllowGet);
            }
        }

        #endregion

        #region Save Inventory Section
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult SaveInventorySection(int nodeId, int parent, string nameEn, string nameAr, string description, string requestFrom)
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
                    model.DepartmentColor = "";
                    var descp = description.Replace("\n", "");
                    descp = descp.Replace("\t", "");
                    descp = descp.Replace("\r", "");
                    descp = descp.Replace("\"", "'");
                    model.DepartmentDesc = descp;
                    model.RecCreatedBy = User.Identity.GetUserId();
                    model.RecCreatedDt = DateTime.Now;
                    model.RecLastUpdatedBy = User.Identity.GetUserId();
                    model.RecLastUpdatedDt = DateTime.Now;
                    var nodeToUpdate = model.CreateFromClientToServerModel();
                    if (departmentService.UpdateDepartment(nodeToUpdate))
                    {
                        const string responseMessage = "Updated";
                        return Json(responseMessage, JsonRequestBehavior.AllowGet);
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
                    model.DepartmentColor = "";
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
                        const string responseMessage = "Section Saved";
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

        #region Sections SubSections

        IEnumerable<InventoryDepartment> MakeSectionSubSections(IEnumerable<InventoryDepartment> Sections)
        {
            List<InventoryDepartment> listOfNodes = new List<InventoryDepartment>();
            List<InventoryDepartment> listOfChilds = new List<InventoryDepartment>();
            foreach (var inventoryDepartment in Sections)
            {
                if (!inventoryDepartment.InventoryDepartments.Any())
                {
                    ParentNodesSection nodes = new ParentNodesSection { TextEn = "None", TextAr = "None", Href = "" };
                    inventoryDepartment.ParentNodesSections.Add(nodes);
                    listOfChilds.Add(inventoryDepartment);
                }
                else
                {
                    ParentNodesSection nodes = new ParentNodesSection { TextEn = "None", TextAr = "None", Href = "" };
                    inventoryDepartment.ParentNodesSections.Add(nodes);
                    listOfChilds.Add(inventoryDepartment);
                    listOfNodes.Add(inventoryDepartment);
                }
            }
            for (int i = 0; i < listOfNodes.Count; i++)
            {
                for (int j = 0; j < listOfNodes[i].InventoryDepartments.Count(); j++)
                {
                    var childNode = listOfNodes[i].InventoryDepartments[j];
                    if (!childNode.InventoryDepartments.Any())
                    {
                        var parent = childNode.ParentSection;
                        while (parent.ParentId != null)
                        {
                            if (parent.DepartmentNameEn != null)
                            {
                                ParentNodesSection node = new ParentNodesSection
                                {
                                    TextEn = parent.DepartmentNameEn,
                                    TextAr = parent.DepartmentNameAr,
                                    Href = parent.DepartmentId.ToString()
                                };
                                childNode.ParentNodesSections.Add(node);
                            }
                            parent = parent.ParentSections;
                        }
                        listOfChilds.Add(childNode);
                    }
                    else
                    {
                        //childNode.NoOfSubSections = childNode.ParentSection.ParentSections == null ? 0 : childNode.InventoryDepartments.Count;
                        var parent = childNode.ParentSection;
                        while (parent.ParentId != null)
                        {
                            if (parent.DepartmentNameEn != null)
                            {
                                ParentNodesSection node = new ParentNodesSection
                                {
                                    TextEn = parent.DepartmentNameEn,
                                    TextAr = parent.DepartmentNameAr,
                                    Href = parent.DepartmentId.ToString()
                                };
                                childNode.ParentNodesSections.Add(node);
                            }
                            parent = parent.ParentSections;
                        }
                        listOfChilds.Add(childNode);
                        listOfNodes.Add(childNode);
                    }
                }
            }

            return listOfChilds;
        }
        #endregion

        #region Count SubNodes

        IList<InventoryDepartment> CountSubNodes(IList<InventoryDepartment> nodes)
        {
            IList<InventoryDepartment> retVal = new List<InventoryDepartment>();

            //foreach (var inventoryDepartment in nodes)
            //{
            //    inventoryDepartment.Color = "white";
            //}
            //IList<InventoryDepartment> listOfSubNodes = new List<InventoryDepartment>();
            //foreach (var inventoryDepartment in nodes)
            //{
            //    var currNode = inventoryDepartment;
            //    int nodeCount = 0;
            //    foreach (var department in currNode.InventoryDepartments)
            //    {
            //        listOfSubNodes.Add(department);
            //    }
            //    while (currNode != null)
            //    {
            //        if (currNode.Color != "grey")
            //        {
            //            currNode.Color = "gery";
            //            nodeCount += currNode.InventoryDepartments.Count;

            //            currNode = 
            //        }
            //        else
            //        {
            //            currNode = null;
            //        }
            //    }
            //}
            return retVal;
        }

        #endregion

        #region Get All Departments
        [HttpGet]
        public JsonResult GetAllDepartments(long? id)
        {
            var departments = departmentService.GetAll();
            IList<JsTree> details = departments.Select(x => x.CreateForJsTree()).ToList();
            return Json(details, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #endregion
    }
}