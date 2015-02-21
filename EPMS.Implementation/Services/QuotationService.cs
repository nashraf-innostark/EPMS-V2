using System;
using System.Collections.Generic;
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

        public Quotation FindQuotationById(long id)
        {
            return Repository.Find(id);
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

            #region Send notification to admin
            if (Utility.IsDate(quotation.FirstInsDueAtCompletion))
            {
                notificationViewModel.NotificationResponse.NotificationId =
                        notificationRepository.GetNotificationsIdByCategories(5,4, quotation.QuotationId);

                notificationViewModel.NotificationResponse.TitleE = "Project delivery date in near.";
                notificationViewModel.NotificationResponse.TitleA = "Project delivery date in near.";

                notificationViewModel.NotificationResponse.CategoryId = 5; //Other
                notificationViewModel.NotificationResponse.SubCategoryId = 4;
                notificationViewModel.NotificationResponse.ItemId = quotation.QuotationId;
                notificationViewModel.NotificationResponse.AlertBefore = 3; //1 day
                notificationViewModel.NotificationResponse.AlertDate = Convert.ToDateTime(quotation.FirstInsDueAtCompletion).ToShortDateString();
                notificationViewModel.NotificationResponse.AlertDateType = 1; //0=Hijri, 1=Gregorian
                notificationViewModel.NotificationResponse.SystemGenerated = true;

                notificationService.AddUpdateNotification(notificationViewModel);
            }
            if (Utility.IsDate(quotation.SecondInsDueAtCompletion))
            {
                notificationViewModel.NotificationResponse.NotificationId =
                        notificationRepository.GetNotificationsIdByCategories(5,5, quotation.QuotationId);

                notificationViewModel.NotificationResponse.TitleE = "Project delivery date in near.";
                notificationViewModel.NotificationResponse.TitleA = "Project delivery date in near.";

                notificationViewModel.NotificationResponse.CategoryId = 5; //Other
                notificationViewModel.NotificationResponse.SubCategoryId = 5;
                notificationViewModel.NotificationResponse.ItemId = quotation.QuotationId;
                notificationViewModel.NotificationResponse.AlertBefore = 3; //1 day
                notificationViewModel.NotificationResponse.AlertDate = Convert.ToDateTime(quotation.SecondInsDueAtCompletion).ToShortDateString();
                notificationViewModel.NotificationResponse.AlertDateType = 1; //0=Hijri, 1=Gregorian
                notificationViewModel.NotificationResponse.SystemGenerated = true;

                notificationService.AddUpdateNotification(notificationViewModel);
            }
            if (Utility.IsDate(quotation.ThirdInsDueAtCompletion))
            {
                notificationViewModel.NotificationResponse.NotificationId =
                        notificationRepository.GetNotificationsIdByCategories(5,6, quotation.QuotationId);

                notificationViewModel.NotificationResponse.TitleE = "Project delivery date in near.";
                notificationViewModel.NotificationResponse.TitleA = "Project delivery date in near.";

                notificationViewModel.NotificationResponse.CategoryId = 5; //Other
                notificationViewModel.NotificationResponse.SubCategoryId = 6;
                notificationViewModel.NotificationResponse.ItemId = quotation.QuotationId;
                notificationViewModel.NotificationResponse.AlertBefore = 3; //1 day
                notificationViewModel.NotificationResponse.AlertDate = Convert.ToDateTime(quotation.ThirdInsDueAtCompletion).ToShortDateString();
                notificationViewModel.NotificationResponse.AlertDateType = 1; //0=Hijri, 1=Gregorian
                notificationViewModel.NotificationResponse.SystemGenerated = true;

                notificationService.AddUpdateNotification(notificationViewModel);
            }
            if (Utility.IsDate(quotation.FourthInsDueAtCompletion))
            {
                notificationViewModel.NotificationResponse.NotificationId =
                        notificationRepository.GetNotificationsIdByCategories(5,7, quotation.QuotationId);

                notificationViewModel.NotificationResponse.TitleE = "Project delivery date in near.";
                notificationViewModel.NotificationResponse.TitleA = "Project delivery date in near.";

                notificationViewModel.NotificationResponse.CategoryId = 5; //Other
                notificationViewModel.NotificationResponse.SubCategoryId = 7;
                notificationViewModel.NotificationResponse.ItemId = quotation.QuotationId;
                notificationViewModel.NotificationResponse.AlertBefore = 3; //1 day
                notificationViewModel.NotificationResponse.AlertDate = Convert.ToDateTime(quotation.FourthInsDueAtCompletion).ToShortDateString();
                notificationViewModel.NotificationResponse.AlertDateType = 1; //0=Hijri, 1=Gregorian
                notificationViewModel.NotificationResponse.SystemGenerated = true;

                notificationService.AddUpdateNotification(notificationViewModel);
            }
            #endregion

            #region Send notification to Customer
            if (Utility.IsDate(quotation.FirstInsDueAtCompletion))
            {
                notificationViewModel.NotificationResponse.NotificationId =
                        notificationRepository.GetNotificationsIdByCategories(5,11, quotation.QuotationId);

                notificationViewModel.NotificationResponse.TitleE = "Project delivery date in near.";
                notificationViewModel.NotificationResponse.TitleA = "Project delivery date in near.";

                notificationViewModel.NotificationResponse.CategoryId = 5; //Other
                notificationViewModel.NotificationResponse.SubCategoryId = 11;
                notificationViewModel.NotificationResponse.ItemId = quotation.QuotationId;
                notificationViewModel.NotificationResponse.AlertBefore = 3; //1 day
                notificationViewModel.NotificationResponse.AlertDate = Convert.ToDateTime(quotation.FirstInsDueAtCompletion).ToShortDateString();
                notificationViewModel.NotificationResponse.AlertDateType = 1; //0=Hijri, 1=Gregorian
                notificationViewModel.NotificationResponse.SystemGenerated = false;
                notificationViewModel.NotificationResponse.UserId =
                    aspNetUserRepository.GetUserIdByCustomerId(quotation.CustomerId);
                notificationService.AddUpdateNotification(notificationViewModel);
            }
            if (Utility.IsDate(quotation.FirstInsDueAtCompletion))
            {
                notificationViewModel.NotificationResponse.NotificationId =
                        notificationRepository.GetNotificationsIdByCategories(5, 12, quotation.QuotationId);

                notificationViewModel.NotificationResponse.TitleE = "Project delivery date in near.";
                notificationViewModel.NotificationResponse.TitleA = "Project delivery date in near.";

                notificationViewModel.NotificationResponse.CategoryId = 5; //Other
                notificationViewModel.NotificationResponse.SubCategoryId = 12;
                notificationViewModel.NotificationResponse.ItemId = quotation.QuotationId;
                notificationViewModel.NotificationResponse.AlertBefore = 3; //1 day
                notificationViewModel.NotificationResponse.AlertDate = Convert.ToDateTime(quotation.FirstInsDueAtCompletion).ToShortDateString();
                notificationViewModel.NotificationResponse.AlertDateType = 1; //0=Hijri, 1=Gregorian
                notificationViewModel.NotificationResponse.SystemGenerated = false;
                notificationViewModel.NotificationResponse.UserId =
                    aspNetUserRepository.GetUserIdByCustomerId(quotation.CustomerId);
                notificationService.AddUpdateNotification(notificationViewModel);
            }
            if (Utility.IsDate(quotation.FirstInsDueAtCompletion))
            {
                notificationViewModel.NotificationResponse.NotificationId =
                        notificationRepository.GetNotificationsIdByCategories(5, 13, quotation.QuotationId);

                notificationViewModel.NotificationResponse.TitleE = "Project delivery date in near.";
                notificationViewModel.NotificationResponse.TitleA = "Project delivery date in near.";

                notificationViewModel.NotificationResponse.CategoryId = 5; //Other
                notificationViewModel.NotificationResponse.SubCategoryId = 13;
                notificationViewModel.NotificationResponse.ItemId = quotation.QuotationId;
                notificationViewModel.NotificationResponse.AlertBefore = 3; //1 day
                notificationViewModel.NotificationResponse.AlertDate = Convert.ToDateTime(quotation.FirstInsDueAtCompletion).ToShortDateString();
                notificationViewModel.NotificationResponse.AlertDateType = 1; //0=Hijri, 1=Gregorian
                notificationViewModel.NotificationResponse.SystemGenerated = false;
                notificationViewModel.NotificationResponse.UserId =
                    aspNetUserRepository.GetUserIdByCustomerId(quotation.CustomerId);
                notificationService.AddUpdateNotification(notificationViewModel);
            }
            if (Utility.IsDate(quotation.FirstInsDueAtCompletion))
            {
                notificationViewModel.NotificationResponse.NotificationId =
                        notificationRepository.GetNotificationsIdByCategories(5, 14, quotation.QuotationId);

                notificationViewModel.NotificationResponse.TitleE = "Project delivery date in near.";
                notificationViewModel.NotificationResponse.TitleA = "Project delivery date in near.";

                notificationViewModel.NotificationResponse.CategoryId = 5; //Other
                notificationViewModel.NotificationResponse.SubCategoryId = 14;
                notificationViewModel.NotificationResponse.ItemId = quotation.QuotationId;
                notificationViewModel.NotificationResponse.AlertBefore = 3; //1 day
                notificationViewModel.NotificationResponse.AlertDate = Convert.ToDateTime(quotation.FirstInsDueAtCompletion).ToShortDateString();
                notificationViewModel.NotificationResponse.AlertDateType = 1; //0=Hijri, 1=Gregorian
                notificationViewModel.NotificationResponse.SystemGenerated = false;
                notificationViewModel.NotificationResponse.UserId =
                    aspNetUserRepository.GetUserIdByCustomerId(quotation.CustomerId);
                notificationService.AddUpdateNotification(notificationViewModel);
            }
            #endregion
        }
    }
}
