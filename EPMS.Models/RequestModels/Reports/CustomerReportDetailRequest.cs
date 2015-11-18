using System;

namespace EPMS.Models.RequestModels.Reports
{
    public class CustomerReportDetailRequest
    {
        public long ReportId { get; set; }
        public string RequesterRole { get; set; }
        public string RequesterId { get; set; }
        public bool IsCreate { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}