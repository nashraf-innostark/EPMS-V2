using System;
using System.Collections.Generic;
using EPMS.Interfaces.IServices;
using EPMS.Interfaces.Repository;
using EPMS.Models.DomainModels;
using EPMS.Models.ResponseModels;

namespace EPMS.Implementation.Services
{
    public class JobTitleService : IJobTitleService
    {
        private readonly IJobTitleRepository repository;
        private readonly IJobTitleHistoryRepository jobTitleHistoryRepository;
        private readonly IEmployeeRepository employeeRepository;
        private readonly IJobOfferedRepository jobOfferedRepository;
        private readonly IDepartmentService departmentService;
        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        public JobTitleService(IJobTitleRepository xRepository, IJobTitleHistoryRepository jobTitleHistoryRepository, IEmployeeRepository employeeRepository, IJobOfferedRepository jobOfferedRepository, IDepartmentService departmentService)
        {
            repository = xRepository;
            this.jobTitleHistoryRepository = jobTitleHistoryRepository;
            this.employeeRepository = employeeRepository;
            this.jobOfferedRepository = jobOfferedRepository;
            this.departmentService = departmentService;
        }

        #endregion

        public List<JobTitle> GetJobTitlesByDepartmentId(long deptId)
        {
            return repository.GetJobTitlesByDepartmentId(deptId);
        }

        
        //public JobTitleResponse GetAllJobTitle(JobTitleSearchRequest jobTitleSearchRequest)
        //{
        //    return repository.GetAllJobTitle(jobTitleSearchRequest);
        //}

        public JobTitle FindJobTitleById(long id)
        {
            if (id != null) return repository.Find((int)id);
            return null;
        }

        public JobTitleResponse GetResponseWithJobTitle(long id)
        {
            JobTitleResponse response = new JobTitleResponse();
            response.Departments = departmentService.GetAll();
            if (id > 0)
            {
                response.JobTitle = FindJobTitleById(id);
            }
            return response;
        }
        public JobTitle GetJobTitlesByJobOfferedId (long id)
        {
            if (id > 0) return repository.GetJobOfferedByJobTitleId(id);
            return null;
        }

        public IEnumerable<JobTitle> GetAll()
        {
            return repository.GetAll();
        }

        public bool AddJob(JobTitle jobTitle)
        {
                if (repository.JobTitleExists(jobTitle))
                {
                    throw new InvalidOperationException("Job Title with same name already exists.");
                }
                repository.Add(jobTitle);
                repository.SaveChanges();
                return true;
        }

        public bool UpdateJob(JobTitle jobTitle)
        {
            try
            {
                if (repository.JobTitleExists(jobTitle))
                {
                    throw new InvalidOperationException("Job Title with same name already exists.");
                }
                var tempjobTitle = repository.Find(jobTitle.JobTitleId);
                if (jobTitle.BasicSalary != tempjobTitle.BasicSalary)
                {
                    var jobHistory = new JobTitleHistory
                    {
                        JobTitleId = Convert.ToInt64(jobTitle.JobTitleId),
                        BasicSalary = Convert.ToDouble(jobTitle.BasicSalary),
                        RecCreatedDate = DateTime.Now,
                        RecCreatedBy = jobTitle.RecLastUpdatedBy
                    };
                    jobTitleHistoryRepository.Add(jobHistory);
                    jobTitleHistoryRepository.SaveChanges();
                }
                repository.Update(jobTitle);
                repository.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
               return false;
            }
        }

        public void DeleteJob(JobTitle jobTitle)
        {
            try
            {
                repository.Delete(jobTitle);
                repository.SaveChanges();
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public IEnumerable<Employee> FindEmployeeByJobTitleId(long? jobTitleId)
        {
            return employeeRepository.GetEmployeesByDepartmentId((long)jobTitleId);
        }
    }
}
