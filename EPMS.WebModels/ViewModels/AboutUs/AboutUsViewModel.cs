namespace EPMS.WebModels.ViewModels.AboutUs
{
    public class AboutUsViewModel
    {
        public AboutUsViewModel()
        {
            AboutUs = new WebsiteModels.AboutUs();
        }
        public WebsiteModels.AboutUs AboutUs { get; set; }
    }
}
