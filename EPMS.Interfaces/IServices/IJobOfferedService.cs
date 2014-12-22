using System.Collections.Generic;
using EPMS.Models.DomainModels;
using EPMS.Models.RequestModels;
using EPMS.Models.ResponseModels;

namespace EPMS.Interfaces.IServices
{
    public interface IJobOfferedService
    {
        IEnumerable<JobOffered> GetAll();
        JobOfferedResponse GetAllJobTitle(JobOfferedSearchRequest jobOfferedSearchRequest);
        JobOffered FindJobOfferedById(long id);
        bool AddJobOffered(JobOffered jobTitle);
        bool UpdateJobOffered(JobOffered jobTitle);
        List<JobOffered> GetJobsOfferedByJobTitleId(long jobTitleId);
    }
}
