using System.Data.Entity;
using System.Linq;
using EPMS.Interfaces.Repository;
using EPMS.Models.DomainModels;
using EPMS.Repository.BaseRepository;
using Microsoft.Practices.Unity;

namespace EPMS.Repository.Repositories
{
    public class NotificationRecipientRepository : BaseRepository<NotificationRecipient>, INotificationRecipientRepository
    {
        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        public NotificationRecipientRepository(IUnityContainer container)
            : base(container)
        {
        }

        /// <summary>
        /// Primary database set
        /// </summary>
        protected override IDbSet<NotificationRecipient> DbSet
        {
            get { return db.NotificationRecipient; }
        }

        #endregion

        public bool DeleteRecipient(long notificationId)
        {
            var itemsToDelete = DbSet.Where(x => x.NotificationId == notificationId);
            foreach (var recipient in itemsToDelete)
            {
                DbSet.Remove(recipient);
            }
            return true;
        }
    }
}
