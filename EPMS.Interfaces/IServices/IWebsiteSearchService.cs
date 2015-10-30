using EPMS.Models.RequestModels;
using EPMS.Models.ResponseModels;

namespace EPMS.Interfaces.IServices
{
    public interface IWebsiteSearchService
    {
        WebsiteSearchResultData GetWebsiteSearchResultData(NewsAndArticleSearchRequest newsAndArticleSearchRequest,
            ProductSearchRequest productSearchRequest, WebsiteServiceSearchRequest websiteServiceSearchRequest, string search);
    }
}
