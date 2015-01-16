using EPMS.Models.DomainModels;

namespace EPMS.Interfaces.IServices
{
    public interface ICompanySocialService
    {
        /// <summary>
        /// Add Company Social Details
        /// </summary>
        bool AddDetail(CompanySocialDetail social);

        /// <summary>
        /// Update Company Social Details
        /// </summary>
        bool UpdateDetail(CompanySocialDetail social);
        ///// <summary>
        ///// Get Company Social Details
        ///// </summary>
        //CompanySocialDetail GetDetail();
    }
}
