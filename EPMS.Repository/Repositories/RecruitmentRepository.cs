using System.Data.Entity;
using EPMS.Interfaces.Repository;
using EPMS.Models.DomainModels;
using EPMS.Repository.BaseRepository;
using Microsoft.Practices.Unity;

namespace EPMS.Repository.Repositories
{
    public class RecruitmentRepository : BaseRepository<JobOffered>, IRecruitmentRepository
    {
        #region Constructor
            public RecruitmentRepository(IUnityContainer container) : base(container)
            {
            }
            protected override IDbSet<JobOffered> DbSet
            {
                get { return db.JobOffereds; }
            }
        #endregion
    }
}
