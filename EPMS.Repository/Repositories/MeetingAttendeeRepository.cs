using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using EPMS.Interfaces.Repository;
using EPMS.Models.DomainModels;
using EPMS.Repository.BaseRepository;
using Microsoft.Practices.Unity;

namespace EPMS.Repository.Repositories
{
    public sealed class MeetingAttendeeRepository : BaseRepository<MeetingAttendee>, IMeetingAttendeeRepository
    {
        #region Constructor
        public MeetingAttendeeRepository(IUnityContainer container) : base(container)
        {
        }

        protected override IDbSet<MeetingAttendee> DbSet
        {
            get { return db.MeetingAttendees; }
        }

        #endregion

        public IEnumerable<MeetingAttendee> GetAttendeesByMeetingId(long meetingId)
        {
            return DbSet.Where(x => x.MeetingId == meetingId);
        }
        public MeetingAttendee GetAttendeeByEmployeeId(long employeeId)
        {
            return DbSet.FirstOrDefault(x => x.EmployeeId == employeeId);
        }

        public MeetingAttendee GetAttendeeByEmployeeAndMeetingId(long employeeId, long meetingId)
        {
            return DbSet.FirstOrDefault(x => x.EmployeeId == employeeId && x.MeetingId == meetingId);
        }
    }
}
