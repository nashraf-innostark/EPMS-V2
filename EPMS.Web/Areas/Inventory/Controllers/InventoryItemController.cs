using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using EPMS.Interfaces.IServices;
using EPMS.Models.DomainModels;
using EPMS.Models.RequestModels;
using EPMS.Web.Controllers;
using EPMS.Web.Models.Common;
using EPMS.Web.ViewModels.Common;
using EPMS.Web.ViewModels.InventoryItem;
using EPMS.Web.ModelMappers;
using EPMS.WebBase.Mvc;

namespace EPMS.Web.Areas.Inventory.Controllers
{
    [Authorize]
    [SiteAuthorize(PermissionKey = "IS", IsModule = true)]
    public class InventoryItemController : BaseController
    {
        #region Private

        private readonly IInventoryItemService inventoryItemService;
        private readonly IItemVariationService itemVariationService;
        private readonly IInventoryDepartmentService departmentService;

        #endregion

        #region Constructor

        public InventoryItemController(IInventoryItemService inventoryItemService, IItemVariationService itemVariationService, IInventoryDepartmentService departmentService)
        {
            this.inventoryItemService = inventoryItemService;
            this.itemVariationService = itemVariationService;
            this.departmentService = departmentService;
        }

        #endregion

        #region Public

        #region Index
        [SiteAuthorize(PermissionKey = "InventoryItemIndex")]
        public ActionResult Index()
        {
            return View(new InventoryItemViewModel
            {
                InventoryItems = inventoryItemService.GetAll().Select(x => x.CreateFromServerToClient())
            });
        }

        #endregion

        #region Create
        [SiteAuthorize(PermissionKey = "InventoryItemCreate")]
        public ActionResult Create(long? id)
        {
            InventoryItemViewModel itemViewModel = new InventoryItemViewModel();
            if (id !=null)
            {
                itemViewModel.InventoryItem = inventoryItemService.FindItemById((long)id).CreateFromServerToClient();
            }
            return View(itemViewModel);
        }

        [HttpPost]
        [ValidateInput(false)]//this is due to CK Editor
        public ActionResult Create(InventoryItemViewModel itemViewModel)
        {
            if (Request.Form["Save"] != null)
            {
                if (itemViewModel.InventoryItem.ItemId > 0)
                {
                    //Update Case
                    InventoryItemRequest itemToSave = itemViewModel.InventoryItem.CreateFromClientToServer();
                    inventoryItemService.SaveItem(itemToSave);
                    {
                        TempData["message"] = new MessageViewModel { Message = Resources.Inventory.InventoryItem.IsUpdated, IsSaved = true };
                        return RedirectToAction("Index");
                    }
                }
                else
                {
                    //Add Case
                    InventoryItemRequest itemToSave = itemViewModel.InventoryItem.CreateFromClientToServer();
                    inventoryItemService.SaveItem(itemToSave);
                    {
                        TempData["message"] = new MessageViewModel { Message = Resources.Inventory.InventoryItem.IsSaved, IsSaved = true };
                        return RedirectToAction("Index");
                    }
                }
            }
            if (Request.Form["Delete"] != null)
            {
                string deleteeStatus = inventoryItemService.DeleteItem(itemViewModel.InventoryItem.ItemId);
                switch (deleteeStatus)
                {
                    case "Success":
                        TempData["message"] = new MessageViewModel
                        {
                            Message = Resources.Inventory.InventoryItem.IsDeleted, 
                            IsUpdated = true
                        };
                        break;
                    case "Associated":
                        TempData["message"] = new MessageViewModel
                        {
                            Message = Resources.Inventory.InventoryItem.IsAssociated, 
                            IsError = true
                        };
                        break;
                    case "Error":
                        TempData["message"] = new MessageViewModel
                        {
                            Message = Resources.Inventory.InventoryItem.IsError, 
                            IsError = true
                        };
                        break;
                }
                return RedirectToAction("Index");
            }
            return View(itemViewModel);
        }

        #endregion

        #region Json Methods

        #region Get Last Item Code

        public JsonResult GetLastItemCode()
        {
            InventoryItem inventoryItem = inventoryItemService.GetAll().OrderByDescending(x => x.ItemCode).FirstOrDefault();
            if (inventoryItem != null)
            {
                var itemCode = inventoryItem.ItemCode;
                return Json(itemCode, JsonRequestBehavior.AllowGet);
            }
            return Json(null, JsonRequestBehavior.AllowGet);
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

        #region Upload Image

        public ActionResult UploadImage()
        {
            HttpPostedFileBase image = Request.Files[0];
            var filename = "";
            try
            {
                //Save File to Folder
                if ((image != null))
                {
                    filename =
                        (DateTime.Now.ToString(CultureInfo.InvariantCulture).Replace(".", "") + image.FileName)
                            .Replace("/", "").Replace("-", "").Replace(":", "").Replace(" ", "").Replace("+", "");
                    var filePathOriginal = Server.MapPath(ConfigurationManager.AppSettings["InventoryItemImage"]);
                    string savedFileName = Path.Combine(filePathOriginal, filename);
                    image.SaveAs(savedFileName);
                }
            }
            catch (Exception exp)
            {
                return
                    Json(
                        new
                        {
                            response = "Failed to upload. Error: " + exp.Message,
                            status = (int)HttpStatusCode.BadRequest
                        }, JsonRequestBehavior.AllowGet);
            }
            return
                Json(
                    new
                    {
                        filename = filename,
                        size = image.ContentLength / 1024 + "KB",
                        response = "Successfully uploaded!",
                        status = (int)HttpStatusCode.OK
                    }, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #endregion
    }
}