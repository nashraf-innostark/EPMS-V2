using System.Data.Entity;
using System.Linq;
using EPMS.Interfaces.Repository;
using EPMS.Models.DomainModels;
using EPMS.Repository.BaseRepository;
using Microsoft.Practices.Unity;

namespace EPMS.Repository.Repositories
{
    public sealed class CompanyProfileRepository : BaseRepository<CompanyProfile>, ICompanyProfileRepository
    {
        #region Constructor
        public CompanyProfileRepository(IUnityContainer container) : base(container)
        {
        }

        protected override IDbSet<CompanyProfile> DbSet
        {
            get { return db.Profile; }
        }
        #endregion
        public CompanyProfile GetCompanyProfile()
        {
            return DbSet.FirstOrDefault();
        }
    }
}
