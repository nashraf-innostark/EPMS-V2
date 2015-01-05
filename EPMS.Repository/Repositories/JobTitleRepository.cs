using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using EPMS.Interfaces.Repository;
using EPMS.Models.Common;
using EPMS.Models.DomainModels;
using EPMS.Models.RequestModels;
using EPMS.Models.ResponseModels;
using Microsoft.Practices.Unity;
using EPMS.Repository.BaseRepository;

namespace EPMS.Repository.Repositories
{
    public class JobTitleRepository : BaseRepository<JobTitle>, IJobTitleRepository
    {
        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        public JobTitleRepository(IUnityContainer container)
            : base(container)
        {
        }

        /// <summary>
        /// Primary database set
        /// </summary>
        protected override IDbSet<JobTitle> DbSet
        {
            get { return db.JobTitleses; }
        }

        #endregion

        #region Private

        /// <summary>
        /// Order by Column Names Dictionary statements
        /// </summary>
        private readonly Dictionary<JobTitleByColumn, Func<JobTitle, object>> jobTitleClause =

            new Dictionary<JobTitleByColumn, Func<JobTitle, object>>
                    {
                        { JobTitleByColumn.JobTitleId, c => c.JobTitleId},
                        { JobTitleByColumn.BasicSalary, c => c.BasicSalary}
                    };
        #endregion

        /// <summary>
        /// Get all job Titles with filters if any
        /// </summary>
        /// <param name="jobTitleSearchRequest"></param>
        /// <returns>Job Response</returns>
        public JobTitleResponse GetAllJobTitle(JobTitleSearchRequest jobTitleSearchRequest)
        {
            int fromRow = (jobTitleSearchRequest.PageNo - 1) * jobTitleSearchRequest.PageSize;
            int toRow = jobTitleSearchRequest.PageSize;

            Expression<Func<JobTitle, bool>> query =
                s => (((jobTitleSearchRequest.JobTitleId == 0) || s.JobTitleId == jobTitleSearchRequest.JobTitleId
                    || s.JobTitleId.Equals(jobTitleSearchRequest.JobTitleId)) &&
                    (string.IsNullOrEmpty(jobTitleSearchRequest.JobTitleName)
                    || (s.JobTitleNameE.Contains(jobTitleSearchRequest.JobTitleName))));

            IEnumerable<JobTitle> jobTitles = jobTitleSearchRequest.IsAsc ?
                DbSet
                .Where(query).OrderBy(jobTitleClause[jobTitleSearchRequest.JobTitleByColumn]).Skip(fromRow).Take(toRow).ToList()
                                           :
                                           DbSet
                                           .Where(query).OrderByDescending(jobTitleClause[jobTitleSearchRequest.JobTitleByColumn]).Skip(fromRow).Take(toRow).ToList();
            return new JobTitleResponse { JobTitles = jobTitles, TotalCount = DbSet.Count(query) };
        }

        /// <summary>
        /// Find Job by Department ID
        /// </summary>
        /// <param name="deptId">Department ID</param>
        /// <returns></returns>
        public List<JobTitle> GetJobTitlesByDepartmentId(long deptId)
        {
            return DbSet.Where(s => s.DepartmentId == deptId).ToList();
        }

        /// <summary>
        /// Checks if Job Title Arabic and English Name already exists
        /// </summary>
        public bool JobTitleExists(JobTitle jobTitle)
        {
            if (jobTitle.JobTitleId > 0) //Alread saved in system
            {
                return DbSet.Any(
                    jt =>
                        jobTitle.JobTitleId != jt.JobTitleId &&
                        (jt.JobTitleNameE == jobTitle.JobTitleNameE || jt.JobTitleNameA == jobTitle.JobTitleNameA) && (jt.DepartmentId == jobTitle.DepartmentId));
            }

            return DbSet.Any(
                    jt =>
                        (jt.JobTitleNameE == jobTitle.JobTitleNameE || jt.JobTitleNameA == jobTitle.JobTitleNameA) && (jt.DepartmentId == jobTitle.DepartmentId));
        }

        public IQueryable<JobTitle> GetEmployeesByDepartment(int id)
        {
            return
                DbSet.Where(x => x.DepartmentId == id);
        }
    }
}
