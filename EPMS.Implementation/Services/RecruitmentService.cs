using System;
using System.Collections.Generic;
using EPMS.Interfaces.IServices;
using EPMS.Interfaces.Repository;
using EPMS.Models.DomainModels;

namespace EPMS.Implementation.Services
{
    public class RecruitmentService:IRecruitmentService
    {
        private readonly IRecruitmentRepository repository;
        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="xRepository"></param>
        public RecruitmentService(IRecruitmentRepository xRepository)
        {
            repository = xRepository;
        }

        #endregion
        public long Add(JobOffered job)
        {
            try
            {
                repository.Add(job);
                return job.JobOfferedId;
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public JobOffered Find(long id)
        {
            return repository.Find(id);
        }

        public bool Update(JobOffered job)
        {
            try
            {
                repository.Update(job);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool Delete(long jobId)
        {
            try
            {
                var job = Find(jobId);
                if (job!=null)
                {
                    repository.Delete(job);
                    return true;
                }
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public IEnumerable<JobOffered> LoadAllJobs()
        {
            return repository.GetAll();
        }
    }
}
