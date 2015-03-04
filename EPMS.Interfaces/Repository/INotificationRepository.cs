using System.Collections.Generic;
using EPMS.Models.DomainModels;
using EPMS.Models.RequestModels.NotificationRequestModels;
using EPMS.Models.ResponseModels.NotificationResponseModel;

namespace EPMS.Interfaces.Repository
{
    public interface INotificationRepository:IBaseRepository<Notification,long>
    {
        NotificationRequestResponse GetAllNotifications(NotificationListViewRequest searchRequset);
        NotificationRequestResponse GetAllSentNotifications(NotificationListViewRequest searchRequset);
        int GetUnreadNotificationsCount(NotificationRequestParams requestParams);
        long GetNotificationsIdByCategories(int categoryId, long subCategoryId, long itemId);
        IEnumerable<Notification> SendEmailNotifications();
    }
}
