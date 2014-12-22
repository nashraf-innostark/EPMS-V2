using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EPMS.Models.DomainModels;
using EPMS.Models.RequestModels;
using EPMS.Models.ResponseModels;

namespace EPMS.Interfaces.Repository
{
    public interface IJobTitleRepository : IBaseRepository<JobTitle, long>
    {
        /// <summary>
        /// Get Job Titles by Department Id
        /// </summary>
        /// <param name="deptId"></param>
        /// <returns></returns>
        List<JobTitle> GetJobTitlesByDepartmentId(long deptId); 
        /// <summary>
        /// Get All Job Titles
        /// </summary>
        /// <param name="jobTitleSearchRequest"></param>
        /// <returns></returns>
        JobTitleResponse GetAllJobTitle(JobTitleSearchRequest jobTitleSearchRequest);
    }
}
