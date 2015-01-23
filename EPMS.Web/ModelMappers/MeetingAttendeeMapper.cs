using DomainModels = EPMS.Models.DomainModels;

namespace EPMS.Web.ModelMappers
{
    public static class MeetingAttendeeMapper
    {
        public static Models.MeetingAttendee CreateFromServertoClient(this DomainModels.MeetingAttendee source)
        {
            return new Models.MeetingAttendee
            {
                MeetingId = source.MeetingId,
                EmployeeId = source.EmployeeId,
                EmployeeNameE = source.Employee.EmployeeNameE,
                EmployeeNameA = source.Employee.EmployeeNameA,
                AttendeeName = source.AttendeeName,
                AttendeeEmail = source.AttendeeEmail,
                Status = source.Status,
                RecCreatedBy = source.RecCreatedBy,
                RecCreatedDt = source.RecCreatedDt,
                RecLastUpdatedBy = source.RecLastUpdatedBy,
                RecLastUpdatedDt = source.RecLastUpdatedDt
            };
        }

        public static DomainModels.MeetingAttendee CreateFromClientToServer(this Models.MeetingAttendee source)
        {
            return new DomainModels.MeetingAttendee
            {
                AttendeeId = source.AttendeeId,
                MeetingId = source.MeetingId,
                EmployeeId = source.EmployeeId,
                AttendeeName = source.AttendeeName,
                AttendeeEmail = source.AttendeeEmail,
                Status = source.Status,
                RecCreatedBy = source.RecCreatedBy,
                RecCreatedDt = source.RecCreatedDt,
                RecLastUpdatedBy = source.RecLastUpdatedBy,
                RecLastUpdatedDt = source.RecLastUpdatedDt
            };
        }

    }
}