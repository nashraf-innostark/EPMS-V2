using System;
using System.Collections.Generic;
using System.Linq;
using EPMS.Interfaces.IServices;
using EPMS.Interfaces.Repository;
using EPMS.Models.ModelMapers;
using EPMS.Models.ModelMapers.NotificationMapper;
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
            var employees = employeeRepository.GetAll();
            notificationViewModel.EmployeeDDL = employees.Select(x => x.CreateForEmployeeDDL());
            if (notificationId != null && notificationId > 0)
            {
                var notification = notificationRepository.Find((long) notificationId);
                if(notification!=null)
                    notificationViewModel.NotificationResponse = notification.CreateFromServerToClient();
            }
            return notificationViewModel;
        }

        public bool AddNotification(NotificationResponse notification)
        {
            throw new NotImplementedException();
        }

        public bool UpdateNotification(NotificationResponse notification)
        {
            throw new NotImplementedException();
        }
    }
}
