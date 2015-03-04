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
                EmployeeNameE = source.Employee.EmployeeFirstNameE + " " + source.Employee.EmployeeMiddleNameE + " " + source.Employee.EmployeeLastNameE,
                EmployeeNameA = source.Employee.EmployeeFirstNameA + " " + source.Employee.EmployeeMiddleNameA + " " + source.Employee.EmployeeLastNameA,
                Status = source.Status,
            };
        }

        public static DomainModels.MeetingAttendee CreateFromClientToServer(this Models.MeetingAttendee source)
        {
            return new DomainModels.MeetingAttendee
            {
                AttendeeId = source.AttendeeId,
                MeetingId = source.MeetingId,
                EmployeeId = source.EmployeeId,
                Status = source.Status,
            };
        }

    }
}