using System;

namespace EPMS.WebModels.WebsiteModels
{
    public class Size
    {
        public long SizeId { get; set; }
        public string SizeNameEn { get; set; }
        public string SizeNameAr { get; set; }
        public string RecCreatedBy { get; set; }
        public DateTime RecCreatedDt { get; set; }
        public string RecLastUpdatedBy { get; set; }
        public DateTime RecLastUpdatedDt { get; set; }
    }
}