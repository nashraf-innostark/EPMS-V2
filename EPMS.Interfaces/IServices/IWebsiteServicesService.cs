using System.Collections.Generic;
using EPMS.Models.DomainModels;
using EPMS.Models.ResponseModels;

namespace EPMS.Interfaces.IServices
{
    public interface IWebsiteServicesService
    {
        WebsiteService FindWebsiteServiceById(long id);
        IEnumerable<WebsiteService> GetAll();
        bool AddWebsiteService(WebsiteService service);
        bool UpdateWebsiteService(WebsiteService service);
        void DeleteWebsiteService(long serviceId);
        WebsiteServicesCreateResponse LoadWebsiteServices(long id);
    }
}
