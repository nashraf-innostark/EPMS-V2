namespace EPMS.Models.DomainModels
{
    public class ItemWarehouse
    {
        public long WarehousrId { get; set; }
        public long ItemVariationId { get; set; }

        public virtual ItemVariation ItemVariation { get; set; }
    }
}
