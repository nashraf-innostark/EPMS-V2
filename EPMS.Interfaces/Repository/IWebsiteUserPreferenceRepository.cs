using EPMS.Models.DomainModels;

namespace EPMS.Interfaces.Repository
{
    public interface IWebsiteUserPreferenceRepository : IBaseRepository<WebsiteUserPrefrence, long>
    {
        WebsiteUserPrefrence GetPrefrencesByUserId(string userId);
    }
}
