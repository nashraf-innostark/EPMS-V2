namespace EPMS.WebModels.ViewModels.RFQ
{
    public class RFQDetailViewModel
    {
        public RFQDetailViewModel()
        {
            Rfq = new WebsiteModels.RFQ();
            Profile = new WebsiteModels.CompanyProfile();
            Customer = new WebsiteModels.Customer();
        }
        public WebsiteModels.RFQ Rfq { get; set; }
        public WebsiteModels.CompanyProfile Profile { get; set; }
        public WebsiteModels.Customer Customer { get; set; }

    }
}
