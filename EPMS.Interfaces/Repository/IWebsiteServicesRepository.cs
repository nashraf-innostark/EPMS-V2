using System.Collections.Generic;
using EPMS.Models.DomainModels;

namespace EPMS.Interfaces.Repository
{
    public interface IWebsiteServicesRepository : IBaseRepository<WebsiteService, long>
    {
        IEnumerable<WebsiteService> SearchInWebsiteService(string search);
    }
}
