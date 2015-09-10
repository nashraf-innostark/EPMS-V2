using EPMS.Models.DomainModels;

namespace EPMS.Interfaces.IServices
{
    public interface IWebsiteUserPreferenceService
    {
        WebsiteUserPrefrence LoadPrefrencesByUserId(string userId);
        bool AddUpdateCulture(string userId, string culture);
    }
}
