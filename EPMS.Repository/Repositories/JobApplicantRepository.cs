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
using EPMS.Repository.BaseRepository;
using Microsoft.Practices.Unity;

namespace EPMS.Repository.Repositories
{
    class JobApplicantRepository : BaseRepository<JobApplicant>, IJobApplicantRepository
    {
        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        public JobApplicantRepository(IUnityContainer container)
            : base(container)
        {
        }

        /// <summary>
        /// Primary database set
        /// </summary>
        protected override IDbSet<JobApplicant> DbSet
        {
            get { return db.JobApplicants; }
        }

        #endregion

        #region Private
        
        private readonly Dictionary<JobApplicantByColumn, Func<JobApplicant, object>> jobApplicantClause =

            new Dictionary<JobApplicantByColumn, Func<JobApplicant, object>>
                    {
                        { JobApplicantByColumn.ApplicantName,  c => c.ApplicantName},
                        { JobApplicantByColumn.ApplicantEmail, c => c.ApplicantEmail},
                        { JobApplicantByColumn.ApplicantMobile, c => c.ApplicantMobile},
                        { JobApplicantByColumn.JobOffered, c => c.JobOffered.JobTitle.JobTitleNameE},
                        {JobApplicantByColumn.Departemnt, c => c.JobOffered.JobTitle.Department.DepartmentNameE}
                    };
        #endregion

        public JobApplicantResponse GetAllJobApplicants(JobApplicantSearchRequest jobApplicantSearchRequest)
        {
            int fromRow = jobApplicantSearchRequest.iDisplayStart;
            int toRow = jobApplicantSearchRequest.iDisplayStart + jobApplicantSearchRequest.iDisplayLength;
            
            Expression<Func<JobApplicant, bool>> query =
                s => ((string.IsNullOrEmpty(jobApplicantSearchRequest.SearchString)) || (s.ApplicantName.Contains(jobApplicantSearchRequest.SearchString)) ||
                    (s.ApplicantEmail.Contains(jobApplicantSearchRequest.SearchString)) || (s.ApplicantMobile.Contains(jobApplicantSearchRequest.SearchString)) ||
                    (s.JobOffered.JobTitle.JobTitleNameE.Contains(jobApplicantSearchRequest.SearchString)) || (s.JobOffered.JobTitle.JobTitleNameA.Contains(jobApplicantSearchRequest.SearchString)) ||
                    (s.JobOffered.JobTitle.Department.DepartmentNameE.Contains(jobApplicantSearchRequest.SearchString)) || (s.JobOffered.JobTitle.Department.DepartmentNameA.Contains(jobApplicantSearchRequest.SearchString)));
            IEnumerable<JobApplicant> jobApplicants = jobApplicantSearchRequest.sSortDir_0=="Asc" ?
                DbSet
                .Where(query).OrderBy(jobApplicantClause[jobApplicantSearchRequest.JobApplicantRequestByColumn]).Skip(fromRow).Take(toRow).ToList()
                                           :
                                           DbSet
                                           .Where(query).OrderByDescending(jobApplicantClause[jobApplicantSearchRequest.JobApplicantRequestByColumn]).Skip(fromRow).Take(toRow).ToList();
            return new JobApplicantResponse { JobApplicants = jobApplicants, TotalCount = DbSet.Count(query) };
        }
    }
}
