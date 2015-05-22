using System;
using System.Collections.Generic;
using System.Configuration;
using EPMS.Interfaces.IServices;
using EPMS.Interfaces.Repository;
using EPMS.Models.DomainModels;
using EPMS.Models.RequestModels;
using EPMS.Models.ResponseModels;
using EPMS.Models.ResponseModels.NotificationResponseModel;
using Project = EPMS.Models.DomainModels.Project;

namespace EPMS.Implementation.Services
{
    public class QuotationService : IQuotationService
    {
        private readonly INotificationRepository notificationRepository;
        private readonly IAspNetUserRepository aspNetUserRepository;
        private readonly IQuotationRepository Repository;
        private readonly INotificationService notificationService;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="aspNetUserRepository"></param>
        /// <param name="repository"></param>
        /// <param name="notificationService"></param>
        /// <param name="notificationRepository"></param>
        public QuotationService(INotificationRepository notificationRepository,IAspNetUserRepository aspNetUserRepository,IQuotationRepository repository,INotificationService notificationService)
        {
            this.notificationRepository = notificationRepository;
            this.aspNetUserRepository = aspNetUserRepository;
            Repository = repository;
            this.notificationService = notificationService;
        }

        public IEnumerable<Quotation> GetAll()
        {
            return Repository.GetAll();
        }

        public IEnumerable<Quotation> GetAllQuotationByCustomerId(long customerId)
        {
            return Repository.GetAllQuotationByCustomerId(customerId);
        }

        public Quotation FindQuotationById(long id)
        {
            return Repository.Find(id);
        }

        public IEnumerable<Quotation> FindQuotationByIdForProjectDetail(long id)
        {
            return Repository.FindQuotationByIdForProjectDetail(id);
        }

        public long AddQuotation(Quotation quotation)
        {
            Repository.Add(quotation);
            Repository.SaveChanges();
            SendNotification(quotation);
            return quotation.QuotationId;
        }

        public bool UpdateQuotation(Quotation quotation)
        {
            try
            {
                Repository.Update(quotation);
                Repository.SaveChanges();
                SendNotification(quotation);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public void DeleteQuotation(Quotation quotation)
        {
            
        }

        public QuotationResponse GetAllQuotation(QuotationSearchRequest searchRequest)
        {
            return Repository.GetAllQuotation(searchRequest);
        }

        public Quotation FindQuotationByOrderId(long orderId)
        {
            return Repository.FindQuotationByOrderId(orderId);
        }

        public void SendNotification(Quotation quotation)
        {
            NotificationViewModel notificationViewModel = new NotificationViewModel();
            notificationViewModel.NotificationResponse.SystemGenerated = true;
            notificationViewModel.NotificationResponse.ForAdmin = true;

            if (Utility.IsDate(quotation.FirstInsDueAtCompletion))
            {
                notificationViewModel.NotificationResponse.NotificationId =
                        notificationRepository.GetNotificationsIdByCategories(5, 11, quotation.QuotationId);

                notificationViewModel.NotificationResponse.TitleE = ConfigurationManager.AppSettings["FirstInsDueE"];
                notificationViewModel.NotificationResponse.TitleA = ConfigurationManager.AppSettings["FirstInsDueA"];
                notificationViewModel.NotificationResponse.AlertBefore = Convert.ToInt32(ConfigurationManager.AppSettings["FirstInsDueAlertBefore"]); //Days

                notificationViewModel.NotificationResponse.CategoryId = 5; //Other
                notificationViewModel.NotificationResponse.SubCategoryId = 11;
                notificationViewModel.NotificationResponse.ItemId = quotation.QuotationId;
                notificationViewModel.NotificationResponse.AlertDate = Convert.ToDateTime(quotation.FirstInsDueAtCompletion).ToShortDateString();
                notificationViewModel.NotificationResponse.AlertDateType = 1; //0=Hijri, 1=Gregorian
                notificationViewModel.NotificationResponse.UserId =
                    aspNetUserRepository.GetUserIdByCustomerId(quotation.CustomerId);
                notificationService.AddUpdateNotification(notificationViewModel.NotificationResponse);
            }
            if (Utility.IsDate(quotation.SecondInsDueAtCompletion))
            {
                notificationViewModel.NotificationResponse.NotificationId =
                        notificationRepository.GetNotificationsIdByCategories(5, 12, quotation.QuotationId);

                notificationViewModel.NotificationResponse.TitleE = ConfigurationManager.AppSettings["SecondInsDueE"];
                notificationViewModel.NotificationResponse.TitleA = ConfigurationManager.AppSettings["SecondInsDueA"];
                notificationViewModel.NotificationResponse.AlertBefore = Convert.ToInt32(ConfigurationManager.AppSettings["SecondInsDueAlertBefore"]); //Days

                notificationViewModel.NotificationResponse.CategoryId = 5; //Other
                notificationViewModel.NotificationResponse.SubCategoryId = 12;
                notificationViewModel.NotificationResponse.ItemId = quotation.QuotationId;
                notificationViewModel.NotificationResponse.AlertDate = Convert.ToDateTime(quotation.FirstInsDueAtCompletion).ToShortDateString();
                notificationViewModel.NotificationResponse.AlertDateType = 1; //0=Hijri, 1=Gregorian
                notificationViewModel.NotificationResponse.UserId =
                    aspNetUserRepository.GetUserIdByCustomerId(quotation.CustomerId);
                notificationService.AddUpdateNotification(notificationViewModel.NotificationResponse);
            }
            if (Utility.IsDate(quotation.ThirdInsDueAtCompletion))
            {
                notificationViewModel.NotificationResponse.NotificationId =
                        notificationRepository.GetNotificationsIdByCategories(5, 13, quotation.QuotationId);

                notificationViewModel.NotificationResponse.TitleE = ConfigurationManager.AppSettings["ThirdInsDueE"];
                notificationViewModel.NotificationResponse.TitleA = ConfigurationManager.AppSettings["ThirdInsDueA"];
                notificationViewModel.NotificationResponse.AlertBefore = Convert.ToInt32(ConfigurationManager.AppSettings["ThirdInsDueAlertBefore"]); //Days

                notificationViewModel.NotificationResponse.CategoryId = 5; //Other
                notificationViewModel.NotificationResponse.SubCategoryId = 13;
                notificationViewModel.NotificationResponse.ItemId = quotation.QuotationId;
                notificationViewModel.NotificationResponse.AlertDate = Convert.ToDateTime(quotation.FirstInsDueAtCompletion).ToShortDateString();
                notificationViewModel.NotificationResponse.AlertDateType = 1; //0=Hijri, 1=Gregorian
                notificationViewModel.NotificationResponse.UserId =
                    aspNetUserRepository.GetUserIdByCustomerId(quotation.CustomerId);
                notificationService.AddUpdateNotification(notificationViewModel.NotificationResponse);
            }
            if (Utility.IsDate(quotation.FourthInsDueAtCompletion))
            {
                notificationViewModel.NotificationResponse.NotificationId =
                        notificationRepository.GetNotificationsIdByCategories(5, 14, quotation.QuotationId);

                notificationViewModel.NotificationResponse.TitleE = ConfigurationManager.AppSettings["FourthInsDueE"];
                notificationViewModel.NotificationResponse.TitleA = ConfigurationManager.AppSettings["FourthInsDueA"];
                notificationViewModel.NotificationResponse.AlertBefore = Convert.ToInt32(ConfigurationManager.AppSettings["FourthInsDueAlertBefore"]); //Days

                notificationViewModel.NotificationResponse.CategoryId = 5; //Other
                notificationViewModel.NotificationResponse.SubCategoryId = 14;
                notificationViewModel.NotificationResponse.ItemId = quotation.QuotationId;
                notificationViewModel.NotificationResponse.AlertDate = Convert.ToDateTime(quotation.FirstInsDueAtCompletion).ToShortDateString();
                notificationViewModel.NotificationResponse.AlertDateType = 1; //0=Hijri, 1=Gregorian
                notificationViewModel.NotificationResponse.UserId =
                    aspNetUserRepository.GetUserIdByCustomerId(quotation.CustomerId);
                notificationService.AddUpdateNotification(notificationViewModel.NotificationResponse);
            }
        }
    }
}
