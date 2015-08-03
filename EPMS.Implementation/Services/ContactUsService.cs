using EPMS.Interfaces.IServices;
using EPMS.Interfaces.Repository;
using EPMS.Models.DomainModels;

namespace EPMS.Implementation.Services
{
    public class ContactUsService : IContactUsService
    {
        #region Private

        private readonly IContactUsRepository contactUsRepository;

        #endregion

        #region Constructor
        
        public ContactUsService(IContactUsRepository contactUsRepository)
        {
            this.contactUsRepository = contactUsRepository;
        }

        #endregion

        #region Public

        public bool AddDetail(ContactUs contactUs)
        {
            contactUsRepository.Add(contactUs);
            contactUsRepository.SaveChanges();
            return true;
        }

        public bool UpdateDetail(ContactUs contactUs)
        {
            contactUsRepository.Update(contactUs);
            contactUsRepository.SaveChanges();
            return true;
        }

        public ContactUs GetDetail()
        {
            return contactUsRepository.GetContactUs();
        }

        #endregion
    }
}
