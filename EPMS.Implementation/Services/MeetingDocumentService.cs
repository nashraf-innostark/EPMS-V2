using System.Collections.Generic;
using EPMS.Interfaces.IServices;
using EPMS.Interfaces.Repository;
using EPMS.Models.DomainModels;

namespace EPMS.Implementation.Services
{
    public class MeetingDocumentService : IMeetingDocumentService
    {
        private readonly IMeetingDocumentRepository meetingDocumentRepository;

        #region Constructor

        public MeetingDocumentService(IMeetingDocumentRepository meetingDocumentRepository)
        {
            this.meetingDocumentRepository = meetingDocumentRepository;
        }

        #endregion
        public bool AddMeetingDocument(MeetingDocument document)
        {
            meetingDocumentRepository.Add(document);
            meetingDocumentRepository.SaveChanges();
            return true;
        }

        public IEnumerable<MeetingDocument> FindDocumentByMeetingId(long meetingId)
        {
            return meetingDocumentRepository.LoadDocumentByMeetingId(meetingId);
        }
    }
}
