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

        public long GetLastReceiptNumber()
        {
            Receipt receipt = DbSet.OrderByDescending(x => x.ReceiptNumber).FirstOrDefault();
            if (receipt != null)
                return receipt.ReceiptNumber;
            return 100000;
        }

        public IEnumerable<Receipt> GetReceiptsByInvoiceId(long invoiceId)
        {
            return DbSet.Where(x => x.InvoiceId == invoiceId);
        }
    }
}
