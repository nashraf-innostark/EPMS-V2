using EPMS.Models.DomainModels;
using EPMS.Models.RequestModels;
using EPMS.Models.ResponseModels;

namespace EPMS.Interfaces.Repository
{
    /// <summary>
    /// Job Applicant Repository
    /// </summary>
    public interface IJobApplicantRepository : IBaseRepository<JobApplicant, long>
    {
        /// <summary>
        /// Get All Job Applicants
        /// </summary>
        JobApplicantResponse GetAllJobApplicants(JobApplicantSearchRequest jobApplicantSearchRequest);
    }
}