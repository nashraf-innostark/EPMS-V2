using System;
using System.Collections.Generic;
using EPMS.Interfaces.IServices;
using EPMS.Interfaces.Repository;
using EPMS.Models.DomainModels;
using EPMS.Models.RequestModels;
using EPMS.Models.ResponseModels;

namespace EPMS.Implementation.Services
{
    public class JobOfferedService : IJobOfferedService
    {
        private readonly IJobOfferedRepository jobOfferedRepository;

        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="jobOfferedRepository"></param>
        public JobOfferedService(IJobOfferedRepository jobOfferedRepository)
        {
            this.jobOfferedRepository = jobOfferedRepository;
        }

        #endregion
        public IEnumerable<JobOffered> GetAll()
        {
            return jobOfferedRepository.GetAll();
        }

        public JobOffered FindJobOfferedById(long id)
        {
            if (id != null) return jobOfferedRepository.Find((int)id);
            return null;
        }

        public bool AddJobOffered(JobOffered jobOffered)
        {
            try
            {
                jobOfferedRepository.Add(jobOffered);
                jobOfferedRepository.SaveChanges();
                return true;
            }
            catch (Exception exception)
            {
                return false;
            }
        }

        public bool UpdateJobOffered(JobOffered jobOffered)
        {
            try
            {
                jobOfferedRepository.Update(jobOffered);
                jobOfferedRepository.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public List<JobOffered> GetJobsOfferedByJobTitleId(long jobTitleId)
        {
            return jobOfferedRepository.GetJobsOfferedByJobTitleId(jobTitleId);
        }
        
    }
}
