using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EPMS.Interfaces.IServices;
using EPMS.Interfaces.Repository;
using EPMS.Models.DomainModels;

namespace EPMS.Implementation.Services
{
    public class InvoiceService : IInvoiceService
    {
        #region Private
        private readonly IInvoiceRepository invoiceRepository;
        #endregion

        #region Constructor
        public InvoiceService(IInvoiceRepository invoiceRepository)
        {
            this.invoiceRepository = invoiceRepository;
        }

        #endregion

        #region Public
        public IEnumerable<Invoice> GetAll()
        {
            return invoiceRepository.GetAll();
        }

        public Invoice FindInvoiceById(long id)
        {
            return invoiceRepository.Find(id);
        }

        public bool AddInvoice(Invoice invoice)
        {
            invoiceRepository.Add(invoice);
            invoiceRepository.SaveChanges();
            return true;
        }

        public bool UpdateInvoice(Invoice invoice)
        {
            invoiceRepository.Update(invoice);
            invoiceRepository.SaveChanges();
            return true;
        }

        public void DeleteInvoice(Invoice invoice)
        {
            invoiceRepository.Delete(invoice);
            invoiceRepository.SaveChanges();
        }

        #endregion
    }
}
