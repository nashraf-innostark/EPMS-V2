namespace EPMS.Models.DomainModels
{
    public class PhysicalCount
    {
        public long PCId { get; set; }
        public long ItemVariationId { get; set; }
        public long WarehouseId { get; set; }
        public long NoOfPackagesInWarehouse { get; set; }
        public long NoOfItemInWarehouse { get; set; }
        public string RecCreatedBy { get; set; }
        public System.DateTime RecCreatedDate { get; set; }
        public string RecLastUpdatedBy { get; set; }
        public System.DateTime RecLastUpdatedDate { get; set; }

        public virtual ItemVariation ItemVariation { get; set; }
        public virtual Warehouse Warehouse { get; set; }
    }
}
