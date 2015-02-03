using System.Collections.Generic;

namespace EPMS.Models.ResponseModels.NotificationResponseModel
{
    public class NotificationListView
    {
        public NotificationSearchRequest NotificationSearchRequest { get; set; }
        /// <summary>
        /// List
        /// </summary>
        public IEnumerable<NotificationResponse> aaData { get; set; }
        /// <summary>
        /// Total Records in DB
        /// </summary>
        public int iTotalRecords;
        public int sLimit;
        /// <summary>
        /// Total Records Filtered
        /// </summary>
        public int iTotalDisplayRecords;
        public string sEcho;
    }
}
