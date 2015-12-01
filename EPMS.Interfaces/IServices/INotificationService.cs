using System;
using System.Collections.Generic;
using EPMS.Models.DomainModels;
using EPMS.Models.RequestModels.NotificationRequestModels;
using EPMS.Models.ResponseModels.NotificationResponseModel;

namespace EPMS.Interfaces.IServices
{
    public interface INotificationService
    {
        NotificationViewModel LoadNotificationAndBaseData(long? notificationId);
        NotificationViewModel LoadNotificationDetailsAndBaseData(long? notificationId, string userId, long employeeId);
        bool AddUpdateNotification(NotificationResponse notificationResponse);
        bool AddUpdateMeetingNotification(NotificationViewModel notificationViewModel, List<long> employeeIds);
        bool AddUpdateInvoiceNotification(NotificationViewModel notificationViewModel, long customerId);
        long AddNotification(NotificationResponse notification);
        long UpdateNotification(NotificationResponse notification);
        int LoadUnreadNotificationsCount(NotificationRequestParams requestParams);
        NotificationListView LoadAllNotifications(NotificationListViewRequest searchRequset);
        NotificationListView LoadAllSentNotifications(NotificationListViewRequest searchRequset);
        long AddNotificationRecipient(NotificationRecipient notification);
        long UpdateNotificationRecipient(NotificationRecipient notification);
        void SendEmailNotifications();
        void CreateNotification(string notificationFor, long itemId, DateTime? alertDate);
        void GenerateNotificationDescription(NotificationResponse notificationResponse);
    }
}
