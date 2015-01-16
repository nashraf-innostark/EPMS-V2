using EPMS.Interfaces.IServices;
using EPMS.Interfaces.Repository;
using EPMS.Models.DomainModels;

namespace EPMS.Implementation.Services
{
    public class CompanyProfileService : ICompanyProfileService
    {
        private readonly ICompanyProfileRepository profileRepository;

        #region Constructor
        public CompanyProfileService(ICompanyProfileRepository profileRepository)
        {
            this.profileRepository = profileRepository;
        }
        #endregion
        public bool AddDetail(CompanyProfile profile)
        {
            profileRepository.Add(profile);
            profileRepository.SaveChanges();
            return true;
        }

        public bool UpdateDetail(CompanyProfile profile)
        {
            profileRepository.Update(profile);
            profileRepository.SaveChanges();
            return true;
        }

        public CompanyProfile GetDetail()
        {
            return profileRepository.GetCompanyProfile();
        }
    }
}
