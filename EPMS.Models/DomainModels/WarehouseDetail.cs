using System;
using System.Collections.Generic;

namespace EPMS.Models.DomainModels
{
    public class WarehouseDetail
    {
        public long WarehouseDetailId { get; set; }
        public long WarehouseId { get; set; }
        public string NameEn { get; set; }
        public string NameAr { get; set; }
        public short? NodeLevel { get; set; }
        public long? ParentId { get; set; }
        public string RecCreatedBy { get; set; }
        public DateTime RecCreatedDt { get; set; }
        public string RecLastUpdatedBy { get; set; }
        public DateTime RecLastUpdatedDt { get; set; }

        public virtual Warehouse Warehouse { get; set; }
        public virtual ICollection<WarehouseDetail> WarehouseDetails { get; set; }
        public virtual WarehouseDetail ParentDetail { get; set; }
    }
}
