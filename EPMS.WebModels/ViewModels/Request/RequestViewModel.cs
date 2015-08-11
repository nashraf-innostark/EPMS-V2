namespace EPMS.WebModels.ViewModels.Request
{
    public class RequestViewModel
    {
        public RequestViewModel()
        {
            Request = new WebsiteModels.Request();
            RequestDetail = new WebsiteModels.RequestDetail();
            RequestReply = new WebsiteModels.RequestDetail();
        }
        public WebsiteModels.Request Request { get; set; }
        public WebsiteModels.RequestDetail RequestDetail { get; set; }
        public WebsiteModels.RequestDetail RequestReply { get; set; }
        public string RequestDesc { get; set; }
    }
}