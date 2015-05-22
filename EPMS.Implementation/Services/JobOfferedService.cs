using System;
using System.Collections.Generic;
using EPMS.Interfaces.IServices;
using EPMS.Interfaces.Repository;
using EPMS.Models.DomainModels;
using EPMS.Models.ResponseModels;

namespace EPMS.Implementation.Services
{
    public class JobOfferedService : IJobOfferedService
    {
        private readonly IJobOfferedRepository jobOfferedRepository;
        private readonly IJobTitleRepository jobTitleRepository;
        private readonly IDepartmentService departmentService;

        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="jobOfferedRepository"></param>
        public JobOfferedService(IJobOfferedRepository jobOfferedRepository, IJobTitleRepository jobTitleRepository, IDepartmentService departmentService)
        {
            this.jobOfferedRepository = jobOfferedRepository;
            this.jobTitleRepository = jobTitleRepository;
            this.departmentService = departmentService;
        }

        #endregion
        public IEnumerable<JobOffered> GetAll()
        {
            return jobOfferedRepository.GetAll();
        }

        public IEnumerable<JobOffered> GetRecentJobOffereds()
        {
            return jobOfferedRepository.GetRecentJobOffereds();
        }

        public JobOffered FindJobOfferedById(long id)
        {
            if (id != null) return jobOfferedRepository.Find((int)id);
            return null;
        }

        public JobApplicantResponse GetJobOfferedResponse(long id)
        {
            JobApplicantResponse response = new JobApplicantResponse
            {
                JobOffered = FindJobOfferedById(id),
                JobTitles = jobTitleRepository.GetAll(),
                Departments = departmentService.GetAll()
            };
            return response;
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
