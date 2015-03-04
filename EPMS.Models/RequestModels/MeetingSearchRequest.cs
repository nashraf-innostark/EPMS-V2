using System;
using EPMS.Models.Common;

namespace EPMS.Models.RequestModels
{
    public class MeetingSearchRequest : GetPagedListRequest
    {
        public string SearchText { get; set; }
        public long MeetingId { get; set; }
        public string Topic { get; set; }
        public string ProjectRelated { get; set; }
        public DateTime Date { get; set; }
        public string sEcho { get; set; }
        public string sSearch { get; set; }
        public MeetingByColumn MeetingRequestByColumn
        {
            get
            {
                return (MeetingByColumn)iSortCol_0;
            }
            set
            {
                iSortCol_0 = (short)value;
            }
        }
    }
}
