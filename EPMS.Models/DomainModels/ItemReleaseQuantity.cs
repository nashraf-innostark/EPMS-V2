namespace EPMS.Models.DomainModels
{
    public class ItemReleaseQuantity
    {
        public long ItemReleaseQuantityId { get; set; }
        public long ItemReleaseId { get; set; }
        public long ItemVariationId { get; set; }
        public long WarehouseId { get; set; }
        public long? Quantity { get; set; }

        public virtual ItemRelease ItemRelease { get; set; }
        public virtual ItemVariation ItemVariation { get; set; }
        public virtual Warehouse Warehouse { get; set; }
    }
}
