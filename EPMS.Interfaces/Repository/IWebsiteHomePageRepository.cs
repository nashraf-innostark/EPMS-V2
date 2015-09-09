using EPMS.Models.DomainModels;

namespace EPMS.Interfaces.Repository
{
    public interface IWebsiteHomePageRepository : IBaseRepository<WebsiteHomePage, long>
    {
        WebsiteHomePage GetHomePageLogo();
    }
}
