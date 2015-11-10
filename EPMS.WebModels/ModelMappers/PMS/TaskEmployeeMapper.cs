using EPMS.Models.DomainModels;

namespace EPMS.WebModels.ModelMappers.PMS
{
    public static class TaskEmployeeMapper
    {
        public static WebsiteModels.TaskEmployee CreateFromServerToClient(this TaskEmployee source)
        {
            var retval = new WebsiteModels.TaskEmployee
            {
                TaskEmployeeId = source.TaskEmployeeId,
                TaskId = source.TaskId,
                EmployeeId = source.EmployeeId,
                RecCreatedBy = source.RecCreatedBy,
                RecCreatedDt = source.RecCreatedDt,
                RecLastUpdatedBy = source.RecLastUpdatedBy,
                RecLastUpdatedDt = source.RecLastUpdatedDt,
                ProjectTask = source.ProjectTask.CreateFromServerToClientForEmployee()
            };
            if (source.Employee != null)
            {
                retval.EmployeeNameEn = source.Employee.EmployeeFirstNameE + " " + source.Employee.EmployeeMiddleNameE +
                                        " " + source.Employee.EmployeeLastNameE;
                retval.EmployeeNameAr = source.Employee.EmployeeFirstNameA + " " + source.Employee.EmployeeMiddleNameA +
                                        " " + source.Employee.EmployeeLastNameA;
            }
            return retval;
        }
    }
}