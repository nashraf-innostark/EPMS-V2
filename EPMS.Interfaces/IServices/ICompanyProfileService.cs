using System.Collections.Generic;
using System.Data.Entity;
using EPMS.Models.DomainModels;

namespace EPMS.Interfaces.IServices
{
    public interface ICompanyProfileService
    {
        /// <summary>
        /// Add Company Profile Details
        /// </summary>
        bool AddDetail(CompanyProfile profile);
        /// <summary>
        /// Update Company Profile Details
        /// </summary>
        bool UpdateDetail(CompanyProfile profile);

        /// <summary>
        /// Get Company Profile Details
        /// </summary>
        CompanyProfile GetDetail();
    }
}
