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
        public bool IsFull { get; set; }
        public string WarehouseSize { get; set; }
        public long? NoOfSpaces { get; set; }
        public string RecCreatedBy { get; set; }
        public System.DateTime RecCreatedDt { get; set; }
        public string RecLastUpdatedBy { get; set; }
        public System.DateTime RecLastUpdatedDt { get; set; }

        public virtual ICollection<Aisle> Aisles { get; set; }
        public virtual Employee Employee { get; set; }
        public virtual ICollection<ItemVariation> ItemVariations { get; set; }
    }
}
