using System.Data.Entity;
using EPMS.Models.DomainModels;

namespace EPMS.Interfaces.Repository
{
    /// <summary>
    /// Company Profile Repository
    /// </summary>
    public interface ICompanyProfileRepository : IBaseRepository<CompanyProfile, long>
    {
        CompanyProfile GetCompanyProfile();
    }
}
