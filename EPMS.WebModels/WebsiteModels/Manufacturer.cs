using System;

namespace EPMS.WebModels.WebsiteModels
{
    public class Manufacturer
    {
        public long ManufacturerId { get; set; }
        public string ManufacturerNameEn { get; set; }
        public string ManufacturerNameAr { get; set; }
        public string RecCreatedBy { get; set; }
        public string RecCreatedDt { get; set; }
        public string RecLastUpdatedBy { get; set; }
        public DateTime RecLastUpdatedDt { get; set; }
    }
}