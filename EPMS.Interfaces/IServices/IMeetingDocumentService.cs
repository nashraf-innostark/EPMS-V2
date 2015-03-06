﻿using System.Collections.Generic;
using EPMS.Models.DomainModels;

namespace EPMS.Interfaces.IServices
{
    public interface IMeetingDocumentService
    {
        /// <summary>
        /// Add Meeting Documents
        /// </summary>
        bool AddMeetingDocument(MeetingDocument document);
        /// <summary>
        /// Delete Meeting Document
        /// </summary>
        bool Delete(long documentId);
        /// <summary>
        /// Find Document by Meeting Id
        /// </summary>
        IEnumerable<MeetingDocument> FindDocumentByMeetingId(long meetingId);
        MeetingDocument FindMeetingDocumentById(long documentId);

    }
}