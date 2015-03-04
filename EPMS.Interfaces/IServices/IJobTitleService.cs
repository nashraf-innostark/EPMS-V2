using System.Collections.Generic;
using EPMS.Models.DomainModels;

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
        IEnumerable<Employee> FindEmployeeByJobTitleId(long? jobTitleId);
        JobTitle GetJobTitlesByJobOfferedId(long jobOfferedId);
    }
}
