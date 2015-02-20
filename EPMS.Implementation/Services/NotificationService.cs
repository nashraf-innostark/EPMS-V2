using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using EPMS.Interfaces.IServices;
using EPMS.Interfaces.Repository;
using EPMS.Models.DomainModels;
using EPMS.Models.ModelMapers.NotificationMapper;
using EPMS.Models.RequestModels.NotificationRequestModels;
using EPMS.Models.ResponseModels.NotificationResponseModel;
using Microsoft.Practices.EnterpriseLibrary.Common.Properties;

namespace EPMS.Implementation.Services
{
    public class NotificationService:INotificationService
    {
        private readonly AspNetUserService aspNetUserService;
        private readonly INotificationRecipientRepository notificationRecipientRepository;
        private readonly INotificationRepository notificationRepository;
        private readonly IEmployeeRepository employeeRepository;

        public NotificationService(AspNetUserService aspNetUserService,INotificationRecipientRepository notificationRecipientRepository,INotificationRepository notificationRepository, IEmployeeRepository employeeRepository)
        {
            this.aspNetUserService = aspNetUserService;
            this.notificationRecipientRepository = notificationRecipientRepository;
            this.notificationRepository = notificationRepository;
            this.employeeRepository = employeeRepository;
        }

        public IEnumerable<NotificationResponse> GetAll()
        {
            return notificationRepository.GetAll().Select(x => x.CreateFromServerToClient());
        }

        public NotificationResponse FindNotification(long notificationId)
        {
            return notificationRepository.Find(notificationId).CreateFromServerToClient();
        }

        public NotificationViewModel LoadNotificationAndBaseData(long? notificationId)
        {
            NotificationViewModel notificationViewModel=new NotificationViewModel();
            IEnumerable<Employee> employees = employeeRepository.GetAll().ToList();
            if (employees.Any())
            {                
                notificationViewModel.EmployeeDDL = employees.Select(x => x.CreateForEmployeeDDL()).ToList();
            }
            if (notificationId != null && notificationId > 0)
            {
                Notification notification = notificationRepository.Find((long) notificationId);
                if(notification!=null)
                    notificationViewModel.NotificationResponse = notification.CreateFromServerToClient();
            }
            return notificationViewModel;
        }

        public NotificationViewModel LoadNotificationDetailsAndBaseData(long? notificationId,string userId)
        {
            NotificationViewModel notificationViewModel = new NotificationViewModel();
            IEnumerable<Employee> employees = employeeRepository.GetAll().ToList();
            if (employees.Any())
            {
                notificationViewModel.EmployeeDDL = employees.Select(x => x.CreateForEmployeeDDL()).ToList();
            }
            if (notificationId != null && notificationId > 0)
            {
                Notification notification = notificationRepository.Find((long)notificationId);
                if (notification != null)
                {
                    //Mark the notification as READ
                    if (notification.SystemGenerated)
                    {
                        //Save, who viewed SystemGenerated notification
                        if (notification.NotificationRecipients.All(x => x.UserId != userId))
                        {
                            NotificationRecipient recipient = new NotificationRecipient();
                            recipient.UserId = userId;
                            recipient.NotificationId = notification.NotificationId;
                            recipient.IsRead = true;
                            notificationRecipientRepository.Add(recipient);
                            notificationRecipientRepository.SaveChanges();
                        }
                    }
                    //Mark as READ, who viewed Manual notification
                    else if (!notification.NotificationRecipients.FirstOrDefault().IsRead)
                    {
                        notification.NotificationRecipients.FirstOrDefault().IsRead = true;
                        notificationRecipientRepository.Update(notification.NotificationRecipients.FirstOrDefault());
                        notificationRecipientRepository.SaveChanges();
                    }
                    notificationViewModel.NotificationResponse = notification.CreateFromServerToClient();
                }
            }
            return notificationViewModel;
        }

        public bool AddUpdateNotification(NotificationViewModel notificationViewModel)
        {
            //Get UserId of selected employee
            if (notificationViewModel.NotificationResponse.EmployeeId > 0)
                notificationViewModel.NotificationResponse.UserId =
                    aspNetUserService.GetUserIdByEmployeeId(notificationViewModel.NotificationResponse.EmployeeId);

            if (notificationViewModel.NotificationResponse.NotificationId > 0)
            {
                notificationViewModel.NotificationResponse.RecLastUpdatedBy = notificationViewModel.UserId;
                notificationViewModel.NotificationResponse.RecLastUpdatedDate = DateTime.Now;
                //Update notification
                notificationViewModel.NotificationResponse.NotificationId = UpdateNotification(notificationViewModel.NotificationResponse);
                //Delete Notification recipient
                if(notificationRecipientRepository.DeleteRecipient(notificationViewModel.NotificationResponse.NotificationId))
                    notificationRepository.SaveChanges();
            }
            else
            {
                notificationViewModel.NotificationResponse.RecCreatedBy = notificationViewModel.UserId;
                notificationViewModel.NotificationResponse.RecCreatedDate = DateTime.Now;
                notificationViewModel.NotificationResponse.RecLastUpdatedBy = notificationViewModel.UserId;
                notificationViewModel.NotificationResponse.RecLastUpdatedDate = DateTime.Now;
                //Save Notification
                notificationViewModel.NotificationResponse.NotificationId=AddNotification(notificationViewModel.NotificationResponse);
            }
            //Save Notification recipient
            if(!(string.IsNullOrEmpty(notificationViewModel.NotificationResponse.UserId) && notificationViewModel.NotificationResponse.EmployeeId==0))
                AddNotificationRecipient(notificationViewModel.NotificationResponse.CreateRecipientFromClientToServer());
            //// if notification alertdate is today, send email and sms
            ////Send Email
            //SentNotificationEmail(notificationViewModel);
            ////Send SMS
            //SendNotificationSMS(notificationViewModel);
            return true;
        }
        
        public long AddNotification(NotificationResponse notification)
        {
            var _notification = notification.CreateFromClientToServer();
            notificationRepository.Add(_notification);
            notificationRepository.SaveChanges();
            return _notification.NotificationId;
        }
        public long AddNotificationRecipient(NotificationRecipient recipient)
        {
            notificationRecipientRepository.Add(recipient);
            notificationRecipientRepository.SaveChanges();
            return recipient.Id;
        }

        public long UpdateNotificationRecipient(NotificationRecipient recipient)
        {
            notificationRecipientRepository.Update(recipient);
            notificationRecipientRepository.SaveChanges();
            return recipient.Id;
        }
        public long UpdateNotification(NotificationResponse notification)
        {
            notificationRepository.Update(notification.CreateFromClientToServer());
            notificationRepository.SaveChanges();
            return notification.NotificationId;
        }

        public int LoadUnreadNotificationsCount(NotificationRequestParams requestParams)
        {
            return notificationRepository.GetUnreadNotificationsCount(requestParams);
        }

        public NotificationListView LoadAllNotifications(NotificationListViewRequest searchRequset)
        {
            NotificationListView notificationListView=new NotificationListView();
            //if (searchRequset.NotificationRequestParams.SystemGenerated)
            //{
            //    NotificationRecipient recipient=new NotificationRecipient();
            //    recipient.UserId = searchRequset.NotificationRequestParams.UserId;
            //    recipient.NotificationId
            //    notificationRecipientRepository.Add();
            //}
            var notifications = notificationRepository.GetAllNotifications(searchRequset);
            if (notifications.Notifications.Any())
            {
                notificationListView.aaData = notifications.Notifications.Select(x => x.CreateFromServerToClientList());
            }
            else
                notificationListView.aaData = Enumerable.Empty<NotificationListResponse>();

            notificationListView.iTotalDisplayRecords = notifications.TotalFiltered;
            notificationListView.iTotalRecords = notifications.TotalFiltered;
            notificationListView.sEcho = searchRequset.sEcho;
            return notificationListView;
        }
        public NotificationListView LoadAllSentNotifications(NotificationListViewRequest searchRequset)
        {
            NotificationListView notificationListView = new NotificationListView();
            var notifications = notificationRepository.GetAllSentNotifications(searchRequset);
            if (notifications.Notifications.Any())
            {
                notificationListView.aaData = notifications.Notifications.Select(x => x.CreateFromServerToClientList());

            }
            else
                notificationListView.aaData = Enumerable.Empty<NotificationListResponse>();

            notificationListView.iTotalDisplayRecords = notifications.TotalFiltered;
            notificationListView.iTotalRecords = notifications.TotalFiltered;
            notificationListView.sEcho = searchRequset.sEcho;
            return notificationListView;
        }

        private static void SendNotificationSMS(NotificationViewModel notificationViewModel)
        {
            if (!string.IsNullOrEmpty(notificationViewModel.NotificationResponse.MobileNo))
            {
                string smsText = "Notification:\n" + notificationViewModel.NotificationResponse.TitleE + "<br/>" +
                                 notificationViewModel.NotificationResponse.TitleA;
                Utility.SendNotificationSms(smsText, notificationViewModel.NotificationResponse.MobileNo);
            }
        }

        private static void SentNotificationEmail(NotificationViewModel notificationViewModel)
        {
            if (!string.IsNullOrEmpty(notificationViewModel.NotificationResponse.Email))
            {
                string emailBody = "Notification:<br/>" + notificationViewModel.NotificationResponse.TitleE + "<br/>" +
                                   notificationViewModel.NotificationResponse.TitleA;
                Utility.SendEmail(notificationViewModel.NotificationResponse.Email,
                    ConfigurationManager.AppSettings["SubjectNotification"], emailBody);
            }
        }
    }
}
