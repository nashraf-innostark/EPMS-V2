using System.Collections.Generic;
using EPMS.Models.DomainModels;

namespace EPMS.Interfaces.Repository
{
    /// <summary>
    /// Meeting Document Repository
    /// </summary>
    public interface IMeetingDocumentRepository : IBaseRepository<MeetingDocument, long>
    {
        /// <summary>
        /// Load Documents by Meeting Id
        /// </summary>
        IEnumerable<MeetingDocument> LoadDocumentByMeetingId(long meetingId);
    }
}
