namespace EPMS.WebModels.ViewModels.Partner
{
    public class PartnerViewModel
    {
        public PartnerViewModel()
        {
            Partner = new WebsiteModels.Partner();
        }
        public WebsiteModels.Partner Partner { get; set; }
    }
}