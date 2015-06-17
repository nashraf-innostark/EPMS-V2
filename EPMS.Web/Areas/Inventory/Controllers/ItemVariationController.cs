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
using EPMS.Interfaces.Repository;
using EPMS.Models.DomainModels;
using EPMS.Models.RequestModels;
using EPMS.Web.Controllers;
using EPMS.Web.ModelMappers;
using EPMS.Web.ViewModels.Common;
using EPMS.Web.ViewModels.ItemVariation;
using Microsoft.AspNet.Identity;

namespace EPMS.Web.Areas.Inventory.Controllers
{
    public class ItemVariationController : BaseController
    {
        #region Private

        private readonly IItemVariationService itemVariationService;
        private readonly IColorService colorService;
        private readonly ISizeService sizeService;
        private readonly IManufacturerService manufacturerService;
        private readonly IStatusService statusService;
        private readonly IItemImageService itemImageService;
        private readonly IWarehouseService warehouseService;

        #endregion

        #region Constructor

        public ItemVariationController(IItemVariationService itemVariationService, IColorService colorService,
            ISizeService sizeService, IManufacturerService manufacturerService, IStatusService statusService,
            IItemImageService itemImageService, IWarehouseService warehouseService)
        {
            this.itemVariationService = itemVariationService;
            this.colorService = colorService;
            this.sizeService = sizeService;
            this.manufacturerService = manufacturerService;
            this.statusService = statusService;
            this.itemImageService = itemImageService;
            this.warehouseService = warehouseService;
        }

        #endregion

        #region Public

        #region Create
        public ActionResult Create(long? id)
        {
            ItemVariationViewModel variationViewModel = new ItemVariationViewModel();
            if (id != null)
            {
                variationViewModel.ItemVariation = itemVariationService.FindVariationById((long)id).CreateFromServerToClient();
            }
            variationViewModel.ColorsForDdl = colorService.GetAll().Select(x => x.CreateFromServerToClient()).ToList();
            variationViewModel.SizesForDdl = sizeService.GetAll().Select(x => x.CreateFromServerToClient()).ToList();
            variationViewModel.ManufacturersForDdl = manufacturerService.GetAll().Select(x => x.CreateFromServerToClient()).ToList();
            variationViewModel.StatusesForDdl = statusService.GetAll().Select(x => x.CreateFromServerToClient()).ToList();
            variationViewModel.WarehousesForDdl = warehouseService.GetAll().Select(x => x.CreateFromServerToClient()).ToList();
            return View(variationViewModel);
        }

        [HttpPost]
        public ActionResult Create(ItemVariationViewModel variationViewModel)
        {
            ItemVariationRequest itemToSave = variationViewModel.ItemVariation.CreateFromClientToServer();
            itemToSave.SizeArrayList = variationViewModel.SizeArrayList;
            itemToSave.ManufacturerArrayList = variationViewModel.ManufacturerArrayList;
            itemToSave.StatusArrayList = variationViewModel.StatusArrayList;
            itemToSave.ColorArrayList = variationViewModel.ColorArrayList;
            itemVariationService.SaveItemVariation(itemToSave);
            {
                TempData["message"] = new MessageViewModel { Message = "Added", IsSaved = true };
                return RedirectToAction("Create");
            }
        }

        #endregion

        #region JSON

        #region Save Size
        public JsonResult SaveSize(string nameEn, string nameAr)
        {
            Size sizeToSave = new Size();
            sizeToSave.SizeNameEn = nameEn;
            sizeToSave.SizeNameAr = nameAr;
            sizeToSave.RecCreatedBy = User.Identity.GetUserId();
            sizeToSave.RecCreatedDt = DateTime.Now;
            sizeToSave.RecLastUpdatedBy = User.Identity.GetUserId();
            sizeToSave.RecLastUpdatedDt = DateTime.Now;
            sizeService.AddSize(sizeToSave);
            return Json(sizeToSave, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Save Manufacturer

        public JsonResult SaveManufacturer(string nameEn, string nameAr)
        {
            Manufacturer manufacturerToSave = new Manufacturer();
            manufacturerToSave.ManufacturerNameEn = nameEn;
            manufacturerToSave.ManufacturerNameAr = nameAr;
            manufacturerToSave.RecCreatedBy = User.Identity.GetUserId();
            manufacturerToSave.RecCreatedDt = DateTime.Now;
            manufacturerToSave.RecLastUpdatedBy = User.Identity.GetUserId();
            manufacturerToSave.RecLastUpdatedDt = DateTime.Now;
            manufacturerService.AddManufacturer(manufacturerToSave);
            return Json(manufacturerToSave, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Save Status
        public JsonResult SaveStatus(string nameEn, string nameAr)
        {
            Status statusToSave = new Status();
            statusToSave.StatusNameEn = nameEn;
            statusToSave.StatusNameAr = nameAr;
            statusToSave.RecCreatedBy = User.Identity.GetUserId();
            statusToSave.RecCreatedDt = DateTime.Now;
            statusToSave.RecLastUpdatedBy = User.Identity.GetUserId();
            statusToSave.RecLastUpdatedDt = DateTime.Now;
            statusService.AddStatus(statusToSave);
            return Json(statusToSave, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Save Color
        public JsonResult SaveColor(string nameEn, string nameAr, string code)
        {
            Color colorToSave = new Color();
            colorToSave.ColorNameEn = nameEn;
            colorToSave.ColorNameAr = nameAr;
            colorToSave.ColorCode = code;
            colorToSave.RecCreatedBy = User.Identity.GetUserId();
            colorToSave.RecCreatedDt = DateTime.Now;
            colorToSave.RecLastUpdatedBy = User.Identity.GetUserId();
            colorToSave.RecLastUpdatedDt = DateTime.Now;
            colorService.AddColor(colorToSave);
            return Json(colorToSave, JsonRequestBehavior.AllowGet);
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
                    var filePathOriginal = Server.MapPath(ConfigurationManager.AppSettings["ItemImage"]);
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