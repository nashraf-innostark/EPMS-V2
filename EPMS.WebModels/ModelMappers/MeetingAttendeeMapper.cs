namespace EPMS.WebModels.ModelMappers
{
    public static class MeetingAttendeeMapper
    {
        public static WebsiteModels.MeetingAttendee CreateFromServertoClient(this Models.DomainModels.MeetingAttendee source)
        {
            return new WebsiteModels.MeetingAttendee
            {
                MeetingId = source.MeetingId,
                EmployeeId = source.EmployeeId,
                EmployeeNameE = source.Employee.EmployeeFirstNameE + " " + source.Employee.EmployeeMiddleNameE + " " + source.Employee.EmployeeLastNameE,
                EmployeeNameA = source.Employee.EmployeeFirstNameA + " " + source.Employee.EmployeeMiddleNameA + " " + source.Employee.EmployeeLastNameA,
                Status = source.Status,
            };
        }

        public static Models.DomainModels.MeetingAttendee CreateFromClientToServer(this WebsiteModels.MeetingAttendee source)
        {
            return new Models.DomainModels.MeetingAttendee
            {
                AttendeeId = source.AttendeeId,
                MeetingId = source.MeetingId,
                EmployeeId = source.EmployeeId,
                Status = source.Status,
            };
        }

    }
}