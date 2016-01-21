﻿using System;
using System.Collections.Generic;

namespace EPMS.WebModels.WebsiteModels
{
    public class Invoice
    {
        public long InvoiceId { get; set; }
        public string InvoiceNumber { get; set; }
        public long QuotationId { get; set; }
        public string Notes { get; set; }
        public string NotesA { get; set; }
        public string RecCreatedBy { get; set; }
        public string RecCreatedDt { get; set; }
        public string RecLastUpdatedBy { get; set; }
        public DateTime RecLastUpdatedDt { get; set; }
        public string ClientNameE { get; set; }
        public string ClientNameA { get; set; }
        public long CustomerId { get; set; }

        public Quotation Quotation { get; set; }
        public IList<Receipt> Receipts { get; set; }
    }
}
