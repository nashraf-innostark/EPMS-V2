using System.Collections.Generic;
using EPMS.Models.RequestModels.NotificationRequestModels;

namespace EPMS.Models.ResponseModels.NotificationResponseModel
{
    public class NotificationListView
    {
        public NotificationListViewRequest NotificationSearchRequest { get; set; }
        /// <summary>
        /// List
        /// </summary>
        public IEnumerable<NotificationListResponse> aaData { get; set; }
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
