using System.Collections.Generic;
using EPMS.Models.ResponseModels.EmployeeResponseModel;

namespace EPMS.Models.ResponseModels.NotificationResponseModel
{
    public class NotificationViewModel
    {
        public NotificationViewModel()
        {
            NotificationResponse=new NotificationResponse();
            //string EmployeeNotification =
            //    UserNotifications.FirstOrDefault(str => str == UserNotificationType.EmployeeNotification);
        }
        public IEnumerable<EmployeeDDL> EmployeeDDL { get; set; }
        public NotificationResponse NotificationResponse { get; set; }
        public string UserId { get; set; }

        //public IList<string> UserNotifications { get { return UserNotification.Types; } } 


    }
}
