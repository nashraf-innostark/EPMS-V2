using System;
using System.Collections.Generic;
using System.Linq;
using EPMS.Interfaces.IServices;
using EPMS.Interfaces.Repository;
using EPMS.Models.ModelMapers.NotificationMapper;
using EPMS.Models.ResponseModels.NotificationResponseModel;

namespace EPMS.Implementation.Services
{
    public class NotificationService:INotificationService
    {
        private readonly INotificationRepository notificationRepository;

        public NotificationService(INotificationRepository notificationRepository)
        {
            this.notificationRepository = notificationRepository;
        }

        public IEnumerable<NotificationResponse> GetAll()
        {
            return notificationRepository.GetAll().Select(x => x.CreateFromServerToClient());
        }

        public NotificationResponse FindNotification(long notificationId)
        {
            throw new NotImplementedException();
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
