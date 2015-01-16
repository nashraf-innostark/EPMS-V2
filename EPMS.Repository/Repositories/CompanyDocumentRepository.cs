using System.Data.Entity;
using EPMS.Interfaces.Repository;
using EPMS.Models.DomainModels;
using EPMS.Repository.BaseRepository;
using Microsoft.Practices.Unity;

namespace EPMS.Repository.Repositories
{
    public sealed class CompanyDocumentRepository : BaseRepository<CompanyDocumentDetail>, ICompanyDocumentRepository
    {
        public CompanyDocumentRepository(IUnityContainer container) : base(container)
        {
        }

        protected override IDbSet<CompanyDocumentDetail> DbSet
        {
            get { return db.Document; }
        }
    }
}
