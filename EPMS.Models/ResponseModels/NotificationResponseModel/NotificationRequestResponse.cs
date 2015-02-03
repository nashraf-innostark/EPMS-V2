using System.Collections.Generic;
using EPMS.Models.DomainModels;

namespace EPMS.Models.ResponseModels.NotificationResponseModel
{
    public class NotificationRequestResponse
    {
        public NotificationRequestResponse()
        {
            Notifications = new List<Notification>();
        }
        public IEnumerable<Notification> Notifications { get; set; }

        /// <summary>
        /// Total Count
        /// </summary>
        public int TotalCount { get; set; }
    }
}
