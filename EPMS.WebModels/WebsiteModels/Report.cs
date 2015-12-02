﻿using System;

namespace EPMS.WebModels.WebsiteModels
{
    public class Report
    {
        public long ReportId { get; set; }
        public int ReportCategoryId { get; set; }
        public DateTime ReportFromDate { get; set; }
        public DateTime ReportToDate { get; set; }
        public string ReportCreatedBy { get; set; }
        public DateTime ReportCreatedDate { get; set; }
        public long? ProjectId { get; set; }
        public long? TaskId { get; set; }
        public long? WarehouseId { get; set; }
        public long? RfqOrderId { get; set; }
        public long? EmployeeId { get; set; }
        public long? InventoryItemId { get; set; }
        public long? CustomerId { get; set; }


        public string ReportCreatedDateString { get; set; }
        public string ReportFromDateString { get; set; }
        public string ReportToDateString { get; set; }
        public string ReportCategoryItemTitle { get; set; }
        public string ReportCreatedByName { get; set; }
    }
}
