using System.Collections.Generic;
using EPMS.Models.DomainModels;
using EPMS.Models.RequestModels;
using EPMS.Models.ResponseModels;

namespace EPMS.Interfaces.Repository
{
    public interface IWebsiteServicesRepository : IBaseRepository<WebsiteService, long>
    {
        IEnumerable<WebsiteService> SearchInWebsiteService(string search);
        WebsiteSearchResponse SearchInWebsiteService(WebsiteServiceSearchRequest serviceSearchRequest, string search);
    }
}
