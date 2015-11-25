namespace EPMS.WebModels.ViewModels.Receipt
{
    public class ReceiptViewModel
    {
        public ReceiptViewModel()
        {
            Receipt = new WebsiteModels.Receipt();
            Invoice = new WebsiteModels.Invoice();
            Quotation = new WebsiteModels.Quotation();
            CompanyProfile = new WebsiteModels.CompanyProfile();
            Customer = new WebsiteModels.Customer();
        }
        public WebsiteModels.Receipt Receipt { get; set; }
        public WebsiteModels.Invoice Invoice { get; set; }
        public WebsiteModels.Quotation Quotation { get; set; }
        public WebsiteModels.CompanyProfile CompanyProfile { get; set; }
        public WebsiteModels.Customer Customer { get; set; }

    }
}
