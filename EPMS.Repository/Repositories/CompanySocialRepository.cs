using System.Data.Entity;
using EPMS.Interfaces.Repository;
using EPMS.Models.DomainModels;
using EPMS.Repository.BaseRepository;
using Microsoft.Practices.Unity;

namespace EPMS.Repository.Repositories
{
    public sealed class CompanySocialRepository : BaseRepository<CompanySocialDetail>, ICompanySocialRepository
    {
        public CompanySocialRepository(IUnityContainer container) : base(container)
        {
        }

        protected override IDbSet<CompanySocialDetail> DbSet
        {
            get { return db.Social; }
        }
    }
}
