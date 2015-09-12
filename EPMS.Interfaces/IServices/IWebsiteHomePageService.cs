using EPMS.Models.DomainModels;
using EPMS.Models.ResponseModels;

namespace EPMS.Interfaces.IServices
{
    public interface IWebsiteHomePageService
    {
        WebsiteHomePage GetHomePageLogo();
        WebsiteHomeResponse WebsiteHomeResponse();
        bool SaveLogo(WebsiteHomePage homePage);
        bool AddWebsiteLogo(WebsiteHomePage homePage);
        bool UpdateWebsiteLogo(WebsiteHomePage homePage);
    }
}
