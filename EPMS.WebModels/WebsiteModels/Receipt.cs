using System;

namespace EPMS.WebModels.WebsiteModels
{
    public class Receipt
    {
        public long ReceiptId { get; set; }
        public string ReceiptNumber { get; set; }
        public long InvoiceId { get; set; }
        public decimal AmountPaid { get; set; }
        public int InstallmentNumber { get; set; }
        public string RecCreatedBy { get; set; }
        public DateTime RecCreatedDt { get; set; }
        public string RecLastUpdatedBy { get; set; }
        public DateTime RecLastUpdatedDt { get; set; }
        public short PaymentType { get; set; }
        public bool IsPaid { get; set; }
        public long? PaypalId { get; set; }
        public string OrderNumber { get; set; }
        public decimal AmountPaidTillNow { get; set; }
        public decimal AmountLeft { get; set; }
        public string InvoiceNumber { get; set; }
        public string CustomerNameE { get; set; }
        public string CustomerNameA { get; set; }
        public long CustomerId { get; set; }
    }
}
