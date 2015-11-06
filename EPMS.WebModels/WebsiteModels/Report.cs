using System;

namespace EPMS.WebModels.WebsiteModels
{
    public class Report
    {
        public long ReportId { get; set; }
        public int ReportCategoryId { get; set; }
        public long ReportCategoryItemId { get; set; }
        public string ReportCategoryItemTitle { get; set; }
        public DateTime ReportFromDate { get; set; }
        public DateTime ReportToDate { get; set; }
        public long ReportCreatedById { get; set; }
        public long ReportCreatedByName { get; set; }
        public DateTime ReportCreatedDate { get; set; }
    }
}
