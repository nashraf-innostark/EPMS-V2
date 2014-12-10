using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EPMS.Models.DomainModels;
using EPMS.Models.RequestModels;
using EPMS.Models.ResponseModels;

namespace EPMS.Interfaces.IServices
{
    public interface IJobTitleService
    {
        IEnumerable<JobTitle> LoadAll();
        List<JobTitle> GetJobTitlesByDepartmentId(long deptId);
        JobTitleResponse GetAllJobTitle(JobTitleSearchRequest jobTitleSearchRequest);

        JobTitle FindJobTitleById(int? id);

        bool AddJob(JobTitle jobTitle);
    }
}
