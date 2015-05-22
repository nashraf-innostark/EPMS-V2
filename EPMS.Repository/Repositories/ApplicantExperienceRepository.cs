using System.Data.Entity;
using System.Linq;
using EPMS.Interfaces.Repository;
using EPMS.Models.DomainModels;
using EPMS.Repository.BaseRepository;
using Microsoft.Practices.Unity;

namespace EPMS.Repository.Repositories
{
    public class ApplicantExperienceRepository : BaseRepository<ApplicantExperience>, IApplicantExperienceRepository
    {
        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        public ApplicantExperienceRepository(IUnityContainer container)
            : base(container)
        {
        }

        /// <summary>
        /// Primary database set
        /// </summary>
        protected override IDbSet<ApplicantExperience> DbSet
        {
            get { return db.ApplicantExperiences; }
        }

        #endregion

        public ApplicantExperience Find(double id)
        {
            return DbSet.FirstOrDefault(e => e.ExperienceId == id);
        }
    }
}
