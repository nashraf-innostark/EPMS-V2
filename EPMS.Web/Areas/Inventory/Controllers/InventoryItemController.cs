using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using EPMS.Interfaces.IServices;
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
            if (itemViewModel.InventoryItem.ItemId > 0)
            {
                //Update Case
                InventoryItemRequest itemToSave = itemViewModel.InventoryItem.CreateFromClientToServer();
                inventoryItemService.SaveItem(itemToSave);
                {
                    TempData["message"] = new MessageViewModel {Message = Resources.Inventory.InventoryItem.IsUpdated, IsSaved = true};
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

        #endregion

        #region Json Methods

        #region Get Last Item Code

        public JsonResult GetLastItemCode()
        {
            var itemCode = inventoryItemService.GetAll().OrderByDescending(x => x.ItemCode).FirstOrDefault().ItemCode;
            return Json(itemCode, JsonRequestBehavior.AllowGet);
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

        #endregion
    }
}