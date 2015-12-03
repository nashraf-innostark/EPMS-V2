using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Linq;
using EPMS.Interfaces.IServices;
using EPMS.Interfaces.Repository;
using EPMS.Models.Common;
using EPMS.Models.DomainModels;
using EPMS.Models.ResponseModels;
using EPMS.Models.ResponseModels.NotificationResponseModel;

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
        private readonly IReceiptRepository receiptRepository;
        private readonly INotificationService notificationService;
        private readonly IAspNetUserRepository aspNetUserRepository;
        private readonly INotificationRepository notificationRepository;

        #endregion

        #region Constructor

        public InvoiceService(IInvoiceRepository invoiceRepository, IQuotationRepository quotationRepository,
            IQuotationItemRepository quotationItemRepository, ICustomerRepository customerRepository,
            ICompanyProfileRepository companyProfileRepository, IReceiptRepository receiptRepository, INotificationRepository notificationRepository, INotificationService notificationService, IAspNetUserRepository aspNetUserRepository)
        {
            this.invoiceRepository = invoiceRepository;
            this.quotationRepository = quotationRepository;
            this.quotationItemRepository = quotationItemRepository;
            this.customerRepository = customerRepository;
            this.companyProfileRepository = companyProfileRepository;
            this.receiptRepository = receiptRepository;
            this.notificationRepository = notificationRepository;
            this.notificationService = notificationService;
            this.aspNetUserRepository = aspNetUserRepository;
        }

        #endregion

        #region Public

        public IEnumerable<Invoice> GetAll()
        {
            return invoiceRepository.GetAll();
        }

        public IEnumerable<Invoice> GetAll(string userId)
        {
            return invoiceRepository.GetAll().Where(x =>x.Quotation.Customer.AspNetUsers.FirstOrDefault().Id == userId);
        }

        public Invoice FindInvoiceById(long id)
        {
            return invoiceRepository.Find(id);
        }

        public bool AddInvoice(Invoice invoice)
        {
            invoiceRepository.Add(invoice);
            invoiceRepository.SaveChanges();
            // Send Notification
            SendNotification(invoice);
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

        private void SendNotification(Invoice invoice)
        {
            NotificationViewModel notificationViewModel = new NotificationViewModel();
            
            #region Send notification to admin
            notificationViewModel.NotificationResponse.NotificationId =
                        notificationRepository.GetNotificationsIdByCategories(5, 20, invoice.InvoiceId);

            notificationViewModel.NotificationResponse.TitleE = ConfigurationManager.AppSettings["InvoiceE"];
            notificationViewModel.NotificationResponse.TitleA = ConfigurationManager.AppSettings["InvoiceA"];
            notificationViewModel.NotificationResponse.AlertBefore = Convert.ToInt32(ConfigurationManager.AppSettings["InvoiceAlertBefore"]); //Days
            notificationViewModel.NotificationResponse.SystemGenerated = true;
            notificationViewModel.NotificationResponse.ForAdmin = true;
            notificationViewModel.NotificationResponse.ForRole = UserRole.Admin;

            notificationViewModel.NotificationResponse.CategoryId = 5; //Other
            notificationViewModel.NotificationResponse.SubCategoryId = 20; //Invoice Pending
            notificationViewModel.NotificationResponse.ItemId = invoice.InvoiceId; //Invoice
            notificationViewModel.NotificationResponse.AlertDate = DateTime.Now.ToString("dd/MM/yyyy", new CultureInfo("en"));
            notificationViewModel.NotificationResponse.AlertDateType = 1; //0=Hijri, 1=Gregorian

            notificationService.AddUpdateNotification(notificationViewModel.NotificationResponse);
            #endregion

            #region Send notification to assigned employees
            notificationViewModel.NotificationResponse.NotificationId =
                        notificationRepository.GetNotificationsIdByCategories(5, 20, invoice.InvoiceId);

            notificationViewModel.NotificationResponse.TitleE = ConfigurationManager.AppSettings["InvoiceE"];
            notificationViewModel.NotificationResponse.TitleA = ConfigurationManager.AppSettings["InvoiceA"];
            notificationViewModel.NotificationResponse.AlertBefore = Convert.ToInt32(ConfigurationManager.AppSettings["InvoiceAlertBefore"]); //Days
            notificationViewModel.NotificationResponse.SystemGenerated = true;
            notificationViewModel.NotificationResponse.ForAdmin = false;

            notificationViewModel.NotificationResponse.CategoryId = 5; //Other
            notificationViewModel.NotificationResponse.SubCategoryId = 20; //Invoice Pending
            notificationViewModel.NotificationResponse.ItemId = invoice.InvoiceId; //Invoice
            notificationViewModel.NotificationResponse.AlertDate = DateTime.Now.ToString("dd/MM/yyyy", new CultureInfo("en"));
            notificationViewModel.NotificationResponse.AlertDateType = 1; //0=Hijri, 1=Gregorian

            notificationService.AddUpdateInvoiceNotification(notificationViewModel, invoice.Quotation.CustomerId);
            #endregion
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

            var employee = aspNetUserRepository.Find(response.Invoice.RecCreatedBy).Employee;
            response.EmployeeNameE = employee.EmployeeFirstNameE + " "+ employee.EmployeeMiddleNameE + " " + employee.EmployeeLastNameE;
            response.EmployeeNameA = employee.EmployeeFirstNameA + " " + employee.EmployeeMiddleNameA + " " + employee.EmployeeLastNameA;

            IEnumerable<Receipt> receipts = receiptRepository.GetReceiptsByInvoiceId(id);

            if (receipts != null)
            {
                response.FirstReceiptId = receipts.FirstOrDefault(x => x.InstallmentNumber == 1) != null
                    ? receipts.FirstOrDefault(x => x.InstallmentNumber == 1).ReceiptId
                    : 0;
                response.SecondReceiptId = receipts.FirstOrDefault(x => x.InstallmentNumber == 2) != null
                    ? receipts.FirstOrDefault(x => x.InstallmentNumber == 2).ReceiptId
                    : 0;
                response.ThirdReceiptId = receipts.FirstOrDefault(x => x.InstallmentNumber == 3) != null
                    ? receipts.FirstOrDefault(x => x.InstallmentNumber == 3).ReceiptId
                    : 0;
                response.FourthReceiptId = receipts.FirstOrDefault(x => x.InstallmentNumber == 4) != null
                    ? receipts.FirstOrDefault(x => x.InstallmentNumber == 4).ReceiptId
                    : 0;
            }

            return response;
        }

        #endregion
    }
}