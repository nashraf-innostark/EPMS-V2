using System;

namespace EPMS.Models.DomainModels
{
    public class ItemWarehouse
    {
        public long WarehouseId { get; set; }
        public long ItemVariationId { get; set; }
        public long? Quantity { get; set; }
        public string PlaceInWarehouse { get; set; }
        public long? WarehouseDetailId { get; set; }

        public virtual Warehouse Warehouse { get; set; }
        public virtual ItemVariation ItemVariation { get; set; }
        public virtual WarehouseDetail WarehouseDetail { get; set; }
    }
}
