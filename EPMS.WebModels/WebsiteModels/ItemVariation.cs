using System;
using System.Collections.Generic;
using System.Web.Mvc.Routing.Constraints;

namespace EPMS.WebModels.WebsiteModels
{
    public class ItemVariation
    {
        public ItemVariation()
        {
            InventoryItem  = new InventoryItemForVariation();
        }
        public long ItemVariationId { get; set; }
        public string ItemBarcode { get; set; }
        public string SKUCode { get; set; }
        public double? UnitCost { get; set; }
        public bool CostCalculation { get; set; }
        public double? UnitPrice { get; set; }
        public double? QuantityInPackage { get; set; }
        public double? PackagePrice { get; set; }
        public bool PriceCalculation { get; set; }
        public string DescriptionEn { get; set; }
        public string DescriptionAr { get; set; }
        public string SKUDescriptionEn { get; set; }
        public string SKUDescriptionAr { get; set; }
        public string QuantityInHand { get; set; }
        public long? QuantitySold { get; set; }
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
        public string DescriptionForQuotationEn { get; set; }
        public string DescriptionForQuotationAr { get; set; }
        public string RecCreatedBy { get; set; }
        public DateTime RecCreatedDt { get; set; }
        public string RecLastUpdatedBy { get; set; }
        public DateTime RecLastUpdatedDt { get; set; }
        public long InventoryItemId { get; set; }
        public string InventoryItemDesc { get; set; }
        public string ItemDescForIndexEn { get; set; }
        public string ItemDescForIndexAr { get; set; }
        public List<Color> Colors { get; set; }
        public List<Size> Sizes { get; set; }
        public string SizeNameEn { get; set; }
        public string SizeNameAr { get; set; }
        public string StatusNameEn { get; set; }
        public string StatusNameAr { get; set; }
        public string ColorNameEn { get; set; }
        public string ColorNameAr { get; set; }
        public double? AverageCost { get; set; }
        public double? TotalQuantityInHand { get; set; }
        public bool QtyInHand { get; set; }

        public List<ItemManufacturer> ItemManufacturers { get; set; }
        public List<Status> Statuses { get; set; }
        public List<ItemWarehouse> ItemWarehouses { get; set; }
        public List<ItemImage> ItemImages { get; set; }
        public InventoryItemForVariation InventoryItem { get; set; }

    }
}