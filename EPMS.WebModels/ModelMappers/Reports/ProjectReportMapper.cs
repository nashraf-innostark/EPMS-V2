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
                report.ReportCategoryItemTitleE = source.Project.NameE;
                report.ReportCategoryItemTitleA = source.Project.NameA;
            }

            return report;
        }
    }
}