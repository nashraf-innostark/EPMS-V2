namespace EPMS.Web.Models
{
    public class ItemReleaseQuantity
    {
        public long ItemReleaseQuantityId { get; set; }
        public long ItemReleaseId { get; set; }
        public long ItemVariationId { get; set; }
        public long WarehouseId { get; set; }
        public long? Quantity { get; set; }

        public virtual Warehouse Warehouse { get; set; }
    }
}