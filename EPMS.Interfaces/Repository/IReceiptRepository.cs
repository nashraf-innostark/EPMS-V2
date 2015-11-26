using System.Collections;
using System.Collections.Generic;
using EPMS.Models.DomainModels;

namespace EPMS.Interfaces.Repository
{
    public interface IReceiptRepository : IBaseRepository<Receipt, long>
    {
        Receipt GetLastReceipt();
        IEnumerable<Receipt> GetReceiptsByInvoiceId(long invoiceId);
    }
}
