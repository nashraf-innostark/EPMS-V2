namespace EPMS.Web.Models
{
    public class PhysicalCountModel
    {
        public long PCId { get; set; }
        public long ItemVariationId { get; set; }
        public long WarehouseId { get; set; }
        public long NoOfPackagesInWarehouse { get; set; }
        public long NoOfItemInWarehouse { get; set; }
        public string ItemDetailsEn { get; set; }
        public string ItemDetailsAr { get; set; }
        public long ItemsInPackage { get; set; }
        public long TotalItemsInPackages { get; set; }
        public long TotalItemsCount { get; set; }
        public string RecCreatedBy { get; set; }
        public System.DateTime RecCreatedDate { get; set; }
        public string RecLastUpdatedBy { get; set; }
        public System.DateTime RecLastUpdatedDate { get; set; }
    }
}