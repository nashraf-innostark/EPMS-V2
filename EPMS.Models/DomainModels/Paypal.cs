using System.Collections.Generic;

namespace EPMS.Models.DomainModels
{
    public class Paypal
    {
        public long PaypalId { get; set; }
        public string TransactionId { get; set; }
        public string CurrencyCode { get; set; }

        public virtual ICollection<Receipt> Receipts { get; set; }
    }
}
