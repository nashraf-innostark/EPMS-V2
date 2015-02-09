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
        private readonly IQuotationRepository Repository;
        private readonly INotificationService notificationService;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="repository"></param>
        /// <param name="notificationService"></param>
        public QuotationService(IQuotationRepository repository,INotificationService notificationService)
        {
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
            //SendNotification(quotation);
            return quotation.QuotationId;
        }

        public bool UpdateQuotation(Quotation quotation)
        {
            try
            {
                Repository.Update(quotation);
                Repository.SaveChanges();
                //SendNotification(quotation);
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
                notificationViewModel.NotificationResponse.TitleE = "Project delivery date in near.";
                notificationViewModel.NotificationResponse.TitleA = "Project delivery date in near.";

                notificationViewModel.NotificationResponse.CategoryId = 5; //Other
                notificationViewModel.NotificationResponse.AlertBefore = 2; //1 Week
                notificationViewModel.NotificationResponse.AlertDate = Convert.ToDateTime(quotation.FirstInsDueAtCompletion).ToShortDateString();
                notificationViewModel.NotificationResponse.AlertDateType = 1; //0=Hijri, 1=Gregorian
                notificationViewModel.NotificationResponse.SystemGenerated = true;

                notificationService.AddUpdateNotification(notificationViewModel);
            }
            #endregion
        }
    }
}
