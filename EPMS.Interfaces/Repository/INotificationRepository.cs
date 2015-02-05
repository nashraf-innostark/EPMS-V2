using EPMS.Models.DomainModels;
using EPMS.Models.RequestModels.NotificationRequestModels;
using EPMS.Models.ResponseModels.NotificationResponseModel;

namespace EPMS.Interfaces.Repository
{
    public interface INotificationRepository:IBaseRepository<Notification,long>
    {
        NotificationRequestResponse GetAllNotifications(NotificationListViewRequest searchRequset);
        //int GetUnreadNotificationsCount(NotificationListViewRequest searchRequset);
    }
}
