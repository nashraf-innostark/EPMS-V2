using System.Data.Entity;
using EPMS.Interfaces.Repository;
using EPMS.Models.DomainModels;
using EPMS.Repository.BaseRepository;
using Microsoft.Practices.Unity;

namespace EPMS.Repository.Repositories
{
    public class ReceiptRepository : BaseRepository<Receipt>, IReceiptRepository
    {
        public ReceiptRepository(IUnityContainer container) : base(container)
        {
        }

        protected override IDbSet<Receipt> DbSet
        {
            get { return db.Receipts; }
        }
    }
}
