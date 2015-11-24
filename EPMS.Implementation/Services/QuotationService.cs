using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Linq;
using EPMS.Interfaces.IServices;
using EPMS.Interfaces.Repository;
using EPMS.Models.Common;
using EPMS.Models.DomainModels;
using EPMS.Models.ModelMapers;
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
        private readonly IQuotationItemRepository itemRepository;
        private readonly INotificationService notificationService;
        private readonly ICustomerService customerService;
        private readonly IItemVariationRepository variationRepository;
        private readonly IRFQRepository rfqRepository;

        /// <summary>
        /// Constructor
        /// </summary>
        public QuotationService(INotificationRepository notificationRepository, IAspNetUserRepository aspNetUserRepository, IQuotationRepository repository, INotificationService notificationService, ICustomerService customerService, IRFQRepository rfqRepository, IItemVariationRepository variationRepository, IQuotationItemRepository itemRepository)
        {
            this.notificationRepository = notificationRepository;
            this.aspNetUserRepository = aspNetUserRepository;
            Repository = repository;
            this.notificationService = notificationService;
            this.customerService = customerService;
            this.rfqRepository = rfqRepository;
            this.variationRepository = variationRepository;
            this.itemRepository = itemRepository;
        }

        public IEnumerable<Quotation> GetAll()
        {
            return Repository.GetAll();
        }

        public IEnumerable<Quotation> GetAllQuotationByCustomerId(long customerId)
        {
            return Repository.GetAllQuotationByCustomerId(customerId);
        }

        public QuotationResponse GetQuotationResponse(long quotationId, long customerId, string from)
        {
            QuotationResponse response = new QuotationResponse
            {
                Customers = customerService.GetAll().ToList(),
                ItemVariationDropDownList = variationRepository.GetItemVariationDropDownList(),
            };
            if (quotationId != 0)
            {
                response.Quotation = Repository.Find(quotationId);
                if (response.Quotation != null)
                {
                    response.Rfqs = rfqRepository.GetRfqsByCustomerId(response.Quotation.CustomerId).ToList();
                }
            }
            return response;
        }

        public QuotationResponse GetRfqForQuotationResponse(long rfqId)
        {
            QuotationResponse response = new QuotationResponse
            {
                Rfq = rfqRepository.FindByRfqId(rfqId),
                Customers = customerService.GetAll().ToList(),
                Rfqs = rfqRepository.GetAllPendingRfqs().ToList()
            };
            return response;
        }

        public Quotation FindQuotationById(long id)
        {
            return Repository.Find(id);
        }

        public IEnumerable<Quotation> FindQuotationByIdForProjectDetail(long id)
        {
            return Repository.FindQuotationByIdForProjectDetail(id);
        }

        public QuotationResponse AddQuotation(Quotation quotation)
        {
            try
            {
                if (quotation.QuotationId == 0)
                {
                    quotation.SerialNumber = GetQuotationSerialNumber();
                    Repository.Add(quotation);
                    Repository.SaveChanges();
                    if (quotation.RFQId != null)
                    {
                        // update RFQ status
                        var rfq = rfqRepository.Find((long)quotation.RFQId);
                        if (rfq != null)
                        {
                            rfq.Status = (int)RFQStatus.QoutationCreated;
                            rfqRepository.Update(rfq);
                            rfqRepository.SaveChanges();
                        }
                    }
                }
                SendNotification(quotation);
                return new QuotationResponse { Status = true };
            }
            catch (Exception)
            {
                return new QuotationResponse { Status = false, Customers = customerService.GetAll() };
            }
        }
        public string GetQuotationSerialNumber()
        {
            string year = DateTime.Now.ToString("yyyy");
            string month = DateTime.Now.ToString("MM");
            string day = DateTime.Now.ToString("dd");
            var result = Repository.GetAll().OrderByDescending(x => x.RecCreatedDate);
            if (result.FirstOrDefault() != null)
            {
                var rfq = result.FirstOrDefault();
                string oId = rfq.SerialNumber.Substring(rfq.SerialNumber.Length - 5, 5);
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

        public QuotationResponse UpdateQuotation(Quotation quotation)
        {
            try
            {
                Quotation dbData = Repository.Find(quotation.QuotationId);
                if (dbData != null)
                {
                    var dataToUpdate = dbData.UpdateDbDataFromClient(quotation);
                    Repository.Update(dataToUpdate);
                    Repository.SaveChanges();

                    // update items detail
                    var itemsInDb = dbData.QuotationItemDetails.ToList();
                    foreach (var detail in quotation.QuotationItemDetails)
                    {
                        if (detail.ItemId > 0)
                        {
                            var itemInDb = itemsInDb.FirstOrDefault(x => x.ItemId == detail.ItemId);
                            if (itemInDb != null)
                            {
                                var itemToUpdate = itemInDb.UpdateDbDataFromClient(detail);
                                itemRepository.Update(itemToUpdate);
                                itemRepository.SaveChanges();
                                itemsInDb.RemoveAll(x => x.ItemId == detail.ItemId);
                            }
                        }
                        else
                        {
                            itemRepository.Add(detail);
                            itemRepository.SaveChanges();
                        }
                    }
                    
                    SendNotification(quotation);
                    return new QuotationResponse{ Status = true};
                }
            }
            catch (Exception)
            {
            }
            return new QuotationResponse{ Status = false, Customers = customerService.GetAll() };
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
            notificationViewModel.NotificationResponse.ForRole = UserRole.Admin; ;

            if (Utility.IsDate(quotation.FirstInsDueAtCompletion))
            {
                notificationViewModel.NotificationResponse.NotificationId =
                        notificationRepository.GetNotificationsIdByCategories(5, 11, quotation.QuotationId);

                notificationViewModel.NotificationResponse.TitleE = ConfigurationManager.AppSettings["FirstInsDueE"];
                notificationViewModel.NotificationResponse.TitleA = ConfigurationManager.AppSettings["FirstInsDueA"];
                notificationViewModel.NotificationResponse.AlertBefore = Convert.ToInt32(ConfigurationManager.AppSettings["FirstInsDueAlertBefore"]); //Days

                notificationViewModel.NotificationResponse.CategoryId = 5; //Other
                notificationViewModel.NotificationResponse.SubCategoryId = 11;//FirstInsDueE
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
