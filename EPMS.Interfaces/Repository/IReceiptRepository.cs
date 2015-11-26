using System.Collections;
using System.Collections.Generic;
using EPMS.Models.DomainModels;

namespace EPMS.Interfaces.Repository
{
    public interface IReceiptRepository : IBaseRepository<Receipt, long>
    {
        long GetLastReceiptNumber();
        IEnumerable<Receipt> GetReceiptsByInvoiceId(long invoiceId);
    }
}
