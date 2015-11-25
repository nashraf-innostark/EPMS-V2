﻿namespace EPMS.WebModels.ViewModels.Invoice
{
    public class InvoiceViewModel
    {
        public InvoiceViewModel()
        {
            Invoice = new WebsiteModels.Invoice();
            Quotation = new WebsiteModels.Quotation();
            CompanyProfile = new WebsiteModels.CompanyProfile();
            Customer = new WebsiteModels.Customer();
        }
        public WebsiteModels.Invoice Invoice { get; set; }
        public WebsiteModels.Quotation Quotation { get; set; }
        public WebsiteModels.QuotationItemDetail QuotationItemDetail { get; set; }
        public WebsiteModels.CompanyProfile CompanyProfile { get; set; }
        public WebsiteModels.Customer Customer { get; set; }
    }
}
