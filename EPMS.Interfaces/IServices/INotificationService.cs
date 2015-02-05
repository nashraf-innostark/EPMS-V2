using System.Collections.Generic;
using EPMS.Models.RequestModels.NotificationRequestModels;
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
        NotificationListView LoadAllNotifications(NotificationListViewRequest searchRequset);
        NotificationListView LoadAllSentNotifications(NotificationListViewRequest searchRequset);
    }
}
