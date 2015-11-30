using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using EPMS.Interfaces.IServices;
using EPMS.Interfaces.Repository;
using EPMS.Models.Common;
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

        public IEnumerable<Receipt> GetAll(string userId)
        {
            return receiptRepository.GetAll().Where(x => x.Invoice.Quotation.Customer.AspNetUsers.FirstOrDefault().Id == userId);
        }

        public long AddReceipt(Receipt receipt)
        {
            Invoice invoice = invoiceRepository.Find(receipt.InvoiceId);
            Quotation quotation = quotationRepository.Find(invoice.QuotationId);

            if (receipt.InstallmentNumber == 1)
            {
                quotation.FirstInstallmentStatus = true;
            }
            if (receipt.InstallmentNumber == 2)
            {
                quotation.SecondInstallmentStatus = true;
            }
            if (receipt.InstallmentNumber == 3)
            {
                quotation.ThirdInstallmentStatus = true;
            }
            if (receipt.InstallmentNumber == 4)
            {
                quotation.FourthInstallmentStatus = true;
            }
            if (receipt.PaymentType == (short)PaymentType.OffLine || receipt.PaymentType == (short)PaymentType.OnDelivery)
            {
                receipt.AmountPaid = GetAmountPaid(quotation, receipt.InstallmentNumber);
            }
            
            receipt.ReceiptNumber = GetReceiptNumber();
            receipt.RecCreatedBy = ClaimsPrincipal.Current.Identity.GetUserId();
            receipt.RecCreatedDt = DateTime.Now;
            receipt.RecLastUpdatedBy = ClaimsPrincipal.Current.Identity.GetUserId();
            receipt.RecLastUpdatedDt = DateTime.Now;

            receiptRepository.Add(receipt);
            receiptRepository.SaveChanges();
            
            quotationRepository.Update(quotation);
            quotationRepository.SaveChanges();
            
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

        public string GetReceiptNumber()
        {
            string year = DateTime.Now.ToString("yyyy");
            string month = DateTime.Now.ToString("MM");
            string day = DateTime.Now.ToString("dd");

            var receipt = receiptRepository.GetLastReceipt();

            if (receipt != null)
            {
                string oId = receipt.ReceiptNumber.Substring(receipt.ReceiptNumber.Length - 5, 5);
                int id = Convert.ToInt32(oId) + 1;
                int len = id.ToString(CultureInfo.InvariantCulture).Length;
                string zeros = "";
                switch (len)
                {
                    case 1:
                        zeros = "0000";
                        break;
                    case 2:
                        zeros = "000";
                        break;
                    case 3:
                        zeros = "00";
                        break;
                    case 4:
                        zeros = "0";
                        break;
                    case 5:
                        zeros = "";
                        break;
                }
                string orderId = year + month + day + zeros + id.ToString(CultureInfo.InvariantCulture);
                return orderId;
            }
            return year + month + day + "00001";
        }

        public decimal GetAmountPaid(Quotation quotation, int ins)
        {
            var grandTotal = quotation.QuotationItemDetails.Sum(x => x.TotalPrice);
            double amount = Convert.ToDouble(grandTotal);
            double disc = (Convert.ToDouble(quotation.QuotationDiscount) / 100) * amount;
            grandTotal = Convert.ToDecimal(amount - disc);
            switch (ins.ToString())
            {
                case "1":
                    grandTotal = (quotation.FirstInstallement / 100) * grandTotal;
                    break;
                case "2":
                    grandTotal = quotation.SecondInstallment != 0 ? (Convert.ToDecimal(quotation.SecondInstallment) / 100) * grandTotal : 0;
                    break;
                case "3":
                    grandTotal = quotation.ThirdInstallment != 0 ? (Convert.ToDecimal(quotation.ThirdInstallment) / 100) * grandTotal : 0;
                    break;
                case "4":
                    grandTotal = quotation.FourthInstallment != 0 ? (Convert.ToDecimal(quotation.FourthInstallment) / 100) * grandTotal : 0;
                    break;
            }
            return grandTotal;
        }

        #endregion
    }
}