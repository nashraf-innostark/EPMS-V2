using System.Collections.Generic;

namespace EPMS.Models.ResponseModels.NotificationResponseModel
{
    public class NotificationSearchRequest
    {
        public NotificationSearchRequest()
        {
            Notifications = new List<NotificationResponse>();
        }
        public IEnumerable<NotificationResponse> Notifications { get; set; }

        /// <summary>
        /// Total Count
        /// </summary>
        public int TotalCount { get; set; }
    }
}
