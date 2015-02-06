using System.Collections.Generic;

namespace EPMS.Models.ResponseModels.NotificationResponseModel
{
    /// <summary>
    /// User Notification Types to get the list of all types of user notifications
    /// </summary>
    public static class UserNotification
    {
        /// <summary>
        /// Types of Notification
        /// </summary>
        public static IList<string> Types
        {
            get
            {
                return new List<string>
                {
                    UserNotificationType.EmployeeNotification,
                    UserNotificationType.ProjectNotification,
                };
            }
        }
    }
}
