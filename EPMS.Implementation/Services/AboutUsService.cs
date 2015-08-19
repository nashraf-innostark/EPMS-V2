using EPMS.Interfaces.IServices;
using EPMS.Interfaces.Repository;
using EPMS.Models.DomainModels;

namespace EPMS.Implementation.Services
{
    public class AboutUsService : IAboutUsService
    {

        #region Private

        private readonly IAboutUsRepository aboutUsRepository;

        #endregion

        #region Constructor

        public AboutUsService(IAboutUsRepository aboutUsRepository)
        {
            this.aboutUsRepository = aboutUsRepository;
        }

        #endregion

        #region Public
        public bool AddDetail(AboutUs aboutUs)
        {
            aboutUsRepository.Add(aboutUs);
            aboutUsRepository.SaveChanges();
            return true;
        }

        public bool UpdateDetail(AboutUs aboutUs)
        {
            aboutUsRepository.Update(aboutUs);
            aboutUsRepository.SaveChanges();
            return true;
        }

        public AboutUs GetDetail()
        {
            return aboutUsRepository.GetAboutUs();
        }
        #endregion
    }
}
