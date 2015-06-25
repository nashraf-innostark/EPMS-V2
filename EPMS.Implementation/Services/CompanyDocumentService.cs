using System;
using System.Configuration;
using System.Globalization;
using EPMS.Interfaces.IServices;
using EPMS.Interfaces.Repository;
using EPMS.Models.Common;
using EPMS.Models.DomainModels;
using EPMS.Models.ResponseModels.NotificationResponseModel;

namespace EPMS.Implementation.Services
{
    public class CompanyDocumentService : ICompanyDocumentService
    {
        #region Private

        private readonly ICompanyDocumentRepository documentRepository;
        private readonly INotificationService notificationService;
        private readonly INotificationRepository notificationRepository;

        #endregion

        #region Constructor

        public CompanyDocumentService(ICompanyDocumentRepository documentRepository,INotificationService notificationService,INotificationRepository notificationRepository)
        {
            this.documentRepository = documentRepository;
            this.notificationService = notificationService;
            this.notificationRepository = notificationRepository;
        }

        #endregion
        public bool AddDetail(CompanyDocumentDetail document)
        {
            documentRepository.Add(document);
            documentRepository.SaveChanges();

            #region Send Notification
            SendNotification(document);
            #endregion
            return true;
        }
        public bool UpdateDetail(CompanyDocumentDetail document)
        {
            documentRepository.Update(document);
            documentRepository.SaveChanges();

            #region Send Notification
            SendNotification(document);
            #endregion
            return true;
        }
        public void SendNotification(CompanyDocumentDetail document)
        {
                NotificationViewModel notificationViewModel = new NotificationViewModel();
                notificationViewModel.NotificationResponse.SystemGenerated = true;
                notificationViewModel.NotificationResponse.ForAdmin = true;
                notificationViewModel.NotificationResponse.ForRole = UserRole.Admin;
                notificationViewModel.NotificationResponse.AlertDateType = 0;//Hijri, 1=Gregorian
                
                #region CommercialRegisterExpiryDate
                if (Utility.IsDate(document.CommercialRegisterExpiryDate))
                {
                    notificationViewModel.NotificationResponse.NotificationId =
                        notificationRepository.GetNotificationsIdByCategories(1, 0, document.CompanyId);

                    notificationViewModel.NotificationResponse.TitleE = ConfigurationManager.AppSettings["CommercialRegisterExpiryDateE"];
                    notificationViewModel.NotificationResponse.TitleA = ConfigurationManager.AppSettings["CommercialRegisterExpiryDateA"];
                    notificationViewModel.NotificationResponse.AlertBefore = Convert.ToInt32(ConfigurationManager.AppSettings["CommercialRegisterExpiryDateAlertBefore"]); //Days
                    notificationViewModel.NotificationResponse.CategoryId = 1;//Company
                    notificationViewModel.NotificationResponse.SubCategoryId = 0;
                    notificationViewModel.NotificationResponse.ItemId = document.CompanyId;
                    notificationViewModel.NotificationResponse.AlertDate = Convert.ToDateTime(document.CommercialRegisterExpiryDate).ToShortDateString();
                    notificationService.AddUpdateNotification(notificationViewModel.NotificationResponse);
                }
                #endregion

                #region InsuranceCertificateExpiryDate
                if (Utility.IsDate(document.InsuranceCertificateExpiryDate))
                {
                    notificationViewModel.NotificationResponse.NotificationId =
                        notificationRepository.GetNotificationsIdByCategories(1, 1, document.CompanyId);

                    notificationViewModel.NotificationResponse.TitleE = ConfigurationManager.AppSettings["InsuranceCertificateExpiryDateE"];
                    notificationViewModel.NotificationResponse.TitleA = ConfigurationManager.AppSettings["InsuranceCertificateExpiryDateA"];
                    notificationViewModel.NotificationResponse.AlertBefore = Convert.ToInt32(ConfigurationManager.AppSettings["InsuranceCertificateExpiryDateAlertBefore"]); //Days
                    notificationViewModel.NotificationResponse.CategoryId = 1;//Company
                    notificationViewModel.NotificationResponse.SubCategoryId = 1;
                    notificationViewModel.NotificationResponse.ItemId = document.CompanyId;
                    notificationViewModel.NotificationResponse.AlertDate = Convert.ToDateTime(document.InsuranceCertificateExpiryDate).ToShortDateString();
                    
                    
                    notificationService.AddUpdateNotification(notificationViewModel.NotificationResponse);
                }
                #endregion

                #region ChamberCertificateExpiryDate
                if (Utility.IsDate(document.ChamberCertificateExpiryDate))
                {
                    notificationViewModel.NotificationResponse.NotificationId =
                           notificationRepository.GetNotificationsIdByCategories(1, 2, document.CompanyId);

                    notificationViewModel.NotificationResponse.TitleE = ConfigurationManager.AppSettings["ChamberCertificateExpiryDateE"];
                    notificationViewModel.NotificationResponse.TitleA = ConfigurationManager.AppSettings["ChamberCertificateExpiryDateA"];
                    notificationViewModel.NotificationResponse.AlertBefore = Convert.ToInt32(ConfigurationManager.AppSettings["ChamberCertificateExpiryDateAlertBefore"]); //Days
                    notificationViewModel.NotificationResponse.CategoryId = 1;//Company
                    notificationViewModel.NotificationResponse.SubCategoryId = 2;
                    notificationViewModel.NotificationResponse.ItemId = document.CompanyId;
                    notificationViewModel.NotificationResponse.AlertDate = Convert.ToDateTime(document.ChamberCertificateExpiryDate).ToShortDateString();
                    notificationService.AddUpdateNotification(notificationViewModel.NotificationResponse);
                }
                #endregion

                #region IncomeAndZakaCertificateExpiryDate
                if (Utility.IsDate(document.IncomeAndZakaCertificateExpiryDate))
                {
                    notificationViewModel.NotificationResponse.NotificationId =
                           notificationRepository.GetNotificationsIdByCategories(1, 3, document.CompanyId);

                    notificationViewModel.NotificationResponse.TitleE = ConfigurationManager.AppSettings["IncomeAndZakaCertificateExpiryDateE"];
                    notificationViewModel.NotificationResponse.TitleA = ConfigurationManager.AppSettings["IncomeAndZakaCertificateExpiryDateA"];
                    notificationViewModel.NotificationResponse.AlertBefore = Convert.ToInt32(ConfigurationManager.AppSettings["IncomeAndZakaCertificateExpiryDateAlertBefore"]); //Days
                    notificationViewModel.NotificationResponse.CategoryId = 1;//Company
                    notificationViewModel.NotificationResponse.SubCategoryId = 3;
                    notificationViewModel.NotificationResponse.ItemId = document.CompanyId;
                    
                    notificationViewModel.NotificationResponse.AlertDate = Convert.ToDateTime(document.IncomeAndZakaCertificateExpiryDate).ToShortDateString();

                    notificationService.AddUpdateNotification(notificationViewModel.NotificationResponse);
                }
                #endregion

                #region SaudilizationCertificateExpiryDate
                if (Utility.IsDate(document.SaudilizationCertificateExpiryDate))
                {
                    notificationViewModel.NotificationResponse.NotificationId =
                           notificationRepository.GetNotificationsIdByCategories(1, 4, document.CompanyId);

                    notificationViewModel.NotificationResponse.TitleE = ConfigurationManager.AppSettings["SaudilizationCertificateExpiryDateE"];
                    notificationViewModel.NotificationResponse.TitleA = ConfigurationManager.AppSettings["SaudilizationCertificateExpiryDateA"];
                    notificationViewModel.NotificationResponse.AlertBefore = Convert.ToInt32(ConfigurationManager.AppSettings["SaudilizationCertificateExpiryDateAlertBefore"]); //Days
                    notificationViewModel.NotificationResponse.CategoryId = 1;//Company
                    notificationViewModel.NotificationResponse.SubCategoryId = 4;
                    notificationViewModel.NotificationResponse.ItemId = document.CompanyId;
                    notificationViewModel.NotificationResponse.AlertDate = Convert.ToDateTime(document.SaudilizationCertificateExpiryDate).ToShortDateString();
                    
                    notificationService.AddUpdateNotification(notificationViewModel.NotificationResponse);
                }
                #endregion
        }
    }
}
