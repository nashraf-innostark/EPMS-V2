using System;
using EPMS.Models.DomainModels;
using EPMS.Models.ResponseModels.NotificationResponseModel;

namespace EPMS.Models.ModelMapers.NotificationMapper
{
    public static class NotificationMapper
    {
        public static NotificationResponse CreateFromServerToClient(this Notification notification)
        {
            return new NotificationResponse
            {
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
    }
}
