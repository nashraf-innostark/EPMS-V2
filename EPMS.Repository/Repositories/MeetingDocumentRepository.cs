using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using EPMS.Interfaces.Repository;
using EPMS.Models.DomainModels;
using EPMS.Repository.BaseRepository;
using Microsoft.Practices.Unity;

namespace EPMS.Repository.Repositories
{
    public sealed class MeetingDocumentRepository : BaseRepository<MeetingDocument>, IMeetingDocumentRepository
    {
        public MeetingDocumentRepository(IUnityContainer container) : base(container)
        {
        }

        protected override IDbSet<MeetingDocument> DbSet
        {
            get { return db.MeetingDocuments; }
        }

        public IEnumerable<MeetingDocument> LoadDocumentByMeetingId(long meetingId)
        {
            return DbSet.Where(x => x.MeetingId == meetingId);
        }
    }
}
