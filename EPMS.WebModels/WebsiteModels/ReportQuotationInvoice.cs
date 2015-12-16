﻿using System.Collections.Generic;
using EPMS.Models.DomainModels;

namespace EPMS.WebModels.WebsiteModels
{
    public class ReportQuotationInvoice
    {
        public long ReportQuotationInvoiceId { get; set; }
        public long ReportId { get; set; }
        public long EmployeeId { get; set; }
        public string EmployeeNameA { get; set; }
        public string EmployeeNameE { get; set; }
        public int NoOfQuotations { get; set; }
        public int NoOfInvoices { get; set; }
        public string ReportFromDateString { get; set; }
        public string ReportToDateString { get; set; }

        public virtual ICollection<ReportQuotationInvoiceItem> ReportQuotationInvoiceItems { get; set; }
    }
}