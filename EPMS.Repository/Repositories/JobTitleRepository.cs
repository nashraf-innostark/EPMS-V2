using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
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
                        { JobTitleByColumn.JobId, c => c.JobId},
                        { JobTitleByColumn.JobTitleNameE,  c => c.JobTitleNameE},
                        { JobTitleByColumn.JobTitleNameA, c => c.JobTitleNameA},
                        { JobTitleByColumn.JobDescriptionE, c => c.JobDescriptionE},
                        { JobTitleByColumn.JobDescriptionA, c => c.JobDescriptionA}
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
                s => (((jobTitleSearchRequest.JobId == 0) || s.JobId == jobTitleSearchRequest.JobId
                    || s.JobId.Equals(jobTitleSearchRequest.JobId)) &&
                    (string.IsNullOrEmpty(jobTitleSearchRequest.JobTitleNameE)
                    || (s.JobTitleNameE.Contains(jobTitleSearchRequest.JobTitleNameE))));

            IEnumerable<JobTitle> jobTitles = jobTitleSearchRequest.IsAsc ?
                DbSet
                .Where(query).OrderBy(jobTitleClause[jobTitleSearchRequest.JobTitleByColumn]).Skip(fromRow).Take(toRow).ToList()
                                           :
                                           DbSet
                                           .Where(query).OrderByDescending(jobTitleClause[jobTitleSearchRequest.JobTitleByColumn]).Skip(fromRow).Take(toRow).ToList();
            return new JobTitleResponse { JobTitles = jobTitles, TotalCount = DbSet.Count(query) };
        }

        /// <summary>
        /// Find Job by Job ID
        /// </summary>
        /// <param name="id">Job ID</param>
        /// <returns></returns>
        public JobTitle FindJobTitleById(int? id)
        {
            return DbSet.FirstOrDefault(jobId => jobId.JobId == id);
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

        public IEnumerable<JobTitle> LoadAll()
        {
            return DbSet.ToList();
        }
    }
}
