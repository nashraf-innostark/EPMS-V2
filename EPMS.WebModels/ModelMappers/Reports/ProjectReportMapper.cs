namespace EPMS.WebModels.ModelMappers.Reports
{
    public static class ProjectReportMapper
    {
        public static WebsiteModels.Report CreateFromServerToClient(this Models.DomainModels.Report source)
        {
            return new WebsiteModels.Report
            {
                ReportId = source.ReportId,
                ReportCategoryId = source.ReportCategoryId,
                ReportCategoryItemId = source.ReportCategoryItemId,
                ReportFromDate = source.ReportFromDate,
                ReportToDate = source.ReportToDate,
                ReportCreatedBy = source.ReportCreatedBy,
                ReportCreatedDate = source.ReportCreatedDate
            };
        }
    }
}