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
        private readonly INotificationRepository notificationRepository;
        private readonly IEmployeeRepository employeeRepository;

        public NotificationService(INotificationRepository notificationRepository, IEmployeeRepository employeeRepository)
        {
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

        public bool AddUpdateNotification(NotificationViewModel notificationViewModel)
        {
            if (notificationViewModel.NotificationResponse.NotificationId > 0)
            {
                notificationViewModel.NotificationResponse.RecLastUpdatedBy = notificationViewModel.UserId;
                notificationViewModel.NotificationResponse.RecLastUpdatedDate = DateTime.Now;

                UpdateNotification(notificationViewModel.NotificationResponse);
            }
            else
            {
                notificationViewModel.NotificationResponse.RecCreatedBy = notificationViewModel.UserId;
                notificationViewModel.NotificationResponse.RecCreatedDate = DateTime.Now;
                notificationViewModel.NotificationResponse.RecLastUpdatedBy = notificationViewModel.UserId;
                notificationViewModel.NotificationResponse.RecLastUpdatedDate = DateTime.Now;

                AddNotification(notificationViewModel.NotificationResponse);
            }
            //Send Email
            if (!string.IsNullOrEmpty(notificationViewModel.NotificationResponse.Email))
            {
                string emailBody = notificationViewModel.NotificationResponse.TitleE + "<br/>" + notificationViewModel.NotificationResponse.TitleA;
                Utility.SendEmail(notificationViewModel.NotificationResponse.Email, ConfigurationManager.AppSettings["SubjectNotification"], emailBody);
            }
            //Send SMS
            if (!string.IsNullOrEmpty(notificationViewModel.NotificationResponse.MobileNo))
            {

            }
            return true;
        }

        public bool AddNotification(NotificationResponse notification)
        {
            notificationRepository.Add(notification.CreateFromClientToServer());
            notificationRepository.SaveChanges();
            return true;
        }

        public bool UpdateNotification(NotificationResponse notification)
        {
            notificationRepository.Update(notification.CreateFromClientToServer());
            notificationRepository.SaveChanges();
            return true;
        }

        public int LoadUnreadNotificationsCount(NotificationRequestParams requestParams)
        {
            return notificationRepository.GetUnreadNotificationsCount(requestParams);
        }

        public NotificationListView LoadAllNotifications(NotificationListViewRequest searchRequset)
        {
            NotificationListView notificationListView=new NotificationListView();
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
    }
}
