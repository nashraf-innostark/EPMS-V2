﻿using System;
using System.Collections.Generic;
using EPMS.Interfaces.IServices;
using EPMS.Interfaces.Repository;
using EPMS.Models.DomainModels;

namespace EPMS.Implementation.Services
{
    public class JobTitleService : IJobTitleService
    {
        private readonly IJobTitleRepository repository;
        private readonly IEmployeeRepository employeeRepository;

        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="xRepository"></param>
        public JobTitleService(IJobTitleRepository xRepository)
        {
            repository = xRepository;
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
            if (repository.JobTitleExists(jobTitle))
            {
                throw new InvalidOperationException("Job Title with same name already exists.");
            }
            repository.Update(jobTitle);
            repository.SaveChanges();
            return true;
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
