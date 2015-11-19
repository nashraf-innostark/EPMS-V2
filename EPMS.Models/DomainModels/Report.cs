namespace EPMS.Models.DomainModels
{
    public class Report
    {
        public long ReportId { get; set; }
        public int ReportCategoryId { get; set; }
        public System.DateTime ReportFromDate { get; set; }
        public System.DateTime ReportToDate { get; set; }
        public string ReportCreatedBy { get; set; }
        public System.DateTime ReportCreatedDate { get; set; }
        public long? ProjectId { get; set; }
        public long? TaskId { get; set; }
        public long? WarehouseId { get; set; }
        public long? RfqOrderId { get; set; }

        public virtual AspNetUser AspNetUser { get; set; }
        public virtual Project Project { get; set; }
        public virtual ProjectTask ProjectTask { get; set; }
        public virtual Warehouse Warehouse { get; set; }
        public virtual RfqOrder RfqOrder { get; set; }
    }
}
