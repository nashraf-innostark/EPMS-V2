namespace EPMS.WebModels.ViewModels.Quotation
{
    public class QuotationDetailViewModel
    {
        public WebsiteModels.CompanyProfile Profile { get; set; }
        public WebsiteModels.Customer Customer { get; set; }
        public WebsiteModels.Quotation Quotation { get; set; }
        public WebsiteModels.Order Order { get; set; }
    }
}