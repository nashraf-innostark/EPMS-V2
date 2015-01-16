using System.Data.Entity;
using EPMS.Repository.BaseRepository;
using EPMS.Interfaces.Repository;
using EPMS.Models.DomainModels;
using Microsoft.Practices.Unity;

namespace EPMS.Repository.Repositories
{
    public sealed class CompanyBankRepository : BaseRepository<CompanyBankDetail>, ICompanyBankRepository
    {
        public CompanyBankRepository(IUnityContainer container) : base(container)
        {
        }

        protected override IDbSet<CompanyBankDetail> DbSet
        {
            get { return db.Bank; }
        }
    }
}
