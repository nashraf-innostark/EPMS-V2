using System;
using System.Collections.Generic;
using EPMS.Models.DomainModels;

namespace EPMS.Web.Models
{
    public class ItemVariation
    {
        public long ItemVariationId { get; set; }
        public string ItemBarcode { get; set; }
        public string SKUCode { get; set; }
        public double? UnitPrice { get; set; }
        public double? PackagePrice { get; set; }
        public bool PriceCalculation { get; set; }
        public string DescriptionEn { get; set; }
        public string DescriptionAr { get; set; }
        public string SKUDescriptionEn { get; set; }
        public string SKUDescriptionAr { get; set; }
        public string QuantityInHand { get; set; }
        public string QuantitySold { get; set; }
        public string ReorderPoint { get; set; }
        public string QuantityInManufacturing { get; set; }
        public string Weight { get; set; }
        public string Height { get; set; }
        public string Width { get; set; }
        public string Depth { get; set; }
        public string NotesEn { get; set; }
        public string NotesAr { get; set; }
        public string AdditionalInfoEn { get; set; }
        public string AdditionalInfoAr { get; set; }
        public string RecCreatedBy { get; set; }
        public DateTime RecCreatedDt { get; set; }
        public string RecLastUpdatedBy { get; set; }
        public DateTime RecLastUpdatedDt { get; set; }
        public long InventoryItemId { get; set; }

        public List<Color> Colors { get; set; }
        public List<Size> Sizes { get; set; }
        public List<Manufacturer> Manufacturers { get; set; }
        public List<Status> Statuses { get; set; }
        public List<Warehouse> Warehouses { get; set; }
        public List<ItemImage> ItemImages { get; set; }

    }
}