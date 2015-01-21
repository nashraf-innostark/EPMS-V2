using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EPMS.Models.DomainModels;
using EPMS.Models.RequestModels;
using EPMS.Models.ResponseModels;

namespace EPMS.Interfaces.Repository
{
    /// <summary>
    /// Meeting Repository
    /// </summary>
    public interface IMeetingRepository : IBaseRepository<Meeting, long>
    {
        /// <summary>
        /// Get All Meetings
        /// </summary>
        MeetingResponse GetAllMeetings(MeetingSearchRequest meetingSearchRequest);
    }
}
