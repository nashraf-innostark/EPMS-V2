using System;
using System.Collections.Generic;
using EPMS.Interfaces.IServices;
using EPMS.Interfaces.Repository;
using EPMS.Models.DomainModels;
using EPMS.Models.RequestModels;
using EPMS.Models.ResponseModels;

namespace EPMS.Implementation.Services
{
    public class JobTitleService : IJobTitleService
    {
        private readonly IJobTitleRepository repository;

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
        public JobTitleResponse GetAllJobTitle(JobTitleSearchRequest jobTitleSearchRequest)
        {
            return repository.GetAllJobTitle(jobTitleSearchRequest);
        }

        public JobTitle FindJobTitleById(long id)
        {
            return repository.Find(id);
        }

        public IEnumerable<JobTitle> GetAll()
        {
            return repository.GetAll();
        }

        public bool AddJob(JobTitle jobTitle)
        {
            try
            {
                repository.Add(jobTitle);
                repository.SaveChanges();
                return true;
            }
            catch (Exception exception)
            {
                return false;
            }
        }

        public bool UpdateJob(JobTitle jobTitle)
        {
            try
            {
                repository.Update(jobTitle);
                repository.SaveChanges();
                return true;
            }
            catch (Exception)
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
    }
}
