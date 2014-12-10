using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EPMS.Interfaces.IServices;
using EPMS.Interfaces.Repository;
using EPMS.Models.DomainModels;
using EPMS.Models.RequestModels;
using EPMS.Models.ResponseModels;

namespace EPMS.Implementation.Services
{
    public class JobTitleService : IJobTitleService
    {
        private readonly IJobTitleRepository iRepository;

        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="xRepository"></param>
        public JobTitleService(IJobTitleRepository xRepository)
        {
            iRepository = xRepository;
        }

        #endregion

        public List<JobTitle> GetJobTitlesByDepartmentId(long deptId)
        {
            return iRepository.GetJobTitlesByDepartmentId(deptId);
        }
        public JobTitleResponse GetAllJobTitle(JobTitleSearchRequest jobTitleSearchRequest)
        {
            return iRepository.GetAllJobTitle(jobTitleSearchRequest);
        }

        public JobTitle FindJobTitleById(int? id)
        {
            return iRepository.FindJobTitleById(id);
        }

        public IEnumerable<JobTitle> LoadAll()
        {
            return iRepository.LoadAll();
        }

        public bool AddJob(JobTitle jobTitle)
        {
            try
            {
                iRepository.Add(jobTitle);
                iRepository.SaveChanges();
                return true;
            }
            catch (Exception exception)
            {
                return false;
            }
        }
    }
}
