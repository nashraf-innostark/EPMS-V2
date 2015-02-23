using System;
using System.Globalization;
using EPMS.Interfaces.IServices;
using EPMS.Interfaces.Repository;
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

                #region CommercialRegisterExpiryDate
                if (Utility.IsDate(document.CommercialRegisterExpiryDate))
                {
                    notificationViewModel.NotificationResponse.NotificationId =
                        notificationRepository.GetNotificationsIdByCategories(1, 0, document.CompanyId);

                    notificationViewModel.NotificationResponse.TitleE = document.CommercialRegister;
                    notificationViewModel.NotificationResponse.TitleA = document.CommercialRegister;

                    notificationViewModel.NotificationResponse.CategoryId = 1;//Company
                    notificationViewModel.NotificationResponse.SubCategoryId = 0;
                    notificationViewModel.NotificationResponse.ItemId = document.CompanyId;
                    notificationViewModel.NotificationResponse.AlertBefore = 1;//Month
                    notificationViewModel.NotificationResponse.AlertDate = Convert.ToDateTime(document.CommercialRegisterExpiryDate).ToShortDateString();
                    notificationViewModel.NotificationResponse.AlertDateType = 0;//Hijri, 1=Gregorian
                    notificationViewModel.NotificationResponse.SystemGenerated = true;

                    notificationService.AddUpdateNotification(notificationViewModel);
                }
                #endregion

                #region InsuranceCertificateExpiryDate
                if (Utility.IsDate(document.InsuranceCertificateExpiryDate))
                {
                    notificationViewModel.NotificationResponse.NotificationId =
                        notificationRepository.GetNotificationsIdByCategories(1, 1, document.CompanyId);

                    notificationViewModel.NotificationResponse.TitleE = document.CommercialRegister;
                    notificationViewModel.NotificationResponse.TitleA = document.CommercialRegister;

                    notificationViewModel.NotificationResponse.CategoryId = 1;//Company
                    notificationViewModel.NotificationResponse.SubCategoryId = 1;
                    notificationViewModel.NotificationResponse.ItemId = document.CompanyId;
                    notificationViewModel.NotificationResponse.AlertBefore = 1;//Month
                    notificationViewModel.NotificationResponse.AlertDate = Convert.ToDateTime(document.InsuranceCertificateExpiryDate).ToShortDateString();
                    notificationViewModel.NotificationResponse.AlertDateType = 0;//Hijri, 1=Gregorian
                    notificationViewModel.NotificationResponse.SystemGenerated = true;

                    notificationService.AddUpdateNotification(notificationViewModel);
                }
                #endregion

                #region ChamberCertificateExpiryDate
                if (Utility.IsDate(document.ChamberCertificateExpiryDate))
                {
                    notificationViewModel.NotificationResponse.NotificationId =
                           notificationRepository.GetNotificationsIdByCategories(1, 2, document.CompanyId);

                    notificationViewModel.NotificationResponse.TitleE = document.CommercialRegister;
                    notificationViewModel.NotificationResponse.TitleA = document.CommercialRegister;

                    notificationViewModel.NotificationResponse.CategoryId = 1;//Company
                    notificationViewModel.NotificationResponse.SubCategoryId = 2;
                    notificationViewModel.NotificationResponse.ItemId = document.CompanyId;
                    notificationViewModel.NotificationResponse.AlertBefore = 1;//Month
                    notificationViewModel.NotificationResponse.AlertDate = Convert.ToDateTime(document.ChamberCertificateExpiryDate).ToShortDateString();
                    notificationViewModel.NotificationResponse.AlertDateType = 0;//Hijri, 1=Gregorian
                    notificationViewModel.NotificationResponse.SystemGenerated = true;

                    notificationService.AddUpdateNotification(notificationViewModel);
                }
                #endregion

                #region IncomeAndZakaCertificateExpiryDate
                if (Utility.IsDate(document.IncomeAndZakaCertificateExpiryDate))
                {
                    notificationViewModel.NotificationResponse.NotificationId =
                           notificationRepository.GetNotificationsIdByCategories(1, 3, document.CompanyId);

                    notificationViewModel.NotificationResponse.TitleE = document.CommercialRegister;
                    notificationViewModel.NotificationResponse.TitleA = document.CommercialRegister;

                    notificationViewModel.NotificationResponse.CategoryId = 1;//Company
                    notificationViewModel.NotificationResponse.SubCategoryId = 3;
                    notificationViewModel.NotificationResponse.ItemId = document.CompanyId;
                    notificationViewModel.NotificationResponse.AlertBefore = 1;//Month
                    notificationViewModel.NotificationResponse.AlertDate = Convert.ToDateTime(document.IncomeAndZakaCertificateExpiryDate).ToShortDateString();
                    notificationViewModel.NotificationResponse.AlertDateType = 0;//Hijri, 1=Gregorian
                    notificationViewModel.NotificationResponse.SystemGenerated = true;

                    notificationService.AddUpdateNotification(notificationViewModel);
                }
                #endregion

                #region SaudilizationCertificateExpiryDate
                if (Utility.IsDate(document.SaudilizationCertificateExpiryDate))
                {
                    notificationViewModel.NotificationResponse.NotificationId =
                           notificationRepository.GetNotificationsIdByCategories(1, 4, document.CompanyId);

                    notificationViewModel.NotificationResponse.TitleE = document.CommercialRegister;
                    notificationViewModel.NotificationResponse.TitleA = document.CommercialRegister;

                    notificationViewModel.NotificationResponse.CategoryId = 1;//Company
                    notificationViewModel.NotificationResponse.SubCategoryId = 4;
                    notificationViewModel.NotificationResponse.ItemId = document.CompanyId;
                    notificationViewModel.NotificationResponse.AlertBefore = 1;//Month
                    notificationViewModel.NotificationResponse.AlertDate = Convert.ToDateTime(document.SaudilizationCertificateExpiryDate).ToShortDateString();
                    notificationViewModel.NotificationResponse.AlertDateType = 0;//Hijri, 1=Gregorian
                    notificationViewModel.NotificationResponse.SystemGenerated = true;

                    notificationService.AddUpdateNotification(notificationViewModel);
                }
                #endregion
        }
    }
}
