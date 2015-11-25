using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using EPMS.Interfaces.IServices;
using EPMS.Interfaces.Repository;
using EPMS.Models.DomainModels;
using EPMS.Models.ResponseModels;
using FaceSharp.Api.Objects;
using Microsoft.AspNet.Identity;

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

        public long AddReceipt(Receipt receipt)
        {
            Invoice invoice = invoiceRepository.Find(receipt.InvoiceId);
            Quotation quotation = quotationRepository.Find(invoice.QuotationId);

            var totalAmount = quotation.QuotationItemDetails.Sum(x => x.TotalPrice);
            decimal discountpercentage = (decimal)(quotation.QuotationDiscount / Convert.ToDecimal(100));
            decimal discountAmount = totalAmount*discountpercentage;
            decimal grantTotal = totalAmount - discountAmount;

            var lastReceiptNumber = receiptRepository.GetLastReceiptNumber();

            if (receipt.InstallmentNumber == 1)
            {
                quotation.FirstInstallmentStatus = true;
                var firstInstallment = discountAmount;
                receipt.AmountPaid = grantTotal * (receipt.AmountPaid / 100);
            }
            if (receipt.InstallmentNumber == 2)
            {
                quotation.SecondInstallmentStatus = true;
                var secondInstallment = quotation.FirstInstallement;
                receipt.AmountPaid = grantTotal * (receipt.AmountPaid / 100);
            }
            if (receipt.InstallmentNumber == 3)
            {
                quotation.ThirdInstallmentStatus = true;
                var thirdInstallment = quotation.FirstInstallement;
                receipt.AmountPaid = grantTotal * (receipt.AmountPaid / 100);
            }
            if (receipt.InstallmentNumber == 1)
            {
                quotation.FourthInstallmentStatus = true;
                var fourthInstallment = quotation.FirstInstallement;
                receipt.AmountPaid = grantTotal * (receipt.AmountPaid / 100);
            }

            receipt.ReceiptNumber = lastReceiptNumber + 1;
            receipt.RecCreatedBy = ClaimsPrincipal.Current.Identity.GetUserId();
            receipt.RecCreatedDt = DateTime.Now;
            receipt.RecLastUpdatedBy = ClaimsPrincipal.Current.Identity.GetUserId();
            receipt.RecLastUpdatedDt = DateTime.Now;


            receiptRepository.Add(receipt);
            quotationRepository.Update(quotation);

            receiptRepository.SaveChanges();
            return receipt.ReceiptId;
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