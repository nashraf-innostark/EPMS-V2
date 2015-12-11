using EPMS.Models.DomainModels;

namespace EPMS.Models.ResponseModels
{
    public class WebsiteHomePageResponse
    {
        public WebsiteHomePageResponse()
        {
            HomePage = new WebsiteHomePage();
            MetaTagsResponse = new MetaTagsResponse();
        }
        public WebsiteHomePage HomePage { get; set; }
        public MetaTagsResponse MetaTagsResponse { get; set; }
    }
}
