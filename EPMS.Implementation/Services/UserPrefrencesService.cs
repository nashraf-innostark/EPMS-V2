using EPMS.Interfaces.IServices;
using EPMS.Interfaces.Repository;
using EPMS.Models.DomainModels;

namespace EPMS.Implementation.Services
{
    public class UserPrefrencesService:IUserPrefrencesService
    {
        private readonly IUserPrefrencesRepository userPrefrencesRepository;

        public UserPrefrencesService(IUserPrefrencesRepository userPrefrencesRepository)
        {
            this.userPrefrencesRepository = userPrefrencesRepository;
        }

        public UserPrefrence LoadPrefrencesByUserId(string userId)
        {
            return userPrefrencesRepository.GetPrefrencesByUserId(userId);
        }

        public bool AddUpdateCulture(string userId, string culture)
        {
            var userPrefrences = userPrefrencesRepository.GetPrefrencesByUserId(userId);
            if (userPrefrences == null)
            {
                UserPrefrence prefrence=new UserPrefrence {UserId = userId, Culture = culture};
                return AddPrefrences(prefrence);
            }
            userPrefrences.Culture = culture;
            return UpdatePrefrences(userPrefrences);
        }

        public bool AddPrefrences(UserPrefrence prefrence)
        {
            userPrefrencesRepository.Add(prefrence);
            userPrefrencesRepository.SaveChanges();
            return true;
        }

        public bool UpdatePrefrences(UserPrefrence prefrence)
        {
            userPrefrencesRepository.Update(prefrence);
            userPrefrencesRepository.SaveChanges();
            return true;
        }
    }
}
