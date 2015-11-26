using EPMS.WebModels.WebsiteModels;

namespace EPMS.WebModels.ViewModels.Payment
{
    public class PaypalViewModel
    {
        public Paypal Paypal { get; set; }
        public decimal Amount { get; set; }
        public string Name { get; set; }
        public long InvoiceId { get; set; }
        public int InstallmentNo { get; set; }
    }
}
