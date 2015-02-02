using System;
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
                AlertBefore = notification.AlertBefore,
                AlertDateType = notification.AlertDateType,
                AlertDate = notification.AlertDate.ToShortDateString(),
                EmployeeId = notification.EmployeeId,
                MobileNo = notification.MobileNo,
                Email = notification.Email,
                ReadStatus = notification.ReadStatus,

                RecCreatedBy = notification.RecCreatedBy,
                RecCreatedDate = notification.RecCreatedDate,
                RecLastUpdatedBy = notification.RecLastUpdatedBy,
                RecLastUpdatedDate = notification.RecLastUpdatedDate
            };
        }
        public static Notification CreateFromClientToServer(this NotificationResponse notification)
        {
            return new Notification
            {
                NotificationId = notification.NotificationId,
                TitleA = notification.TitleA,
                TitleE = notification.TitleE,
                CategoryId = notification.CategoryId,
                AlertBefore = notification.AlertBefore,
                AlertDateType = notification.AlertDateType,
                AlertDate = Convert.ToDateTime(notification.AlertDate),
                EmployeeId = notification.EmployeeId,
                MobileNo = notification.MobileNo,
                Email = notification.Email,
                ReadStatus = notification.ReadStatus,

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
                EmployeeId = source.EmployeeId,
                EmployeeNameE = source.EmployeeNameE,
                EmployeeNameA = source.EmployeeNameA,
                Email = source.Email,
                MobileNo = source.EmployeeMobileNum
            };
        }
    }
}
