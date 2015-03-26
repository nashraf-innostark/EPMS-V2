using System.Collections.Generic;
using EPMS.Models.DomainModels;
using EPMS.Models.ResponseModels;

namespace EPMS.Interfaces.IServices
{
    public interface IJobOfferedService
    {
        IEnumerable<JobOffered> GetAll();
        IEnumerable<JobOffered> GetRecentJobOffereds();
        JobApplicantResponse GetJobOfferedResponse(long id);
        JobOffered FindJobOfferedById(long id);
        bool AddJobOffered(JobOffered jobTitle);
        bool UpdateJobOffered(JobOffered jobTitle);
        List<JobOffered> GetJobsOfferedByJobTitleId(long jobTitleId);
        
    }
}
