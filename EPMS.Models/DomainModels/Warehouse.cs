using System;
using System.Collections.Generic;

namespace EPMS.Models.DomainModels
{
    public class Warehouse
    {
        public long WarehouseId { get; set; }
        public string WarehouseNumber { get; set; }
        public long? WarehouseManager { get; set; }
        public string WarehouseLocation { get; set; }
        public string RecCreatedBy { get; set; }
        public DateTime RecCreatedDt { get; set; }
        public string RecLastUpdatedBy { get; set; }
        public DateTime RecLastUpdatedDt { get; set; }

        public virtual ICollection<Aisle> Aisles { get; set; }
        public virtual Employee Employee { get; set; }
        public virtual ICollection<ItemVariation> ItemVariations { get; set; }
    }
}
