﻿namespace EPMS.Web.Models
{
    public class ItemWarehouse
    {
        public long WarehousrId { get; set; }
        public long ItemVariationId { get; set; }
        public long? Quantity { get; set; }
        public string PlaceInWarehouse { get; set; }
        public string WarehouseNumber { get; set; }
        public string WarehouseNo { get; set; }
    }
}