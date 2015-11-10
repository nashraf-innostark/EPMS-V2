namespace EPMS.WebModels.ModelMappers.Reports
{
    public static class ProjectReportMapper
    {
        public static WebsiteModels.Report CreateProjectReportFromServerToClient(this Models.DomainModels.Report source)
        {
            var report = new WebsiteModels.Report
            {
                ReportId = source.ReportId,
                ReportCategoryId = source.ReportCategoryId,
                ReportFromDate = source.ReportFromDate,
                ReportToDate = source.ReportToDate,
                ReportCreatedBy = source.ReportCreatedBy,
                ReportCreatedDate = source.ReportCreatedDate,

                ReportCreatedDateString = source.ReportCreatedDate.ToShortDateString(),
                ReportFromDateString = source.ReportFromDate.ToShortDateString(),
                ReportToDateString = source.ReportToDate.ToShortDateString(),
            };
            if (source.ProjectId != null)
            {
                report.ProjectId = source.ProjectId;
                report.ReportCategoryItemTitle = System.Threading.Thread.CurrentThread.CurrentCulture.ToString() == "en"
                    ? source.Project.NameE
                    : source.Project.NameA;
            }
            if (source.AspNetUser.Employee != null)
            {
                report.ReportCreatedByName = System.Threading.Thread.CurrentThread.CurrentCulture.ToString() == "en"
                    ? source.AspNetUser.Employee.EmployeeFirstNameE + " " + source.AspNetUser.Employee.EmployeeMiddleNameE + " " + source.AspNetUser.Employee.EmployeeLastNameE
                    : source.AspNetUser.Employee.EmployeeFirstNameA + " " + source.AspNetUser.Employee.EmployeeMiddleNameA + " " + source.AspNetUser.Employee.EmployeeLastNameA;
            }
            return report;
        }
    }
}