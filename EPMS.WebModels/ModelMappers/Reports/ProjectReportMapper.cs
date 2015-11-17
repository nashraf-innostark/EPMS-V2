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
                //ReportFromDateString = Convert.ToDateTime(source.ReportFromDate).ToString("dd/MM/yyyy", new CultureInfo("en")) + "-" + DateTime.ParseExact(source.ReportFromDate.ToShortDateString(), "dd/MM/yyyy", new CultureInfo("ar")).ToShortDateString(),
            };
            if (source.ProjectId != null)
            {
                report.ProjectId = source.ProjectId;
                report.ReportCategoryItemTitle = System.Threading.Thread.CurrentThread.CurrentCulture.ToString() == "en"
                    ? source.Project.NameE
                    : source.Project.NameA;
            }
            if (source.WarehouseId != null)
            {
                report.WarehouseId = source.WarehouseId;
                report.ReportCategoryItemTitle = source.Warehouse.WarehouseNumber;
            }
            else
            {
                report.ReportCategoryItemTitle = Resources.Reports.Reports.All;
            }
            if (source.AspNetUser.Employee != null)
            {
                report.ReportCreatedByName = System.Threading.Thread.CurrentThread.CurrentCulture.ToString() == "en"
                    ? source.AspNetUser.Employee.EmployeeFirstNameE + " " + source.AspNetUser.Employee.EmployeeMiddleNameE + " " + source.AspNetUser.Employee.EmployeeLastNameE
                    : source.AspNetUser.Employee.EmployeeFirstNameA + " " + source.AspNetUser.Employee.EmployeeMiddleNameA + " " + source.AspNetUser.Employee.EmployeeLastNameA;
            } 
            else
            {
                report.ReportCreatedByName = "Admin";
            }
            return report;
        }
        public static WebsiteModels.Report CreateTaskReportFromServerToClient(this Models.DomainModels.Report source)
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
                //ReportFromDateString = Convert.ToDateTime(source.ReportFromDate).ToString("dd/MM/yyyy", new CultureInfo("en")) + "-" + DateTime.ParseExact(source.ReportFromDate.ToShortDateString(), "dd/MM/yyyy", new CultureInfo("ar")).ToShortDateString(),
            };
            if (source.ProjectId != null & source.TaskId != null)
            {
                report.ProjectId = source.ProjectId;
                report.TaskId = source.TaskId;
                report.ReportCategoryItemTitle = System.Threading.Thread.CurrentThread.CurrentCulture.ToString() == "en"
                    ? source.Project.NameE + " - " +  source.ProjectTask.TaskNameE
                    : source.Project.NameA + " - " + source.ProjectTask.TaskNameA;
            }
            else if (source.ProjectId != null && source.TaskId == null)
            {
                report.ProjectId = source.ProjectId;
                report.ReportCategoryItemTitle = System.Threading.Thread.CurrentThread.CurrentCulture.ToString() == "en"
                    ? source.Project.NameE
                    : source.Project.NameA;
            }
            else
            {
                report.ReportCategoryItemTitle = Resources.Reports.Reports.All;
            }
            if (source.AspNetUser.Employee != null)
            {
                report.ReportCreatedByName = System.Threading.Thread.CurrentThread.CurrentCulture.ToString() == "en"
                    ? source.AspNetUser.Employee.EmployeeFirstNameE + " " + source.AspNetUser.Employee.EmployeeMiddleNameE + " " + source.AspNetUser.Employee.EmployeeLastNameE
                    : source.AspNetUser.Employee.EmployeeFirstNameA + " " + source.AspNetUser.Employee.EmployeeMiddleNameA + " " + source.AspNetUser.Employee.EmployeeLastNameA;
            }
            else
            {
                report.ReportCreatedByName = "Admin";
            }
            return report;
        }
    }
}