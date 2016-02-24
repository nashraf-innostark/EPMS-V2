﻿using System;
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
using EPMS.WebBase.Mvc;
using EPMS.WebModels.ModelMappers;
using EPMS.Models.RequestModels;
using EPMS.Models.ResponseModels;
using EPMS.WebModels.ViewModels.ItemVariation;
using EPMS.WebModels.WebsiteModels.Common;
using EPMS.Web.Controllers;
using EPMS.WebModels.ViewModels.Common;
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
        private readonly IWarehouseDetailService warehouseDetailService;
        private readonly IInventoryItemService inventoryItemService;
        private readonly IVendorService vendorService;

        #endregion

        #region Constructor

        public ItemVariationController(IItemVariationService itemVariationService, IColorService colorService,
            ISizeService sizeService, IManufacturerService manufacturerService, IStatusService statusService,
            IItemImageService itemImageService, IWarehouseService warehouseService,
            IWarehouseDetailService warehouseDetailService, IInventoryItemService inventoryItemService, IVendorService vendorService)
        {
            this.itemVariationService = itemVariationService;
            this.colorService = colorService;
            this.sizeService = sizeService;
            this.manufacturerService = manufacturerService;
            this.statusService = statusService;
            this.itemImageService = itemImageService;
            this.warehouseService = warehouseService;
            this.warehouseDetailService = warehouseDetailService;
            this.inventoryItemService = inventoryItemService;
            this.vendorService = vendorService;
        }

        #endregion

        #region Public

        #region Create
        [SiteAuthorize(PermissionKey = "CreateItemVariation,ViewItemVariation")]
        public ActionResult Create(long? id, long inventoryItemId)
        {

            ItemVariationViewModel variationViewModel = new ItemVariationViewModel();
            ItemVariationResponse response;
            if (id != null)
            {
                response = itemVariationService.ItemVariationResponse((long)id, inventoryItemId);
                variationViewModel.ItemVariation = response.ItemVariation.CreateFromServerToClient();
            }
            else
            {
                response = itemVariationService.ItemVariationResponse(0, inventoryItemId);
                variationViewModel.ItemVariation.InventoryItemId = inventoryItemId;
                var descE = response.InventoryItem.ItemDescriptionEn;
                if (!string.IsNullOrEmpty(descE))
                {
                    descE = descE.Replace("\r", "");
                    descE = descE.Replace("\t", "");
                    descE = descE.Replace("\n", "");
                }
                var descA = response.InventoryItem.ItemDescriptionAr;
                if (!string.IsNullOrEmpty(descE))
                {
                    descA = descA.Replace("\r", "");
                    descA = descA.Replace("\t", "");
                    descA = descA.Replace("\n", "");
                }
                variationViewModel.ItemVariation.DescriptionEn = descE;
                variationViewModel.ItemVariation.DescriptionAr = descA;
            }
            variationViewModel.ColorsForDdl = response.ColorsForDdl.Select(x => x.CreateFromServerToClient()).ToList();
            variationViewModel.SizesForDdl = response.SizesForDdl.Select(x => x.CreateFromServerToClient()).ToList();
            //variationViewModel.ManufacturersForDdl = response.ManufacturersForDdl.Select(x => x.CreateFromServerToClient()).ToList();
            variationViewModel.VendorsForDdl = response.ManufacturersForDdl.Select(x => x.CreateFromServerToClientForDdl()).ToList();
            variationViewModel.StatusesForDdl = response.StatusesForDdl.Select(x => x.CreateFromServerToClient()).ToList();
            variationViewModel.WarehousesForDdl = response.WarehousesForDdl.Select(x => x.CreateFromItemVariationDropDown()).ToList();

            return View(variationViewModel);
        }

        [HttpPost]
        [ValidateInput(false)]//this is due to CK Editor
        public ActionResult Create(ItemVariationViewModel variationViewModel)
        {
            //variationViewModel.ItemVariation.InventoryItem =
            //    inventoryItemService.FindItemById(variationViewModel.ItemVariation.InventoryItemId)
            //        .CreateForItemVariation();
            if (variationViewModel.ItemVariation.ItemVariationId > 0)
            {
                ItemVariationRequest itemToSave = variationViewModel.ItemVariation.CreateFromClientToServer();
                itemToSave.SizeArrayList = variationViewModel.SizeArrayList;
                itemToSave.ManufacturerArrayList = variationViewModel.ManufacturerArrayList;
                itemToSave.StatusArrayList = variationViewModel.StatusArrayList;
                itemToSave.ColorArrayList = variationViewModel.ColorArrayList;
                itemVariationService.SaveItemVariation(itemToSave);
                {
                    TempData["message"] = new MessageViewModel { Message = EPMS.WebModels.Resources.Inventory.ItemVariation.IsUpdated, IsUpdated = true };
                    return Redirect(variationViewModel.ItemVariation.ItemVariationId + "?inventoryItemId=" + variationViewModel.ItemVariation.InventoryItemId);
                }
            }
            else
            {
                variationViewModel.ItemVariation.RecCreatedDt = DateTime.Now.ToString("dd/MM/yyyy",
                    new CultureInfo("en"));
                ItemVariationRequest itemToSave = variationViewModel.ItemVariation.CreateFromClientToServer();
                itemToSave.SizeArrayList = variationViewModel.SizeArrayList;
                itemToSave.ManufacturerArrayList = variationViewModel.ManufacturerArrayList;
                itemToSave.StatusArrayList = variationViewModel.StatusArrayList;
                itemToSave.ColorArrayList = variationViewModel.ColorArrayList;
                itemVariationService.SaveItemVariation(itemToSave);
                {
                    TempData["message"] = new MessageViewModel { Message = EPMS.WebModels.Resources.Inventory.ItemVariation.IsUpdated, IsSaved = true };
                    return Redirect("Create/" + itemToSave.ItemVariation.ItemVariationId + "?inventoryItemId=" + variationViewModel.ItemVariation.InventoryItemId);
                }
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

        //#region Save Manufacturer

        //public JsonResult SaveManufacturer(string nameEn, string nameAr)
        //{
        //    Manufacturer manufacturerToSave = new Manufacturer();
        //    manufacturerToSave.ManufacturerNameEn = nameEn;
        //    manufacturerToSave.ManufacturerNameAr = nameAr;
        //    manufacturerToSave.RecCreatedBy = User.Identity.GetUserId();
        //    manufacturerToSave.RecCreatedDt = DateTime.Now;
        //    manufacturerToSave.RecLastUpdatedBy = User.Identity.GetUserId();
        //    manufacturerToSave.RecLastUpdatedDt = DateTime.Now;
        //    manufacturerService.AddManufacturer(manufacturerToSave);
        //    return Json(manufacturerToSave, JsonRequestBehavior.AllowGet);
        //}
        //#endregion

        #region Save Manufacturer

        public JsonResult SaveManufacturer(string nameEn, string nameAr)
        {
            Vendor vendorToSave = new Vendor();
            vendorToSave.VendorNameEn = nameEn;
            vendorToSave.VendorNameAr= nameAr;
            vendorToSave.RecCreatedBy = User.Identity.GetUserId();
            vendorToSave.RecCreatedDt = DateTime.Now;
            vendorToSave.RecLastUpdatedBy = User.Identity.GetUserId();
            vendorToSave.RecLastUpdatedDt = DateTime.Now;
            vendorService.AddVendor(vendorToSave);
            return Json(vendorToSave, JsonRequestBehavior.AllowGet);
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

        #region Get All Warehouses
        [HttpGet]
        public JsonResult GetWarehouseDetails(long id)
        {
            var warehouse = warehouseService.FindWarehouseById(id);
            IList<JsTree> details = warehouse.WarehouseDetails.Select(x => x.CreateForJsTree()).ToList();
            return Json(details, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Get Last Item SKU Code

        public JsonResult GetLastItemSKuCode()
        {
            List<string> list = new List<string>();
            var firstOrDefault = itemVariationService.GetAll().OrderByDescending(x => x.SKUCode).FirstOrDefault();
            if (firstOrDefault != null)
            {
                var itemCode = firstOrDefault.SKUCode;
                var inventoryItem = inventoryItemService.GetAll().OrderByDescending(x => x.ItemCode).FirstOrDefault();
                if (inventoryItem != null)
                {
                    var itemDescription =
                        inventoryItem.ItemCode;
                    list.Add(itemCode);
                    list.Add(itemDescription);
                }
                return Json(list, JsonRequestBehavior.AllowGet);
            }
            list.Add(null);
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Get Item Variation Id
        [HttpPost]
        public JsonResult GetItemVariationId(string[] items)
        {
            var itemVariationId = itemVariationService.GetItemVariationId(items);
            return Json(itemVariationId, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Get Item Variation By Barcode

        [HttpGet]
        public JsonResult GetItemVariationByBarcode(string id)
        {
            PCFromBarcodeResponse barcodeResponse = itemVariationService.FindVariationByBarcode(id);
            if (barcodeResponse != null)
            {
                return Json(barcodeResponse, JsonRequestBehavior.AllowGet);
            }
            return Json(null, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #endregion
    }
}