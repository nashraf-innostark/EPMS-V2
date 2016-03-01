using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using EPMS.Interfaces.Repository;
using EPMS.Models.Common;
using EPMS.Models.DomainModels;
using EPMS.Models.RequestModels;
using EPMS.Models.ResponseModels;
using EPMS.Repository.BaseRepository;
using Microsoft.Practices.Unity;

namespace EPMS.Repository.Repositories
{
    class MeetingRepository : BaseRepository<Meeting>, IMeetingRepository
    {
        #region Constructor
        public MeetingRepository(IUnityContainer container) : base(container)
        {
        }

        protected override IDbSet<Meeting> DbSet
        {
            get { return db.Meetings; }
        }

        #endregion

        #region Private

        private readonly Dictionary<MeetingByColumn, Func<Meeting, object>> meetingClause =

            new Dictionary<MeetingByColumn, Func<Meeting, object>>
                    {
                        { MeetingByColumn.Topic,  c => c.TopicName},
                        { MeetingByColumn.TopicAr,  c => c.TopicNameAr},
                        { MeetingByColumn.RelatedProject,  c => c.RelatedProject},
                        { MeetingByColumn.Date,  c => c.Date},
                    };
        #endregion

        public MeetingResponse GetAllMeetings(MeetingSearchRequest meetingSearchRequest)
        {
            int fromRow = meetingSearchRequest.iDisplayStart;
            int toRow = meetingSearchRequest.iDisplayLength;

            Expression<Func<Meeting, bool>> query =
                s =>
                    ((string.IsNullOrEmpty(meetingSearchRequest.SearchString)) ||
                      (s.TopicName.Contains(meetingSearchRequest.SearchString)) ||
                      (s.TopicNameAr.Contains(meetingSearchRequest.SearchString)) ||
                      (s.RelatedProject.Contains(meetingSearchRequest.SearchString)) ||
                      (s.Date.ToString().Contains(meetingSearchRequest.SearchString)));
            IEnumerable<Meeting> meetings = meetingSearchRequest.sSortDir_0=="asc" ?
                DbSet
                .Where(query).OrderBy(meetingClause[meetingSearchRequest.MeetingRequestByColumn]).Skip(fromRow).Take(toRow).ToList()
                :
                DbSet
                                           .Where(query).OrderByDescending(meetingClause[meetingSearchRequest.MeetingRequestByColumn]).Skip(fromRow).Take(toRow).ToList();
            return new MeetingResponse { Meetings = meetings, TotalCount = DbSet.Count(query) };
        }

        public IEnumerable<Meeting> GetMeetingsForDashboard(string requester)
        {
            if (requester == "Admin")
            {
                return DbSet.OrderByDescending(x => x.Date).Take(4);
            }
            long employeeId = Convert.ToInt64(requester);
            return DbSet.Where(x => x.MeetingAttendees.Any(y => y.EmployeeId == employeeId)).OrderByDescending(x=>x.Date).Take(5);
        }
    }
}
