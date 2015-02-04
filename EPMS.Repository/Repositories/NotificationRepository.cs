using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using EPMS.Interfaces.Repository;
using EPMS.Models.Common;
using EPMS.Models.DomainModels;
using EPMS.Models.ModelMapers.NotificationMapper;
using EPMS.Models.RequestModels.NotificationRequestModels;
using EPMS.Models.ResponseModels;
using EPMS.Models.ResponseModels.NotificationResponseModel;
using EPMS.Repository.BaseRepository;
using Microsoft.Practices.Unity;

namespace EPMS.Repository.Repositories
{
    public class NotificationRepository : BaseRepository<Notification>, INotificationRepository
    {
           #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        public NotificationRepository(IUnityContainer container)
            : base(container)
        {
        }

        /// <summary>
        /// Primary database set
        /// </summary>
        protected override IDbSet<Notification> DbSet
        {
            get { return db.Notification; }
        }

        #endregion
        #region Private

        /// <summary>
        /// Order by Column Names Dictionary statements
        /// </summary>
        private readonly Dictionary<NotificationByColumn, Func<Notification, object>> notificationRequestClause =

            new Dictionary<NotificationByColumn, Func<Notification, object>>
                    {
                        { NotificationByColumn.SerialNo, c => c.NotificationId},
                        { NotificationByColumn.TitleE,  c => c.TitleE},
                        { NotificationByColumn.CategoryId, c => c.CategoryId},
                        { NotificationByColumn.AlertBefore, c => c.AlertBefore},
                        { NotificationByColumn.AlertDate,  c => c.AlertDate},
                        { NotificationByColumn.EmployeeName,  c => c.Employee.EmployeeNameE},
                        { NotificationByColumn.MobileNo, c => c.MobileNo},
                        { NotificationByColumn.Email, c => c.Email},
                        { NotificationByColumn.ReadStatus,  c => c.ReadStatus}
                    };
        #endregion
        public NotificationRequestResponse GetAllNotifications(NotificationListViewRequest searchRequset)
        {
            int fromRow = searchRequset.iDisplayStart;
            int toRow = searchRequset.iDisplayStart + searchRequset.iDisplayLength;
            Expression<Func<Notification, bool>> query =
                s => (((string.IsNullOrEmpty(searchRequset.SearchString)) || (s.TitleE.Contains(searchRequset.SearchString)) || (s.TitleA.Contains(searchRequset.SearchString)) ||
                    (s.Employee.EmployeeNameE.Contains(searchRequset.SearchString)) || (s.Employee.EmployeeNameA.Contains(searchRequset.SearchString)) ||
                    (s.MobileNo.Contains(searchRequset.SearchString)) || (s.Email.Contains(searchRequset.SearchString))) && (s.SystemGenerated)
                    );
            IEnumerable<Notification> notifications;

            if (searchRequset.iSortCol_0 == 0)
            {
                notifications = DbSet
                .Where(query).OrderByDescending(x => x.AlertDate).Skip(fromRow).Take(toRow).ToList();
            }
            else
            {
                notifications = searchRequset.sSortDir_0 == "asc" ?
                DbSet
                .Where(query).OrderBy(notificationRequestClause[searchRequset.NotificationByColumn]).Skip(fromRow).Take(toRow).ToList()
                :
                DbSet
                .Where(query).OrderByDescending(notificationRequestClause[searchRequset.NotificationByColumn]).Skip(fromRow).Take(toRow).ToList();
            }
            return new NotificationRequestResponse { Notifications = notifications, TotalCount = DbSet.Count(), TotalFiltered = DbSet.Count(query) };
        }
    }
}
