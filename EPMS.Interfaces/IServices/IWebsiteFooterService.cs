using EPMS.Models.MenuModels;

namespace EPMS.Interfaces.IServices
{
    public interface IWebsiteFooterService
    {
        WebsiteFooterMenuModel LoadFooterMenu();
    }
}
