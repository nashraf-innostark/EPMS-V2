using System.Collections.Generic;
using EPMS.Models.DomainModels;
using EPMS.Models.ResponseModels;

namespace EPMS.Interfaces.IServices
{
    public interface IInvoiceService
    {
        IEnumerable<Invoice> GetAll();
        Invoice FindInvoiceById(long id);
        bool AddInvoice(Invoice invoice);
        bool UpdateInvoice(Invoice invoice);
        void DeleteInvoice(Invoice invoice);
        InvoiceResponse GetInvoiceDetails(long id);
    }
}
