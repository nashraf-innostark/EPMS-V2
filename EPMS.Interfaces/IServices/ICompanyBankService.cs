using EPMS.Models.DomainModels;

namespace EPMS.Interfaces.IServices
{
    public interface ICompanyBankService
    {
        /// <summary>
        /// Add Company bank Details
        /// </summary>
        bool AddDetail(CompanyBankDetail bank);

        /// <summary>
        /// Update Company bank Details
        /// </summary>
        bool UpdateDetail(CompanyBankDetail bank);
        ///// <summary>
        ///// Get Company bank Details
        ///// </summary>
        //CompanyBankDetail GetDetail();
    }
}
