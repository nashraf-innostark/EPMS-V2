using System;
using System.Collections.Generic;

namespace EPMS.Models.DomainModels
{
    public class Manufacturer
    {
        public long ManufacturerId { get; set; }
        public string ManufacturerNameEn { get; set; }
        public string ManufacturerNameAr { get; set; }
        public string RecCreatedBy { get; set; }
        public DateTime RecCreatedDt { get; set; }
        public string RecLastUpdatedBy { get; set; }
        public DateTime RecLastUpdatedDt { get; set; }

        public virtual ICollection<ItemManufacturer> ItemManufacturers { get; set; }
    }
}
