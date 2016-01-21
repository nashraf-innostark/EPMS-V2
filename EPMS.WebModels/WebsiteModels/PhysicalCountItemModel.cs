using System;
using System.ComponentModel.DataAnnotations;

namespace EPMS.WebModels.WebsiteModels
{
    public class PhysicalCountItemModel
    {
        public long PcItemId { get; set; }
        public long PcId { get; set; }
        public long ItemVariationId { get; set; }
        public long WarehouseId { get; set; }
        [Required]
        [Display(Name = "Number of Packages In Warehouse")]
        public long NoOfPackagesInWarehouse { get; set; }
        [Required]
        [Display(Name = "Number of Item In Warehouse")]
        public long NoOfItemInWarehouse { get; set; }
        [Required]
        [Display(Name = "Item Barcode")]
        public string ItemBarcode { get; set; }
        public string ItemDetailsEn { get; set; }
        public string ItemDetailsAr { get; set; }
        public double ItemsInPackage { get; set; }
        public long TotalItemsInPackages { get; set; }
        public long TotalItemsCount { get; set; }
        public long? TotalItemsCountInWarehouse { get; set; }
        public string RecCreatedByName { get; set; }
        public string RecCreatedBy { get; set; }
        public string RecCreatedDate { get; set; }
        public string RecLastUpdatedBy { get; set; }
        public DateTime RecLastUpdatedDate { get; set; }
    }
}