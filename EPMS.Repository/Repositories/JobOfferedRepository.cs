using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using EPMS.Interfaces.Repository;
using EPMS.Models.DomainModels;
using Microsoft.Practices.Unity;
using EPMS.Repository.BaseRepository;


namespace EPMS.Repository.Repositories
{
    public sealed class JobOfferedRepository : BaseRepository<JobOffered>, IJobOfferedRepository
    {
        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        public JobOfferedRepository(IUnityContainer container)
            : base(container)
        {
        }

        /// <summary>
        /// Primary database set
        /// </summary>
        protected override IDbSet<JobOffered> DbSet
        {
            get { return db.JobsOffered; }
        }

        #endregion

        public List<JobOffered> GetJobsOfferedByJobTitleId(long jobTitleId)
        {
            return DbSet.Where(s => s.JobTitleId == jobTitleId).ToList();
        }
    }
}
