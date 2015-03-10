using EPMS.Models.DomainModels;

namespace EPMS.Interfaces.Repository
{
    public interface IJobTitleHistoryRepository : IBaseRepository<JobTitleHistory, long>
    {
        JobTitleHistory GetJobTitleHistoryByJobTitleId(long jobTitleId);
    }
}
