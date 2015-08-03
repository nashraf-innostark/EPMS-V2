namespace EPMS.Models.DomainModels
{
    public class ItemManufacturer
    {
        public long ItemVariationId { get; set; }
        public long ManufacturerId { get; set; }
        public string Price { get; set; }
        public long? Quantity { get; set; }
        public virtual ItemVariation ItemVariation { get; set; }
        public virtual Vendor Vendor { get; set; }
    }
}
