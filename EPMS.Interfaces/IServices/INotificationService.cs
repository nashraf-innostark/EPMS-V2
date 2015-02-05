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
        NotificationViewModel LoadNotificationDetailsAndBaseData(long? notificationId);
        bool AddUpdateNotification(NotificationViewModel notificationViewModel);
        bool AddNotification(NotificationResponse notification);
        bool UpdateNotification(NotificationResponse notification);
        int LoadUnreadNotificationsCount(NotificationRequestParams requestParams);
        NotificationListView LoadAllNotifications(NotificationListViewRequest searchRequset);
        NotificationListView LoadAllSentNotifications(NotificationListViewRequest searchRequset);
    }
}
