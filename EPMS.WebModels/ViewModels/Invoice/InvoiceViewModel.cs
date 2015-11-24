namespace EPMS.WebModels.ViewModels.Invoice
{
    public class InvoiceViewModel
    {
        public WebsiteModels.Invoice Invoice { get; set; }
        public WebsiteModels.Quotation Quotation { get; set; }
        public WebsiteModels.QuotationItemDetail QuotationItemDetail { get; set; }
        public WebsiteModels.CompanyProfile CompanyProfile { get; set; }
        public WebsiteModels.Customer Customer { get; set; }
    }
}
