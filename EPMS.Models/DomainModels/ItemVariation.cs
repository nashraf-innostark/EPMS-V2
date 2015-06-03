using System;
using System.Collections.Generic;

namespace EPMS.Models.DomainModels
{
    public class ItemVariation
    {
        public long ItemVariationId { get; set; }
        public string ItemBarcode { get; set; }
        public string SKUCode { get; set; }
        public Nullable<double> UnitPrice { get; set; }
        public Nullable<double> PackagePrice { get; set; }
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
        public System.DateTime RecCreatedDt { get; set; }
        public string RecLastUpdatedBy { get; set; }
        public System.DateTime RecLastUpdatedDt { get; set; }
        public long InventoryItemId { get; set; }

        public virtual InventoryItem InventoryItem { get; set; }
        public virtual ICollection<ItemImage> ItemImages { get; set; }
        public virtual ICollection<ItemManufacturer> ItemManufacturers { get; set; }
        public virtual ICollection<RFIItem> RFIItems { get; set; }
        public virtual ICollection<Color> Colors { get; set; }
        public virtual ICollection<Size> Sizes { get; set; }
        public virtual ICollection<Status> Status { get; set; }
        public virtual ICollection<Warehouse> Warehouses { get; set; }
        public virtual ICollection<RIFItem> RIFItems { get; set; }
    }
}
