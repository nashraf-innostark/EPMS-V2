using System.Collections.Generic;
using EPMS.Interfaces.IServices;
using EPMS.Interfaces.Repository;
using EPMS.Models.DomainModels;
using EPMS.Models.ResponseModels;

namespace EPMS.Implementation.Services
{
    public class ReceiptService : IReceiptService
    {

        #region Private

        private readonly IReceiptRepository receiptRepository;
        private readonly IInvoiceRepository invoiceRepository;
        private readonly IQuotationRepository quotationRepository;
        private readonly ICompanyProfileRepository companyProfileRepository;
        private readonly ICustomerRepository customerRepository;

        #endregion

        #region Constructor

        public ReceiptService(IReceiptRepository receiptRepository, IInvoiceRepository invoiceRepository,
            IQuotationRepository quotationRepository, ICompanyProfileRepository companyProfileRepository,
            ICustomerRepository customerRepository)
        {
            this.receiptRepository = receiptRepository;
            this.invoiceRepository = invoiceRepository;
            this.quotationRepository = quotationRepository;
            this.companyProfileRepository = companyProfileRepository;
            this.customerRepository = customerRepository;
        }

        #endregion

        #region Public

        public IEnumerable<Receipt> GetAll()
        {
            return receiptRepository.GetAll();
        }

        public Receipt FindReceiptById(long id)
        {
            return receiptRepository.Find(id);
        }

        public ReceiptResponse GetReceiptDetails(long id)
        {
            ReceiptResponse response = new ReceiptResponse();

            response.Receipt = receiptRepository.Find(id);
            response.Invoice = invoiceRepository.Find(response.Receipt.InvoiceId);
            response.Quotation = quotationRepository.Find(response.Invoice.QuotationId);
            response.Customer = customerRepository.Find(response.Quotation.CustomerId);
            response.CompanyProfile = companyProfileRepository.GetCompanyProfile();

            return response;
        }

        #endregion
    }
}