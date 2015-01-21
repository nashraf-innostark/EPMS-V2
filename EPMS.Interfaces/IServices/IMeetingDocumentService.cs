using System.Collections.Generic;
using EPMS.Models.DomainModels;

namespace EPMS.Web
{
    public interface IMeetingDocumentService
    {
        /// <summary>
        /// Add Meeting Documents
        /// </summary>
        bool AddMeetingDocument(MeetingDocument document);
        /// <summary>
        /// Find Document by Meeting Id
        /// </summary>
        IEnumerable<MeetingDocument> FindDocumentByMeetingId(long meetingId);

    }
}