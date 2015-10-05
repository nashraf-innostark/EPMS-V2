namespace EPMS.WebModels.ViewModels.RFQ
{
    public class RFQCreateViewModel
    {
        public RFQCreateViewModel()
        {
            Rfq = new WebsiteModels.RFQ();
        }
        public WebsiteModels.RFQ Rfq { get; set; }
        public bool FromClient { get; set; }
    }
}