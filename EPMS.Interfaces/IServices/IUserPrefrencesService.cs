using EPMS.Models.DomainModels;

namespace EPMS.Interfaces.IServices
{
    public interface IUserPrefrencesService
    {
        UserPrefrence LoadPrefrencesByUserId(string userId);
        bool AddUpdateCulture(string userId, string culture);
    }
}
