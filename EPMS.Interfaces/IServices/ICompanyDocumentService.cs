using EPMS.Models.DomainModels;

namespace EPMS.Interfaces.IServices
{
    public interface ICompanyDocumentService
    {
        /// <summary>
        /// Add Company Document Details
        /// </summary>
        bool AddDetail(CompanyDocumentDetail document);
        /// <summary>
        /// Update Company Document Details
        /// </summary>
        bool UpdateDetail(CompanyDocumentDetail document);

        ///// <summary>
        ///// Get Company Document Details
        ///// </summary>
        //CompanyDocumentDetail GetDetail();
    }
}
