namespace EPMS.Web.ViewModels.Partner
{
    public class PartnerViewModel
    {
        public PartnerViewModel()
        {
            Partner = new Models.Partner();
        }
        public Models.Partner Partner { get; set; }
    }
}