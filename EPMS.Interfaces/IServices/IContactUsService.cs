using EPMS.Models.DomainModels;

namespace EPMS.Interfaces.IServices
{
    public interface  IContactUsService
    {
        /// <summary>
        /// Add ContactUs Details
        /// </summary>
        bool AddDetail(ContactUs contactUs);
        /// <summary>
        /// Update ContactUs Details
        /// </summary>
        bool UpdateDetail(ContactUs contactUs);

        /// <summary>
        /// Get ContactUs Details
        /// </summary>
        ContactUs GetDetail();
    }
}
