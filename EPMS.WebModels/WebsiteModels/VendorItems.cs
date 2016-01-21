using System;

namespace EPMS.WebModels.WebsiteModels
{
    public class VendorItems
    {
        public long ItemId { get; set; }
        public string ItemDetails { get; set; }
        public long VendorId { get; set; }
        public long? ItemVariationId { get; set; }
        public long ItemQty { get; set; }
        public bool IsItemDescription { get; set; }
        public bool IsItemSKU { get; set; }
        public string PlaceInDepartment { get; set; }
        public string RecCreatedBy { get; set; }
        public string RecCreatedDate { get; set; }
        public string RecUpdatedBy { get; set; }
        public DateTime RecUpdatedDate { get; set; }
    }
}