namespace EPMS.WebModels.ModelMappers.Reports
{
    public static class ProjectReportMapper
    {
        public static WebsiteModels.Report CreateFromServerToClient(this Models.DomainModels.Report source)
        {
            var report = new WebsiteModels.Report
            {
                ReportId = source.ReportId,
                ReportCategoryId = source.ReportCategoryId,
                ReportFromDate = source.ReportFromDate,
                ReportToDate = source.ReportToDate,
                ReportCreatedById = source.ReportCreatedBy,
                ReportCreatedDate = source.ReportCreatedDate
            };
            if (source.ProjectId != null)
            {
                report.ReportCategoryItemId = (long)source.ProjectId;
                report.ReportCategoryItemTitle = System.Threading.Thread.CurrentThread.CurrentCulture.ToString() == "en"
                    ? source.Project.NameE
                    : source.Project.NameA;
            }

            return report;
        }
    }
}