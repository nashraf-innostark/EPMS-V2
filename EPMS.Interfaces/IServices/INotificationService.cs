using System.Collections.Generic;
using EPMS.Models.ResponseModels.NotificationResponseModel;

namespace EPMS.Interfaces.IServices
{
    public interface INotificationService
    {
        IEnumerable<NotificationResponse> GetAll();
        NotificationResponse FindNotification(long notificationId);
        NotificationViewModel LoadNotificationAndBaseData(long? notificationId);
        bool AddNotification(NotificationResponse notification);
        bool UpdateNotification(NotificationResponse notification);
    }
}
