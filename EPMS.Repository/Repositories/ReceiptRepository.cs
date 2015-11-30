using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
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

        public Receipt GetLastReceipt()
        {
            Receipt receipt = DbSet.OrderByDescending(x => x.RecCreatedDt).FirstOrDefault();
            if (receipt != null)
                return receipt;
            return null;
        }

        public IEnumerable<Receipt> GetReceiptsByInvoiceId(long invoiceId)
        {
            return DbSet.Where(x => x.InvoiceId == invoiceId);
        }
    }
}
