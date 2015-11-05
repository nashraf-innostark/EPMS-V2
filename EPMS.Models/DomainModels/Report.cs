﻿namespace EPMS.Models.DomainModels
{
    public class Report
    {
        public long ReportId { get; set; }
        public long? ProjectId { get; set; }
        public long? TaskId { get; set; }
        public int ReportCategoryId { get; set; }
        public long ReportCategoryItemId { get; set; }
        public System.DateTime ReportFromDate { get; set; }
        public System.DateTime ReportToDate { get; set; }
        public long ReportCreatedBy { get; set; }
        public System.DateTime ReportCreatedDate { get; set; }

        public virtual Employee Employee { get; set; }

        public virtual Project Project { get; set; }
        public virtual ProjectTask ProjectTask { get; set; }
    }
}
