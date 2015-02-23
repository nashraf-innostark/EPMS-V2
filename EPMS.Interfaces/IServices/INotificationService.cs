using System.Collections.Generic;
using EPMS.Models.DomainModels;
using EPMS.Models.RequestModels.NotificationRequestModels;
using EPMS.Models.ResponseModels.NotificationResponseModel;

namespace EPMS.Interfaces.IServices
{
    public interface INotificationService
    {
        IEnumerable<NotificationResponse> GetAll();
        NotificationResponse FindNotification(long notificationId);
        NotificationViewModel LoadNotificationAndBaseData(long? notificationId);
        NotificationViewModel LoadNotificationDetailsAndBaseData(long? notificationId,string userId);
        bool AddUpdateNotification(NotificationViewModel notificationViewModel);
        bool AddUpdateMeetingNotification(NotificationViewModel notificationViewModel, List<long> employeeIds);
        long AddNotification(NotificationResponse notification);
        long UpdateNotification(NotificationResponse notification);
        int LoadUnreadNotificationsCount(NotificationRequestParams requestParams);
        NotificationListView LoadAllNotifications(NotificationListViewRequest searchRequset);
        NotificationListView LoadAllSentNotifications(NotificationListViewRequest searchRequset);
        long AddNotificationRecipient(NotificationRecipient notification);
        long UpdateNotificationRecipient(NotificationRecipient notification);
        void SendEmailNotifications();
    }
}
