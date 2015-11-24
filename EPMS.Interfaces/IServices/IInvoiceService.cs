using System.Collections.Generic;
using EPMS.Models.DomainModels;

namespace EPMS.Interfaces.IServices
{
    public interface IInvoiceService
    {
        IEnumerable<Invoice> GetAll();
        Invoice FindInvoiceById(long id);
        bool AddInvoice(Invoice invoice);
        bool UpdateInvoice(Invoice invoice);
        void DeleteInvoice(Invoice invoice);
    }
}
