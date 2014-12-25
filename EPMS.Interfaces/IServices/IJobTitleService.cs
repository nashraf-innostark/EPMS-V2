using System.Collections.Generic;
using EPMS.Models.DomainModels;
using EPMS.Models.RequestModels;
using EPMS.Models.ResponseModels;

namespace EPMS.Interfaces.IServices
{
    public interface IJobTitleService
    {
        IEnumerable<JobTitle> GetAll();
        List<JobTitle> GetJobTitlesByDepartmentId(long deptId);
        //JobTitleResponse GetAllJobTitle(JobTitleSearchRequest jobTitleSearchRequest);

        JobTitle FindJobTitleById(long id);

        bool AddJob(JobTitle jobTitle);
        bool UpdateJob(JobTitle jobTitle);
        void DeleteJob(JobTitle jobTitle);

    }
}
