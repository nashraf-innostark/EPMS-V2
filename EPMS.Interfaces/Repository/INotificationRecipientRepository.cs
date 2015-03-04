using EPMS.Models.DomainModels;

namespace EPMS.Interfaces.Repository
{
    public interface INotificationRecipientRepository : IBaseRepository<NotificationRecipient, long>
    {
        bool DeleteRecipient(long notificationId);
    }
}
