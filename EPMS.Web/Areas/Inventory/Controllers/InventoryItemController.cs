using System.Linq;
using System.Web.Mvc;
using EPMS.Interfaces.IServices;
using EPMS.Models.DomainModels;
using EPMS.Models.RequestModels;
using EPMS.Web.Controllers;
using EPMS.Web.ViewModels.Common;
using EPMS.Web.ViewModels.InventoryItem;
using EPMS.Web.ModelMappers;

namespace EPMS.Web.Areas.Inventory.Controllers
{
    public class InventoryItemController : BaseController
    {
        #region Private

        private readonly IInventoryItemService inventoryItemService;
        private readonly IItemVariationService itemVariationService;

        #endregion

        #region Constructor

        public InventoryItemController(IInventoryItemService inventoryItemService, IItemVariationService itemVariationService)
        {
            this.inventoryItemService = inventoryItemService;
            this.itemVariationService = itemVariationService;
        }

        #endregion

        #region Public

        #region Index

        public ActionResult Index()
        {
            return View(new InventoryItemViewModel
            {
                InventoryItems = inventoryItemService.GetAll().Select(x => x.CreateFromServerToClient())
            });
        }

        #endregion

        #region Create

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
        public ActionResult Create(InventoryItemViewModel itemViewModel)
        {
            InventoryItemRequest itemToSave = itemViewModel.InventoryItem.CreateFromClientToServer();
            inventoryItemService.SaveItem(itemToSave);
            {
                TempData["message"] = new MessageViewModel { Message = "Added", IsSaved = true };
                return RedirectToAction("Index");
            }
        }

        #endregion


        #endregion
    }
}