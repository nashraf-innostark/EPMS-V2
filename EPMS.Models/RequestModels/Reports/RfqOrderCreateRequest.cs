using System;

namespace EPMS.Models.RequestModels.Reports
{
    public class RfqOrderCreateRequest
    {
        public int ReportCategoryId { get; set; }
        public DateTime ReportFromDate { get; set; }
        public DateTime ReportToDate { get; set; }
        public string ReportCreatedBy { get; set; }
        public DateTime ReportCreatedDate { get; set; }
        public long? CustomerId { get; set; }
    }
}
