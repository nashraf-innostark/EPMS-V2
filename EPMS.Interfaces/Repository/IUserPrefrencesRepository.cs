using EPMS.Models.DomainModels;

namespace EPMS.Interfaces.Repository
{
    public interface IUserPrefrencesRepository : IBaseRepository<UserPrefrence, long>
    {
        UserPrefrence GetPrefrencesByUserId(string userId);
    }
}
