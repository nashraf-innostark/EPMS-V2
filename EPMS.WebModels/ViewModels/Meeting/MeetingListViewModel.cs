using System.Collections.Generic;
using EPMS.Models.RequestModels;

namespace EPMS.WebModels.ViewModels.Meeting
{
    /// <summary>
    /// Meeting List View Model
    /// </summary>
    public class MeetingListViewModel
    {
        /// <summary>
        /// List of Meetings
        /// </summary>
        public IEnumerable<WebsiteModels.MeetingModel> aaData { get; set; }
        public MeetingSearchRequest SearchRequest { get; set; }
        /// <summary>
        /// Total Records in DB
        /// </summary>
        public int iTotalRecords;

        /// <summary>
        /// Total Records Filtered
        /// </summary>
        public int iTotalDisplayRecords;

        public string sEcho;
    }
}