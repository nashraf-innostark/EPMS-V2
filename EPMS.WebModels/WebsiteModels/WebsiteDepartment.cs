using System;

namespace EPMS.WebModels.WebsiteModels
{
    public class WebsiteDepartment
    {
        public long DepartmentId { get; set; }
        public string NameEn { get; set; }
        public string NameAr { get; set; }
        public string ImageName { get; set; }
        public int? DepartmentOrder { get; set; }
        public bool ShowToPublic { get; set; }
        public string Link { get; set; }
        public string Description { get; set; }
        public string RecCreatedBy { get; set; }
        public DateTime RecCreatedDate { get; set; }
        public string RecUpdatedBy { get; set; }
        public DateTime RecUpdatedDate { get; set; }
    }
}