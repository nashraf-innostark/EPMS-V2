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

        #endregion

        #region Constructor

        public CompanyDocumentService(ICompanyDocumentRepository documentRepository,INotificationService notificationService)
        {
            this.documentRepository = documentRepository;
            this.notificationService = notificationService;
        }

        #endregion
        public bool AddDetail(CompanyDocumentDetail document)
        {
            documentRepository.Add(document);
            documentRepository.SaveChanges();

            #region Send Notification
                //SendNotification(document);
            #endregion
            return true;
        }
        public bool UpdateDetail(CompanyDocumentDetail document)
        {
            documentRepository.Update(document);
            documentRepository.SaveChanges();

            #region Send Notification
                //SendNotification(document);
            #endregion
            return true;
        }
        public void SendNotification(CompanyDocumentDetail document)
        {
                NotificationViewModel notificationViewModel = new NotificationViewModel();

                #region CommercialRegisterExpiryDate
                if (Utility.IsDate(document.CommercialRegisterExpiryDate))
                {
                    notificationViewModel.NotificationResponse.TitleE = document.CommercialRegister;
                    notificationViewModel.NotificationResponse.TitleA = document.CommercialRegister;

                    notificationViewModel.NotificationResponse.CategoryId = 1;//Company
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
                    notificationViewModel.NotificationResponse.TitleE = document.InsuranceCertificate;
                    notificationViewModel.NotificationResponse.TitleA = document.InsuranceCertificate;

                    notificationViewModel.NotificationResponse.CategoryId = 1;//Company
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
                    notificationViewModel.NotificationResponse.TitleE = document.ChamberCertificate;
                    notificationViewModel.NotificationResponse.TitleA = document.ChamberCertificate;


                    notificationViewModel.NotificationResponse.CategoryId = 1;//Company
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
                    notificationViewModel.NotificationResponse.TitleE = document.IncomeAndZakaCertificate;
                    notificationViewModel.NotificationResponse.TitleA = document.IncomeAndZakaCertificate;

                    notificationViewModel.NotificationResponse.CategoryId = 1;//Company
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
                    notificationViewModel.NotificationResponse.TitleE = document.SaudilizationCertificate;
                    notificationViewModel.NotificationResponse.TitleA = document.SaudilizationCertificate;

                    notificationViewModel.NotificationResponse.CategoryId = 1;//Company
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
