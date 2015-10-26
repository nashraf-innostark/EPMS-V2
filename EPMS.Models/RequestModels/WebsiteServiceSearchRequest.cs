namespace EPMS.Models.RequestModels
{
    public class WebsiteServiceSearchRequest : GetPagedListRequest
    {
        public long Id { get; set; }
        public string From { get; set; }
    }
}
