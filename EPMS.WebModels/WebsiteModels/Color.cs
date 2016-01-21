using System;

namespace EPMS.WebModels.WebsiteModels
{
    public class Color
    {
        public long ColorId { get; set; }
        public string ColorNameEn { get; set; }
        public string ColorNameAr { get; set; }
        public string ColorCode { get; set; }
        public string RecCreatedBy { get; set; }
        public string RecCreatedDt { get; set; }
        public string RecLastUpdatedBy { get; set; }
        public DateTime RecLastUpdatedDt { get; set; }
    }
}