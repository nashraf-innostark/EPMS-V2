using System;
using System.Globalization;
using System.Linq;
using EPMS.Models.DomainModels;
using EPMS.Models.ResponseModels.EmployeeResponseModel;
using EPMS.Models.ResponseModels.NotificationResponseModel;

namespace EPMS.Models.ModelMapers.NotificationMapper
{
    public static class NotificationMapper
    {
        public static NotificationResponse CreateFromServerToClient(this Notification notification)
        {
            return new NotificationResponse
            {
                NotificationId = notification.NotificationId,
                TitleA = notification.TitleA,
                TitleE = notification.TitleE,
                CategoryId = notification.CategoryId,
                SubCategoryId = Convert.ToInt64(notification.SubCategoryId),
                AlertBefore = notification.AlertBefore,
                AlertDateType = notification.AlertDateType,
                AlertDate = notification.AlertDate.ToString("dd/MM/yyyy", new CultureInfo("en")),
                UserId = notification.NotificationRecipients.FirstOrDefault().UserId,
                MobileNo = notification.NotificationRecipients.FirstOrDefault().MobileNo,
                Email = notification.NotificationRecipients.FirstOrDefault().Email,
                ReadStatus = notification.NotificationRecipients.FirstOrDefault().IsRead,
                EmployeeId = Convert.ToInt64(notification.NotificationRecipients.FirstOrDefault().EmployeeId),
                RecCreatedBy = notification.RecCreatedBy,
                RecCreatedDate = notification.RecCreatedDate,
                RecLastUpdatedBy = notification.RecLastUpdatedBy,
                RecLastUpdatedDate = notification.RecLastUpdatedDate
            };
        }
        public static Notification CreateFromClientToServer(this NotificationResponse notification)
        {
            DateTime alertAppearDate = new DateTime(); 
            switch (notification.AlertBefore)
            {
                case 1: alertAppearDate = DateTime.ParseExact(notification.AlertDate, "dd/MM/yyyy", new CultureInfo("en")).AddMonths(-1); break;
                case 2: alertAppearDate = DateTime.ParseExact(notification.AlertDate, "dd/MM/yyyy", new CultureInfo("en")).AddDays(-7); break;
                case 3: alertAppearDate = DateTime.ParseExact(notification.AlertDate, "dd/MM/yyyy", new CultureInfo("en")).AddDays(-1); break;
            }
            return new Notification
            {
                NotificationId = notification.NotificationId,
                TitleA = notification.TitleA,
                TitleE = notification.TitleE,
                CategoryId = notification.CategoryId,
                SubCategoryId = notification.SubCategoryId,
                AlertBefore = notification.AlertBefore,
                AlertDateType = notification.AlertDateType,
                AlertDate = DateTime.ParseExact(notification.AlertDate, "dd/MM/yyyy", new CultureInfo("en")),
                AlertAppearDate = alertAppearDate,
                SystemGenerated = notification.SystemGenerated,

                RecCreatedBy = notification.RecCreatedBy,
                RecCreatedDate = notification.RecCreatedDate,
                RecLastUpdatedBy = notification.RecLastUpdatedBy,
                RecLastUpdatedDate = notification.RecLastUpdatedDate
            };
        }
        public static EmployeeDDL CreateForEmployeeDDL(this Employee source)
        {
            return new EmployeeDDL
            {
                UserId = source.AspNetUsers.FirstOrDefault()!=null?source.AspNetUsers.FirstOrDefault().Id:"",
                EmployeeId = source.EmployeeId,
                EmployeeNameE = source.EmployeeNameE,
                EmployeeNameA = source.EmployeeNameA,
                Email = source.Email,
                MobileNo = source.EmployeeMobileNum
            };
        }
        public static NotificationListResponse CreateFromServerToClientList(this Notification notification)
        {
            NotificationListResponse notificationListResponse=new NotificationListResponse();
            notificationListResponse.NotificationId = notification.NotificationId;
            notificationListResponse.NotificationName = System.Threading.Thread.CurrentThread.CurrentCulture.ToString() == "en" ? notification.TitleE : notification.TitleA;      
            notificationListResponse.AlertEndTime = notification.AlertDate.ToString("dd/MM/yyyy", new CultureInfo("en"));
            if (notification.NotificationRecipients.Count > 0)
            {
                if (notification.NotificationRecipients.FirstOrDefault().EmployeeId > 0)
                {
                    notificationListResponse.EmployeeId = Convert.ToInt64(notification.NotificationRecipients.FirstOrDefault().EmployeeId);
                    notificationListResponse.EmployeeName = System.Threading.Thread.CurrentThread.CurrentCulture.ToString() == "en" ? notification.NotificationRecipients.FirstOrDefault().AspNetUser.Employee.EmployeeNameA : notification.NotificationRecipients.FirstOrDefault().AspNetUser.Employee.EmployeeNameA;
                }

                notificationListResponse.MobileNo = notification.NotificationRecipients.FirstOrDefault().MobileNo;
                notificationListResponse.Email = notification.NotificationRecipients.FirstOrDefault().Email;
                notificationListResponse.Notified = notification.NotificationRecipients.FirstOrDefault().IsRead ? Resources.Notification.Yes : Resources.Notification.No;
           
            }
            
            switch (notification.CategoryId)
            {
                case 1: notificationListResponse.CategoryName = Resources.Notification.Company; break;
                case 2: notificationListResponse.CategoryName = Resources.Notification.Documents; break;
                case 3: notificationListResponse.CategoryName = Resources.Notification.Employees; break;
                case 4: notificationListResponse.CategoryName = Resources.Notification.Meetings; break;
                case 5: notificationListResponse.CategoryName = Resources.Notification.Other; break;
                case 6: notificationListResponse.CategoryName = Resources.Notification.Project; break;
                case 7: notificationListResponse.CategoryName = Resources.Notification.ProjectTask; break;
                case 8: notificationListResponse.CategoryName = Resources.Notification.ProjectTask; break;
                default: notificationListResponse.CategoryName = Resources.Notification.Other; break;
            }
            switch (notification.AlertBefore)
            {
                case 1: notificationListResponse.AlertTime = Resources.Notification.BeforeOneMonth; break;
                case 2: notificationListResponse.AlertTime = Resources.Notification.BeforeOneWeek; break;
                case 3: notificationListResponse.AlertTime = Resources.Notification.BeforeOneDay; break;
            }

            return notificationListResponse;
        }
        public static NotificationRecipient CreateRecipientFromClientToServer(this NotificationResponse source)
        {
            NotificationRecipient recipient=new NotificationRecipient();
            recipient.NotificationId = source.NotificationId;
            recipient.UserId = source.UserId;
            if (string.IsNullOrEmpty(source.UserId))
            {
                recipient.MobileNo = source.MobileNo;
                recipient.Email = source.Email;
            }
            recipient.IsRead = source.ReadStatus;
            recipient.EmployeeId = source.EmployeeId;
            return recipient;
        }
    }
}
