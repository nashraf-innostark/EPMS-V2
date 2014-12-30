using System.Collections.Generic;
using EPMS.Models.DomainModels;
using EPMS.Models.RequestModels;
using EPMS.Models.ResponseModels;

namespace EPMS.Interfaces.Repository
{
    public interface IJobOfferedRepository : IBaseRepository<JobOffered, long>
    {
        List<JobOffered> GetJobsOfferedByJobTitleId(long deptId);
        JobOfferedResponse GetAllJobsOffered(JobOfferedSearchRequest jobOfferedSearchRequest);
    }
}
