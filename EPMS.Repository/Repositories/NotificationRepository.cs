﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using EPMS.Interfaces.Repository;
using EPMS.Models.Common;
using EPMS.Models.DomainModels;
using EPMS.Models.RequestModels.NotificationRequestModels;
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
                        //{ NotificationByColumn.EmployeeName,  c => c.NotificationRecipients.FirstOrDefault().AspNetUser.Employee.EmployeeNameE},
                        //{ NotificationByColumn.MobileNo, c => c.NotificationRecipients.FirstOrDefault().MobileNo},
                        //{ NotificationByColumn.Email, c => c.NotificationRecipients.FirstOrDefault().Email},
                        //{ NotificationByColumn.ReadStatus,  c => c.NotificationRecipients.FirstOrDefault().IsRead}
                    };
        #endregion
        public NotificationRequestResponse GetAllNotifications(NotificationListViewRequest searchRequset)
        {
            int fromRow = searchRequset.iDisplayStart;
            int toRow = searchRequset.iDisplayStart + searchRequset.iDisplayLength;
            Expression<Func<Notification, bool>> query;
            var today = DateTime.Now;
            if (searchRequset.NotificationRequestParams.SystemGenerated)
            {
                query =
                    s =>
                        (
                        ((string.IsNullOrEmpty(searchRequset.SearchString)) ||
                          (s.TitleE.Contains(searchRequset.SearchString)) ||
                          (s.TitleA.Contains(searchRequset.SearchString)) ||
                          (s.NotificationRecipients.FirstOrDefault().AspNetUser.Employee.EmployeeFirstNameE.Contains(searchRequset.SearchString) ||
                          s.NotificationRecipients.FirstOrDefault().AspNetUser.Employee.EmployeeMiddleNameE.Contains(searchRequset.SearchString) ||
                          s.NotificationRecipients.FirstOrDefault().AspNetUser.Employee.EmployeeLastNameE.Contains(searchRequset.SearchString) ||
                          (s.NotificationRecipients.FirstOrDefault().AspNetUser.Employee.EmployeeFirstNameA.Contains(searchRequset.SearchString) ||
                          s.NotificationRecipients.FirstOrDefault().AspNetUser.Employee.EmployeeMiddleNameA.Contains(searchRequset.SearchString) ||
                          s.NotificationRecipients.FirstOrDefault().AspNetUser.Employee.EmployeeMiddleNameA.Contains(searchRequset.SearchString)) ||
                          (s.NotificationRecipients.FirstOrDefault().MobileNo.Contains(searchRequset.SearchString)) ||
                          (s.NotificationRecipients.FirstOrDefault().Email.Contains(searchRequset.SearchString))) 
                          )
                          &&
                          (((s.ForRole == searchRequset.NotificationRequestParams.RoleKey)
                            &&
                            (
                                (s.NotificationRecipients.FirstOrDefault(r => r.UserId == searchRequset.NotificationRequestParams.UserId || r.EmployeeId == searchRequset.NotificationRequestParams.EmployeeId).IsRead == false)
                                ||
                                (s.NotificationRecipients.Count(r => r.UserId == searchRequset.NotificationRequestParams.UserId || r.EmployeeId == searchRequset.NotificationRequestParams.EmployeeId) == 0)
                            ))
                            && (s.AlertAppearDate <= today))
                            );
            }
            else
            {
                query =
                s => (((string.IsNullOrEmpty(searchRequset.SearchString)) || 
                    (s.TitleE.Contains(searchRequset.SearchString)) || 
                    (s.TitleA.Contains(searchRequset.SearchString)) ||
                    (s.NotificationRecipients.FirstOrDefault().AspNetUser.Employee.EmployeeFirstNameE.Contains(searchRequset.SearchString)) ||
                    (s.NotificationRecipients.FirstOrDefault().AspNetUser.Employee.EmployeeMiddleNameE.Contains(searchRequset.SearchString)) ||
                    (s.NotificationRecipients.FirstOrDefault().AspNetUser.Employee.EmployeeLastNameE.Contains(searchRequset.SearchString)) ||
                    (s.NotificationRecipients.FirstOrDefault().AspNetUser.Employee.EmployeeFirstNameA.Contains(searchRequset.SearchString)) ||
                    (s.NotificationRecipients.FirstOrDefault().AspNetUser.Employee.EmployeeMiddleNameA.Contains(searchRequset.SearchString)) ||
                    (s.NotificationRecipients.FirstOrDefault().AspNetUser.Employee.EmployeeLastNameA.Contains(searchRequset.SearchString)) ||
                    (s.NotificationRecipients.FirstOrDefault().MobileNo.Contains(searchRequset.SearchString)) ||
                    (s.NotificationRecipients.FirstOrDefault().Email.Contains(searchRequset.SearchString))) 
                    &&
                    (((s.NotificationRecipients.Any(r=>r.UserId == searchRequset.NotificationRequestParams.UserId))||(s.NotificationRecipients.Any(r=>r.EmployeeId == searchRequset.NotificationRequestParams.EmployeeId))) &&
                           (s.AlertAppearDate <= today))
                    );
            }
            IEnumerable<Notification> notifications;

            if (searchRequset.iSortCol_0 == 0)
            {
                notifications = DbSet
                .Where(query).OrderBy(x => x.NotificationRecipients.Any(y => y.IsRead)).Skip(fromRow).Take(toRow).ToList();
            }
            else if (searchRequset.iSortCol_0 == 8 || searchRequset.SearchString.ToLower() == "yes" || searchRequset.SearchString.ToLower() == "no")
            {
                bool isNotified;
                switch (searchRequset.SearchString.ToLower())
                {
                    case "yes":
                        isNotified = true;
                        if (searchRequset.NotificationRequestParams.SystemGenerated)
                        {
                            query = s => (((string.IsNullOrEmpty(searchRequset.SearchString)) || s.NotificationRecipients.Any(r => r.IsRead == isNotified)) 
                            &&
                            (((s.ForRole == searchRequset.NotificationRequestParams.RoleKey) || (s.NotificationRecipients.FirstOrDefault().UserId == searchRequset.NotificationRequestParams.UserId) || (s.NotificationRecipients.FirstOrDefault().EmployeeId == searchRequset.NotificationRequestParams.EmployeeId)) &&
                           (s.AlertAppearDate <= today)));
                        }
                        else
                        {
                            query = s => (((string.IsNullOrEmpty(searchRequset.SearchString)) || s.NotificationRecipients.Any(r => r.IsRead == isNotified))
                                && (((s.NotificationRecipients.Any(r => r.UserId == searchRequset.NotificationRequestParams.UserId)) || 
                                (s.NotificationRecipients.Any(r => r.EmployeeId == searchRequset.NotificationRequestParams.EmployeeId))) && (s.AlertAppearDate <= today))
                                );
                        }
                        break;
                    case "no":
                        isNotified = false;
                        if (searchRequset.NotificationRequestParams.SystemGenerated)
                        {
                            query = s => (((string.IsNullOrEmpty(searchRequset.SearchString)) || s.NotificationRecipients.All(r => r.IsRead == isNotified))
                            &&
                            (((s.ForRole == searchRequset.NotificationRequestParams.RoleKey) || (s.NotificationRecipients.FirstOrDefault().UserId == searchRequset.NotificationRequestParams.UserId) || (s.NotificationRecipients.FirstOrDefault().EmployeeId == searchRequset.NotificationRequestParams.EmployeeId)) &&
                           (s.AlertAppearDate <= today)));
                        }
                        else
                        {
                            query = s => (((string.IsNullOrEmpty(searchRequset.SearchString)) || s.NotificationRecipients.All(r => r.IsRead == isNotified))
                                && (((s.NotificationRecipients.Any(r => r.UserId == searchRequset.NotificationRequestParams.UserId)) ||
                                (s.NotificationRecipients.Any(r => r.EmployeeId == searchRequset.NotificationRequestParams.EmployeeId))) && (s.AlertAppearDate <= today))
                                );
                        }
                        break;
                    case "":
                        if (searchRequset.NotificationRequestParams.SystemGenerated)
                        {
                            query = s => (((string.IsNullOrEmpty(searchRequset.SearchString)) ||
                          (s.TitleE.Contains(searchRequset.SearchString)) || (s.TitleA.Contains(searchRequset.SearchString)) ||
                          (s.NotificationRecipients.FirstOrDefault().AspNetUser.Employee.EmployeeFirstNameE.Contains(searchRequset.SearchString) ||
                          s.NotificationRecipients.FirstOrDefault().AspNetUser.Employee.EmployeeMiddleNameE.Contains(searchRequset.SearchString) ||
                          s.NotificationRecipients.FirstOrDefault().AspNetUser.Employee.EmployeeLastNameE.Contains(searchRequset.SearchString) ||
                          (s.NotificationRecipients.FirstOrDefault().AspNetUser.Employee.EmployeeFirstNameA.Contains(searchRequset.SearchString) ||
                          s.NotificationRecipients.FirstOrDefault().AspNetUser.Employee.EmployeeMiddleNameA.Contains(searchRequset.SearchString) ||
                          s.NotificationRecipients.FirstOrDefault().AspNetUser.Employee.EmployeeMiddleNameA.Contains(searchRequset.SearchString)) ||
                          (s.NotificationRecipients.FirstOrDefault().MobileNo.Contains(searchRequset.SearchString)) ||
                          (s.NotificationRecipients.FirstOrDefault().Email.Contains(searchRequset.SearchString))))
                            &&
                            (((s.ForRole == searchRequset.NotificationRequestParams.RoleKey) || (s.NotificationRecipients.FirstOrDefault().UserId == searchRequset.NotificationRequestParams.UserId) || (s.NotificationRecipients.FirstOrDefault().EmployeeId == searchRequset.NotificationRequestParams.EmployeeId)) &&
                           (s.AlertAppearDate <= today)));
                        }
                        else
                        {
                            query = s => (((string.IsNullOrEmpty(searchRequset.SearchString)) ||
                          (s.TitleE.Contains(searchRequset.SearchString)) || (s.TitleA.Contains(searchRequset.SearchString)) ||
                          (s.NotificationRecipients.FirstOrDefault().AspNetUser.Employee.EmployeeFirstNameE.Contains(searchRequset.SearchString) ||
                          s.NotificationRecipients.FirstOrDefault().AspNetUser.Employee.EmployeeMiddleNameE.Contains(searchRequset.SearchString) ||
                          s.NotificationRecipients.FirstOrDefault().AspNetUser.Employee.EmployeeLastNameE.Contains(searchRequset.SearchString) ||
                          (s.NotificationRecipients.FirstOrDefault().AspNetUser.Employee.EmployeeFirstNameA.Contains(searchRequset.SearchString) ||
                          s.NotificationRecipients.FirstOrDefault().AspNetUser.Employee.EmployeeMiddleNameA.Contains(searchRequset.SearchString) ||
                          s.NotificationRecipients.FirstOrDefault().AspNetUser.Employee.EmployeeMiddleNameA.Contains(searchRequset.SearchString)) ||
                          (s.NotificationRecipients.FirstOrDefault().MobileNo.Contains(searchRequset.SearchString)) ||
                          (s.NotificationRecipients.FirstOrDefault().Email.Contains(searchRequset.SearchString))))
                                && (((s.NotificationRecipients.Any(r => r.UserId == searchRequset.NotificationRequestParams.UserId)) ||
                                (s.NotificationRecipients.Any(r => r.EmployeeId == searchRequset.NotificationRequestParams.EmployeeId))) && (s.AlertAppearDate <= today))
                                );
                        }
                        break;
                }
                
                notifications = searchRequset.sSortDir_0 == "asc"
                    ? DbSet
                        .Where(query).Select(a => new
                        {
                            a.NotificationId,
                            a.TitleE,
                            a.TitleA,
                            a.SystemGenerated,
                            a.SubCategoryId,
                            a.AlertAppearDate,
                            a.AlertBefore,
                            a.ItemId,
                            a.AlertDateType,
                            a.IsEmailSent,
                            a.IsSMSsent,
                            a.AlertDate,
                            a.CategoryId,
                            a.ForAdmin,
                            a.NotificationRecipients,
                            a.RecCreatedBy,
                            a.RecCreatedDate,
                            a.RecLastUpdatedBy,
                            a.RecLastUpdatedDate,
                            isRead = a.NotificationRecipients.All(nr => nr.IsRead == false)
                        }
                        ).OrderByDescending(a => a.isRead).Skip(fromRow).Take(toRow).ToList().Select(n => new Notification
                        {
                            NotificationId = n.NotificationId,
                            TitleE = n.TitleE,
                            TitleA = n.TitleA,
                            SystemGenerated = n.SystemGenerated,
                            SubCategoryId = n.SubCategoryId,
                            AlertAppearDate = n.AlertAppearDate,
                            AlertBefore = n.AlertBefore,
                            ItemId = n.ItemId,
                            AlertDateType = n.AlertDateType,
                            IsEmailSent = n.IsEmailSent,
                            IsSMSsent = n.IsSMSsent,
                            AlertDate = n.AlertDate,
                            CategoryId = n.CategoryId,
                            ForAdmin = n.ForAdmin,
                            NotificationRecipients = n.NotificationRecipients,
                            RecCreatedBy = n.RecCreatedBy,
                            RecCreatedDate = n.RecCreatedDate,
                            RecLastUpdatedBy = n.RecLastUpdatedBy,
                            RecLastUpdatedDate = n.RecLastUpdatedDate,
                        }).ToList()
                    : DbSet
                        .Where(query).Select(a => new
                        {
                            a.NotificationId,
                            a.TitleE,
                            a.TitleA,
                            a.SystemGenerated,
                            a.SubCategoryId,
                            a.AlertAppearDate,
                            a.AlertBefore,
                            a.ItemId,
                            a.AlertDateType,
                            a.IsEmailSent,
                            a.IsSMSsent,
                            a.AlertDate,
                            a.CategoryId,
                            a.ForAdmin,
                            a.NotificationRecipients,
                            a.RecCreatedBy,
                            a.RecCreatedDate,
                            a.RecLastUpdatedBy,
                            a.RecLastUpdatedDate,
                            isRead = a.NotificationRecipients.All(nr => nr.IsRead == false)
                        }
                        ).OrderBy(n=>n.isRead).Skip(fromRow).Take(toRow).ToList().Select(n => new Notification
                        {
                            NotificationId = n.NotificationId,
                            TitleE = n.TitleE,
                            TitleA = n.TitleA,
                            SystemGenerated = n.SystemGenerated,
                            SubCategoryId = n.SubCategoryId,
                            AlertAppearDate = n.AlertAppearDate,
                            AlertBefore = n.AlertBefore,
                            ItemId = n.ItemId,
                            AlertDateType = n.AlertDateType,
                            IsEmailSent = n.IsEmailSent,
                            IsSMSsent = n.IsSMSsent,
                            AlertDate = n.AlertDate,
                            CategoryId = n.CategoryId,
                            ForAdmin = n.ForAdmin,
                            NotificationRecipients = n.NotificationRecipients,
                            RecCreatedBy = n.RecCreatedBy,
                            RecCreatedDate = n.RecCreatedDate,
                            RecLastUpdatedBy = n.RecLastUpdatedBy,
                            RecLastUpdatedDate = n.RecLastUpdatedDate,
                        }).ToList();
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
        public NotificationRequestResponse GetAllSentNotifications(NotificationListViewRequest searchRequset)
        {
            int fromRow = searchRequset.iDisplayStart;
            int toRow = searchRequset.iDisplayStart + searchRequset.iDisplayLength;
            Expression<Func<Notification, bool>> query =
                   s =>
                       (((string.IsNullOrEmpty(searchRequset.SearchString)) ||
                         (s.TitleE.Contains(searchRequset.SearchString)) ||
                         (s.TitleA.Contains(searchRequset.SearchString)) ||
                         (s.NotificationRecipients.FirstOrDefault().AspNetUser.Employee.EmployeeFirstNameE.Contains(searchRequset.SearchString)) ||
                         (s.NotificationRecipients.FirstOrDefault().AspNetUser.Employee.EmployeeMiddleNameE.Contains(searchRequset.SearchString)) ||
                         (s.NotificationRecipients.FirstOrDefault().AspNetUser.Employee.EmployeeLastNameE.Contains(searchRequset.SearchString)) ||
                         (s.NotificationRecipients.FirstOrDefault().AspNetUser.Employee.EmployeeFirstNameA.Contains(searchRequset.SearchString)) ||
                         (s.NotificationRecipients.FirstOrDefault().AspNetUser.Employee.EmployeeMiddleNameA.Contains(searchRequset.SearchString)) ||
                         (s.NotificationRecipients.FirstOrDefault().AspNetUser.Employee.EmployeeLastNameA.Contains(searchRequset.SearchString)) ||
                         (s.NotificationRecipients.FirstOrDefault().MobileNo.Contains(searchRequset.SearchString)) ||
                         (s.NotificationRecipients.FirstOrDefault().Email.Contains(searchRequset.SearchString)))
                         &&
                         (s.RecCreatedBy == searchRequset.NotificationRequestParams.UserId));
            IEnumerable<Notification> notifications;

            if (searchRequset.iSortCol_0 == 0)
            {
                notifications = DbSet
                .Where(query).OrderByDescending(x => x.RecCreatedDate).Skip(fromRow).Take(toRow).ToList();
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

        public int GetUnreadNotificationsCount(NotificationRequestParams requestParams)
        {
            var today = DateTime.Now;
         
            Expression<Func<Notification, bool>> query;

            if (requestParams.SystemGenerated)
            {
                query = s => 
                    (
                    (
                        (
                            (//s.ForAdmin == true || 
                            s.ForRole == requestParams.RoleKey)
                            && 
                            (
                                (s.NotificationRecipients.FirstOrDefault(r => r.UserId == requestParams.UserId || r.EmployeeId == requestParams.EmployeeId).IsRead == false)
                                || 
                                (s.NotificationRecipients.Count(r => r.UserId == requestParams.UserId || r.EmployeeId == requestParams.EmployeeId) == 0)
                            )
                        ) 
                        || 
                        (s.NotificationRecipients.FirstOrDefault(r => r.UserId == requestParams.UserId || r.EmployeeId == requestParams.EmployeeId).IsRead == false)
                    )
                    && 
                    (s.AlertAppearDate <= today)
                    );
            }
            else
            {
                query = s => (((s.NotificationRecipients.FirstOrDefault(r => r.UserId == requestParams.UserId || r.EmployeeId == requestParams.EmployeeId).IsRead == false))
                          && (s.AlertAppearDate <= today));
            }
            return DbSet.Count(query);
        }

        public long GetNotificationsIdByCategories(int categoryId, long subCategoryId, long itemId)
        {
            var notif=  DbSet.FirstOrDefault(x => x.CategoryId == categoryId && x.SubCategoryId == subCategoryId && x.ItemId==itemId);
            if (notif != null)
                return notif.NotificationId;
            return 0;
        }

        public IEnumerable<Notification> SendEmailNotifications()
        {
            DateTime today= DateTime.Now;
            return DbSet.Where(x => x.AlertAppearDate <= today && (x.IsEmailSent==false || x.IsSMSsent==false));
        }
    }
}
