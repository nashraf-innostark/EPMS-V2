using System.Collections.Generic;

namespace EPMS.WebModels.ViewModels.Reports
{
    public class QuotationInvoiceDetailViewModel
    {
        public long ReportId { get; set; }
        public string ImageSrc { get; set; }
        public IList<WebsiteModels.Quotation> Quotations { get; set; }

        //Grpah related Data
        public long GrpahStartTimeStamp { get; set; }
        public long GrpahEndTimeStamp { get; set; }
        public List<GraphItem> GraphItems { get; set; } 
    }

    public class QuotationOrderDetailViewModel
    {
        public QuotationOrderDetailViewModel()
        {
            GraphItems=new List<GraphItem>();
        }
        public long ReportId { get; set; }
        public IList<WebsiteModels.QuotationOrderReport> QuotationOrderReports { get; set; }

        //Graph related Data
        public string GraphStartTimeStamp { get; set; }
        public string GraphEndTimeStamp { get; set; }
        public List<GraphItem> GraphItems { get; set; }
        public GraphLabelDataValues[] RFQsDataSet { get; set; }
        public GraphLabelDataValues[] OrdersDataSet { get; set; }
    }
}
