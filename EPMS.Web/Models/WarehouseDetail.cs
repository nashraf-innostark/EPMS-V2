using System;

namespace EPMS.Web.Models
{
    public class WarehouseDetail
    {
        public long WarehouseDetailId { get; set; }
        public long WarehouseId { get; set; }
        public string Name { get; set; }
        public short? NodeLevel { get; set; }
        public int? NoOfSpace { get; set; }
        public long? ParentId { get; set; }
        public string RecCreatedBy { get; set; }
        public DateTime RecCreatedDt { get; set; }
        public string RecLastUpdatedBy { get; set; }
        public DateTime RecLastUpdatedDt { get; set; }
    }
}