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
            NotificationResponse response=new NotificationResponse
            {
                NotificationId = notification.NotificationId,
                TitleA = notification.TitleA,
                TitleE = notification.TitleE,
                CategoryId = notification.CategoryId,
                SubCategoryId = Convert.ToInt64(notification.SubCategoryId),
                ItemId = Convert.ToInt64(notification.ItemId),
                AlertBefore = notification.AlertBefore,
                AlertDateType = notification.AlertDateType,
                AlertDate = notification.AlertDate.ToString("dd/MM/yyyy", new CultureInfo("en")),
               
                IsEmailSent = notification.IsEmailSent,
                IsSMSsent = notification.IsSMSsent,

                RecCreatedBy = notification.RecCreatedBy,
                RecCreatedDate = notification.RecCreatedDate,
                RecLastUpdatedBy = notification.RecLastUpdatedBy,
                RecLastUpdatedDate = notification.RecLastUpdatedDate
            };
            if (notification.NotificationRecipients.FirstOrDefault() != null)
            {
                response.UserId = notification.NotificationRecipients.FirstOrDefault().UserId;
                response.MobileNo = notification.NotificationRecipients.FirstOrDefault().MobileNo;
                response.Email = notification.NotificationRecipients.FirstOrDefault().Email;
                response.ReadStatus = notification.NotificationRecipients.FirstOrDefault().IsRead;
                response.EmployeeId = Convert.ToInt64(notification.NotificationRecipients.FirstOrDefault().EmployeeId);
            }
            return response;
        }
        public static NotificationResponse CreateDetailsFromServerToClient(this Notification notification, NotificationRecipient notificationRecipient)
        {
            return new NotificationResponse
            {
                NotificationId = notification.NotificationId,
                TitleA = notification.TitleA,
                TitleE = notification.TitleE,
                CategoryId = notification.CategoryId,
                SubCategoryId = Convert.ToInt64(notification.SubCategoryId),
                ItemId = Convert.ToInt64(notification.ItemId),
                AlertBefore = notification.AlertBefore,
                AlertDateType = notification.AlertDateType,
                AlertDate = notification.AlertDate.ToString("dd/MM/yyyy", new CultureInfo("en")),
                UserId = notification.NotificationRecipients.FirstOrDefault().UserId,
                MobileNo = notification.NotificationRecipients.FirstOrDefault().MobileNo,
                Email = notification.NotificationRecipients.FirstOrDefault().Email,
                ReadStatus = notificationRecipient.IsRead,
                EmployeeId = Convert.ToInt64(notification.NotificationRecipients.FirstOrDefault().EmployeeId),

                IsEmailSent = notification.IsEmailSent,
                IsSMSsent = notification.IsSMSsent,

                RecCreatedBy = notification.RecCreatedBy,
                RecCreatedDate = notification.RecCreatedDate,
                RecLastUpdatedBy = notification.RecLastUpdatedBy,
                RecLastUpdatedDate = notification.RecLastUpdatedDate
            };
        }
        public static Notification CreateFromClientToServer(this NotificationResponse notification)
        {
            var notfication = new Notification
            {
                NotificationId = notification.NotificationId,
                TitleA = notification.TitleA,
                TitleE = notification.TitleE,
                CategoryId = notification.CategoryId,
                SubCategoryId = notification.SubCategoryId,
                ItemId = Convert.ToInt64(notification.ItemId),
                AlertBefore = notification.AlertBefore,
                AlertDateType = notification.AlertDateType,
                AlertDate = DateTime.ParseExact(notification.AlertDate, "dd/MM/yyyy", new CultureInfo("en")),
                AlertAppearDate = DateTime.ParseExact(notification.AlertDate, "dd/MM/yyyy", new CultureInfo("en")).AddDays(-notification.AlertBefore),
                SystemGenerated = notification.SystemGenerated,
                ForAdmin = notification.ForAdmin,
                IsEmailSent = notification.IsEmailSent,
                IsSMSsent = notification.IsSMSsent,

                RecCreatedBy = notification.RecCreatedBy,
                RecCreatedDate = DateTime.Now,
                RecLastUpdatedBy = notification.RecCreatedBy,
                RecLastUpdatedDate = DateTime.Now
            };
            return notfication;
        }
        public static EmployeeDDL CreateForEmployeeDDL(this Employee source)
        {
            return new EmployeeDDL
            {
                UserId = source.AspNetUsers.FirstOrDefault()!=null?source.AspNetUsers.FirstOrDefault().Id:"",
                EmployeeId = source.EmployeeId,
                EmployeeNameE = source.EmployeeFirstNameE + " " + source.EmployeeMiddleNameE + " " + source.EmployeeLastNameE,
                EmployeeNameA = source.EmployeeFirstNameA + " " + source.EmployeeMiddleNameA + " " + source.EmployeeLastNameA,
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
            if (notification.NotificationRecipients.Any())
            {
                if (notification.NotificationRecipients.FirstOrDefault().EmployeeId > 0)
                {
                    var employee = notification.NotificationRecipients.FirstOrDefault().Employee;
                    notificationListResponse.EmployeeId = employee.EmployeeId;
                    notificationListResponse.MobileNo = employee.EmployeeMobileNum;
                    notificationListResponse.Email = employee.Email;
                    var employeeFullNameE =
                        employee.EmployeeFirstNameE +
                        " " +
                        employee.EmployeeMiddleNameE +
                        " " +
                        employee.EmployeeLastNameE;
                    var employeeFullNameA =
                        employee.EmployeeFirstNameA +
                        " " +
                        employee.EmployeeMiddleNameA +
                        " " +
                        employee.EmployeeLastNameA;
                    notificationListResponse.EmployeeName = System.Threading.Thread.CurrentThread.CurrentCulture.ToString() == "en" ? employeeFullNameE : employeeFullNameA;
                }
                else if (notification.NotificationRecipients.FirstOrDefault().AspNetUser.Customer!=null)
                {
                    notificationListResponse.MobileNo = notification.NotificationRecipients.FirstOrDefault().AspNetUser.Customer.CustomerMobile;
                    notificationListResponse.Email = notification.NotificationRecipients.FirstOrDefault().AspNetUser.Email;
                }
                if (notificationListResponse.MobileNo == "" || notificationListResponse.Email == "")
                {
                    notificationListResponse.Notified = "No Data";
                }
                else
                {
                    notificationListResponse.Notified = notification.NotificationRecipients.FirstOrDefault().IsRead
                        ? Resources.Notification.Yes
                        : Resources.Notification.No;
                }
            }
            
            switch (notification.CategoryId)
            {
                case 1: notificationListResponse.CategoryName = Resources.Notification.Company; break;
                case 2: notificationListResponse.CategoryName = Resources.Notification.Documents; break;
                case 3: notificationListResponse.CategoryName = Resources.Notification.Employees; break;
                case 4: notificationListResponse.CategoryName = Resources.Notification.Meetings; break;
                case 5: notificationListResponse.CategoryName = Resources.Notification.Other; break;
                default: notificationListResponse.CategoryName = Resources.Notification.Other; break;
            }
            switch (notification.AlertBefore)
            {
                case 30: notificationListResponse.AlertTime = Resources.Notification.BeforeOneMonth; break;
                case 7: notificationListResponse.AlertTime = Resources.Notification.BeforeOneWeek; break;
                case 1: notificationListResponse.AlertTime = Resources.Notification.BeforeOneDay; break;
                default: notificationListResponse.AlertTime = Resources.Notification.Before + " " + notification.AlertBefore + " " + Resources.Notification.Days; break;
            }

            return notificationListResponse;
        }
        public static NotificationListResponse CreateFromServerToClientListWithRecipient(this Notification notification, string userId, long employeeId)
        {
            NotificationListResponse notificationListResponse = new NotificationListResponse();
            notificationListResponse.NotificationId = notification.NotificationId;
            notificationListResponse.NotificationName = System.Threading.Thread.CurrentThread.CurrentCulture.ToString() == "en" ? notification.TitleE : notification.TitleA;
            notificationListResponse.AlertEndTime = notification.AlertDate.ToString("dd/MM/yyyy", new CultureInfo("en"));
            if (notification.NotificationRecipients.Any())
            {
                if (notification.NotificationRecipients.FirstOrDefault().EmployeeId > 0)
                {
                    var employee = notification.NotificationRecipients.FirstOrDefault().Employee;
                    notificationListResponse.EmployeeId = employee.EmployeeId;
                    notificationListResponse.MobileNo = employee.EmployeeMobileNum;
                    notificationListResponse.Email = employee.Email;
                    var employeeFullNameE =
                        employee.EmployeeFirstNameE +
                        " " +
                        employee.EmployeeMiddleNameE +
                        " " +
                        employee.EmployeeLastNameE;
                    var employeeFullNameA =
                        employee.EmployeeFirstNameA +
                        " " +
                        employee.EmployeeMiddleNameA +
                        " " +
                        employee.EmployeeLastNameA;
                    notificationListResponse.EmployeeName = System.Threading.Thread.CurrentThread.CurrentCulture.ToString() == "en" ? employeeFullNameE : employeeFullNameA;
                }
                else if (notification.NotificationRecipients.FirstOrDefault().AspNetUser.Customer != null)
                {
                    notificationListResponse.MobileNo = notification.NotificationRecipients.FirstOrDefault().AspNetUser.Customer.CustomerMobile;
                    notificationListResponse.Email = notification.NotificationRecipients.FirstOrDefault().AspNetUser.Email;
                }
                if (notification.NotificationRecipients.FirstOrDefault(x => x.UserId == userId || x.EmployeeId == employeeId)!=null)
                    notificationListResponse.Notified = notification.NotificationRecipients.FirstOrDefault(x=>x.UserId==userId||x.EmployeeId==employeeId).IsRead ? Resources.Notification.Yes : Resources.Notification.No;
                else
                    notificationListResponse.Notified = Resources.Notification.No;
            }

            switch (notification.CategoryId)
            {
                case 1: notificationListResponse.CategoryName = Resources.Notification.Company; break;
                case 2: notificationListResponse.CategoryName = Resources.Notification.Documents; break;
                case 3: notificationListResponse.CategoryName = Resources.Notification.Employees; break;
                case 4: notificationListResponse.CategoryName = Resources.Notification.Meetings; break;
                case 5: notificationListResponse.CategoryName = Resources.Notification.Other; break;
                default: notificationListResponse.CategoryName = Resources.Notification.Other; break;
            }
            switch (notification.AlertBefore)
            {
                case 30: notificationListResponse.AlertTime = Resources.Notification.BeforeOneMonth; break;
                case 7: notificationListResponse.AlertTime = Resources.Notification.BeforeOneWeek; break;
                case 1: notificationListResponse.AlertTime = Resources.Notification.BeforeOneDay; break;
                default: notificationListResponse.AlertTime = Resources.Notification.Before + " " + notification.AlertBefore + " " + Resources.Notification.Days; break;
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
                recipient.UserId = null;
                recipient.MobileNo = source.MobileNo;
                recipient.Email = source.Email;
            }
            recipient.IsRead = source.ReadStatus;
            if (source.EmployeeId != 0)
            {
                recipient.EmployeeId = source.EmployeeId;
            }
            return recipient;
        }
    }
}
