using System.Data.Entity;
using EPMS.Interfaces.Repository;
using EPMS.Models.DomainModels;
using EPMS.Repository.BaseRepository;
using Microsoft.Practices.Unity;

namespace EPMS.Repository.Repositories
{
    class InvoiceRepository : BaseRepository<Invoice>, IInvoiceRepository
    {
        public InvoiceRepository(IUnityContainer container) : base(container)
        {
        }

        protected override IDbSet<Invoice> DbSet
        {
            get { return db.Invoices; }
        }
    }
}
