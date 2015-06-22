using System;
using System.Collections.Generic;

namespace EPMS.Models.DomainModels
{
    public class Warehouse
    {
        public long WarehouseId { get; set; }
        public string WarehouseNumber { get; set; }
        public string ManagerName { get; set; }
        public long? WarehouseManager { get; set; }
        public string WarehouseLocation { get; set; }
        public bool IsFull { get; set; }
        public string WarehouseSize { get; set; }
        public long? ParentId { get; set; }
        public string RecCreatedBy { get; set; }
        public DateTime RecCreatedDt { get; set; }
        public string RecLastUpdatedBy { get; set; }
        public DateTime RecLastUpdatedDt { get; set; }

        public virtual Employee Employee { get; set; }
        public virtual ICollection<WarehouseDetail> WarehouseDetails { get; set; }
        public virtual ICollection<ItemVariation> ItemVariations { get; set; }
        public virtual ICollection<ItemWarehouse> ItemWarehouses { get; set; }
        public virtual ICollection<ItemReleaseQuantity> ItemReleaseQuantities { get; set; }
        public virtual ICollection<ItemReleaseQuantityHistory> ItemReleaseQuantityHistories { get; set; }
    }
}
