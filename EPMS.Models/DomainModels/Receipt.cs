using System;

namespace EPMS.Models.DomainModels
{
    public class Receipt
    {
        public long ReceiptId { get; set; }
        public long ReceiptNumber { get; set; }
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

        public virtual Invoice Invoice { get; set; }
        public virtual Paypal Paypal { get; set; }
    }
}
