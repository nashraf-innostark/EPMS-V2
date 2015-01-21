using System.Collections.Generic;
using EPMS.Models.DomainModels;

namespace EPMS.Models.ResponseModels
{
    /// <summary>
    /// Meeting Search response
    /// </summary>
    public class MeetingResponse
    {
        public MeetingResponse()
        {
            Meetings = new List<Meeting>();
        }
        public IEnumerable<Meeting> Meetings { get; set; }
        public int TotalCount { get; set; }
        public int TotalRecords { get; set; }
        public int TotalDisplayRecords { get; set; }
    }
}
