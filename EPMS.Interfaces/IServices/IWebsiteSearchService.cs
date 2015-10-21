using EPMS.Models.ResponseModels;

namespace EPMS.Interfaces.IServices
{
    public interface IWebsiteSearchService
    {
        WebsiteSearchResultData GetWebsiteSearchResultData(string search);
    }
}
