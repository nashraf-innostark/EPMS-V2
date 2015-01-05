using System.Collections.Generic;
using EPMS.Models.DomainModels;
using EPMS.Models.RequestModels;
using EPMS.Models.ResponseModels;

namespace EPMS.Interfaces.IServices
{
    public interface IJobApplicantService
    {
        IEnumerable<JobApplicant> GetAll();
        /// <summary>
        /// Get all Job Applicants
        /// </summary>
        JobApplicantResponse GetJobApplicantList(JobApplicantSearchRequest jobApplicantSearchRequest);
        JobApplicant FindJobApplicantById(long id);
        bool AddJobApplicant(JobApplicant jobApplicant);

        //bool UpdateJobOffered(JobApplicant jobTitle);
        //List<JobApplicant> GetJobsApplicantsByJobOfferedId(long jobOfferedId;
    }
}
