using EPMS.Models.DomainModels;

namespace EPMS.Interfaces.IServices
{
    public interface IAboutUsService
    {
        /// <summary>
        /// Add AboutUs Details
        /// </summary>
        bool AddDetail(AboutUs aboutUs);
        /// <summary>
        /// Update AboutUs Details
        /// </summary>
        bool UpdateDetail(AboutUs aboutUs);

        /// <summary>
        /// Get AboutUs Details
        /// </summary>
        AboutUs GetDetail();

        AboutUs GetDetailForWebsite();
    }
}
