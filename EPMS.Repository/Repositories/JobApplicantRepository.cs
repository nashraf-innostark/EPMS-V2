using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EPMS.Interfaces.Repository;
using EPMS.Models.DomainModels;
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
    }
}
