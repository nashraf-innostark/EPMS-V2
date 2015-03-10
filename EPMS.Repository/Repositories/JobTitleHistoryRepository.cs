using System.Data.Entity;
using System.Linq;
using EPMS.Interfaces.Repository;
using EPMS.Models.DomainModels;
using EPMS.Repository.BaseRepository;
using Microsoft.Practices.Unity;

namespace EPMS.Repository.Repositories
{
    public class JobTitleHistoryRepository: BaseRepository<JobTitleHistory>, IJobTitleHistoryRepository
    {
        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        public JobTitleHistoryRepository(IUnityContainer container)
            : base(container)
        {
        }

        /// <summary>
        /// Primary database set
        /// </summary>
        protected override IDbSet<JobTitleHistory> DbSet
        {
            get { return db.JobTitleHistory; }
        }

        #endregion

        public JobTitleHistory GetJobTitleHistoryByJobTitleId(long jobTitleId)
        {
            return DbSet.OrderByDescending(x => x.JobTitleId).FirstOrDefault();
        }
    }
}
