namespace EPMS.WebModels.ViewModels.RFQ
{
    public class RFQDetailViewModel
    {
        public RFQDetailViewModel()
        {
            Rfq = new WebsiteModels.RFQ();
            Profile = new WebsiteModels.CompanyProfile();
        }
        public WebsiteModels.RFQ Rfq { get; set; }
        public WebsiteModels.CompanyProfile Profile { get; set; }

    }
}
