using System.Collections.Generic;
using EPMS.Models.DomainModels;

namespace EPMS.Interfaces.IServices
{
    public interface IJobApplyService
    {
        IEnumerable<JobApplicant> GetAll();
        JobApplicant FindJobApplicantById(long id);
        bool AddJobApplicant(JobApplicant jobTitle);

        //bool UpdateJobOffered(JobApplicant jobTitle);
        List<JobApplicant> GetJobsOfferedByJobTitleId(long jobTitleId);
    }
}
