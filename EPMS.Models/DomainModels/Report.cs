using System;
using System.Collections.Generic;

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
        public long? EmployeeId { get; set; }
        public long? CustomerId { get; set; }

        public virtual Customer Customer { get; set; }
        public virtual ICollection<ReportQuotationInvoice> ReportQuotationInvoices { get; set; }
        public virtual AspNetUser AspNetUser { get; set; }
        public virtual Project Project { get; set; }
        public virtual ProjectTask ProjectTask { get; set; }
        public virtual Warehouse Warehouse { get; set; }
        public virtual RfqOrder RfqOrder { get; set; }
        public virtual Employee Employee { get; set; }
        public virtual InventoryItem InventoryItem { get; set; }
        public long? InventoryItemId { get; set; }
        public virtual ICollection<ReportProject> ReportProjects { get; set; }
        public virtual ICollection<ReportProjectTask> ReportProjectTasks { get; set; }
        public virtual ICollection<ReportTaskEmployee> ReportTaskEmployees { get; set; }

        public virtual ICollection<ReportInventoryItem> ReportInventoryItems { get; set; }
    }
}
