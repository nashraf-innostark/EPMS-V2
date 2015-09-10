using System;
using EPMS.Interfaces.IServices;
using EPMS.Interfaces.Repository;
using EPMS.Models.DomainModels;

namespace EPMS.Implementation.Services
{
    public class WebsiteUserPreferenceService : IWebsiteUserPreferenceService
    {
        private readonly IWebsiteUserPreferenceRepository userPrefrencesRepository;

        public WebsiteUserPreferenceService(IWebsiteUserPreferenceRepository userPrefrencesRepository)
        {
            this.userPrefrencesRepository = userPrefrencesRepository;
        }

        public WebsiteUserPrefrence LoadPrefrencesByUserId(string userId)
        {
            return userPrefrencesRepository.GetPrefrencesByUserId(userId);
        }

        public bool AddUpdateCulture(string userId, string culture)
        {
            var userPrefrences = userPrefrencesRepository.GetPrefrencesByUserId(userId);
            if (userPrefrences == null)
            {
                WebsiteUserPrefrence prefrence = new WebsiteUserPrefrence { UserId = userId, Culture = culture };
                return AddPrefrences(prefrence);
            }
            userPrefrences.Culture = culture;
            return UpdatePrefrences(userPrefrences);
        }
        public bool AddPrefrences(WebsiteUserPrefrence prefrence)
        {
            try
            {
                userPrefrencesRepository.Add(prefrence);
                userPrefrencesRepository.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool UpdatePrefrences(WebsiteUserPrefrence prefrence)
        {
            try
            {
                userPrefrencesRepository.Update(prefrence);
                userPrefrencesRepository.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
