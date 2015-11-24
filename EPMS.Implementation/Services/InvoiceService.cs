using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EPMS.Interfaces.IServices;
using EPMS.Interfaces.Repository;
using EPMS.Models.DomainModels;
using EPMS.Models.ResponseModels;

namespace EPMS.Implementation.Services
{
    public class InvoiceService : IInvoiceService
    {
        #region Private

        private readonly IInvoiceRepository invoiceRepository;
        private readonly IQuotationRepository quotationRepository;
        private readonly IQuotationItemRepository quotationItemRepository;
        private readonly ICustomerRepository customerRepository;
        private readonly ICompanyProfileRepository companyProfileRepository;

        #endregion

        #region Constructor

        public InvoiceService(IInvoiceRepository invoiceRepository, IQuotationRepository quotationRepository,
            IQuotationItemRepository quotationItemRepository, ICustomerRepository customerRepository,
            ICompanyProfileRepository companyProfileRepository)
        {
            this.invoiceRepository = invoiceRepository;
            this.quotationRepository = quotationRepository;
            this.quotationItemRepository = quotationItemRepository;
            this.customerRepository = customerRepository;
            this.companyProfileRepository = companyProfileRepository;
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

        public InvoiceResponse GetInvoiceDetails(long id)
        {
            InvoiceResponse response = new InvoiceResponse
            {
                Invoice = invoiceRepository.Find(id)
            };
            response.Quotation = quotationRepository.Find(response.Invoice.QuotationId);
            response.CompanyProfile = companyProfileRepository.GetCompanyProfile();
            response.Customer = customerRepository.Find(response.Quotation.CustomerId);
            return response;
        }

        #endregion
    }
}