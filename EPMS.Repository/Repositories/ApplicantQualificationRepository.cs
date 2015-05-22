using System;
using System.Data.Entity;
using System.Linq;
using EPMS.Interfaces.Repository;
using EPMS.Models.DomainModels;
using EPMS.Repository.BaseRepository;
using Microsoft.Practices.Unity;

namespace EPMS.Repository.Repositories
{
    public class ApplicantQualificationRepository : BaseRepository<ApplicantQualification>, IApplicantQualificationRepository
    {
         #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        public ApplicantQualificationRepository(IUnityContainer container)
            : base(container)
        {
        }

        /// <summary>
        /// Primary database set
        /// </summary>
        protected override IDbSet<ApplicantQualification> DbSet
        {
            get { return db.ApplicantQualifications; }
        }

        #endregion

        public ApplicantQualification Find(double id)
        {
            return DbSet.FirstOrDefault(q => q.QualificationId == id);
        }
    }
}
