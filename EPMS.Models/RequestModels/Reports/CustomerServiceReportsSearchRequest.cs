using EPMS.Models.Common;

namespace EPMS.Models.RequestModels.Reports
{
    public class CustomerServiceReportsSearchRequest : GetPagedListRequest
    {
        public long ReportId { get; set; }
        public CustomerReportByColumn ReportByColumn
        {
            get
            {
                return (CustomerReportByColumn)iSortCol_0;
            }
            set
            {
                iSortCol_0 = (short)value;
            }
        }
    }
}
