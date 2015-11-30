using System;

namespace EPMS.Models.RequestModels.Reports
{
    public class ProjectReportCreateOrDetailsRequest
    {
        public long ReportId { get; set; }
        public long ProjectId { get; set; }
        public string RequesterRole { get; set; }
        public string RequesterId { get; set; }
        public bool IsCreate { get; set; }
        public DateTime ReportCreatedDate { get; set; }
    }
    public class InventoryItemReportCreateOrDetailsRequest
    {
        public long ReportId { get; set; }
        public long ItemId { get; set; }
        public string RequesterRole { get; set; }
        public string RequesterId { get; set; }
        public bool IsCreate { get; set; }
    }
    public class QOReportCreateOrDetailsRequest
    {
        public long ReportId { get; set; }
        public long CustomerId { get; set; }
        public string RequesterRole { get; set; }
        public string RequesterId { get; set; }
        public bool IsCreate { get; set; }
        public DateTime From { get; set; }
        public DateTime To { get; set; }
    }
    public class WarehouseReportCreateOrDetailsRequest
    {
        public long ReportId { get; set; }
        public long WarehouseId { get; set; }
        public string RequesterRole { get; set; }
        public string RequesterId { get; set; }
        public bool IsCreate { get; set; }
        public DateTime ReportCreatedDate { get; set; }
    }
}
