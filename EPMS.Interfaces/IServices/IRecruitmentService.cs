using System.Collections.Generic;
using EPMS.Models.DomainModels;

namespace EPMS.Interfaces.IServices
{
    public interface IRecruitmentService
    {
        long Add(JobOffered job);
        JobOffered Find(long id);
        bool Update(JobOffered job);
        bool Delete(long jobId);
        IEnumerable<JobOffered> LoadAllJobs();
    }
}
