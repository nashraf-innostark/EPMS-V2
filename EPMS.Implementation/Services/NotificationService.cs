﻿using System.Collections.Generic;
using System.Linq;
using EPMS.Interfaces.IServices;
using EPMS.Interfaces.Repository;
using EPMS.Models.DomainModels;
using EPMS.Models.ModelMapers.NotificationMapper;
using EPMS.Models.RequestModels.NotificationRequestModels;
using EPMS.Models.ResponseModels.NotificationResponseModel;

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

        public NotificationListView LoadAllNotifications(NotificationListViewRequest searchRequset)
        {
            throw new System.NotImplementedException();
        }
    }
}
