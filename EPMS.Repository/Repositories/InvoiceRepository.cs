using System.Data.Entity;
using System.Linq;
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

        public Invoice GetLastInvoice()
        {
            Invoice invoice = DbSet.OrderByDescending(x => x.RecCreatedDt).FirstOrDefault();
            if (invoice != null)
                return invoice;
            return null;
        }
    }
}
