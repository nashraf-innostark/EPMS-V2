using System;
using System.Collections.Generic;
using EPMS.Interfaces.IServices;
using EPMS.Interfaces.Repository;
using EPMS.Models.DomainModels;

namespace EPMS.Implementation.Services
{
    public class JobApplicantService : IJobApplicantService
    {
        private readonly IJobApplicantRepository jobApplicantRepository;

        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="jobApplicantRepository"></param>
        public JobApplicantService(IJobApplicantRepository jobApplicantRepository)
        {
            this.jobApplicantRepository = jobApplicantRepository;
        }

        #endregion

        public IEnumerable<JobApplicant> GetAll()
        {
            return jobApplicantRepository.GetAll();
        }

        public JobApplicant FindJobApplicantById(long id)
        {
            return jobApplicantRepository.Find((int)id);
        }

        public bool AddJobApplicant(JobApplicant jobApplicant)
        {
            try
            {
                jobApplicantRepository.Add(jobApplicant);
                jobApplicantRepository.SaveChanges();
                return true;
            }
            catch (Exception exception)
            {
                return false;
            }
        }
    }
}
