using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using EPMS.WebModels.WebsiteModels;

namespace EPMS.WebModels.ViewModels.Reports
{
    public class QuotationInvoiceViewModel
    {
        public QuotationInvoiceViewModel()
        {
            Employees = new List<WebsiteModels.Employee>();
            Quotations = new List<WebsiteModels.Quotation>();
            GraphItems = new List<GraphItem>();
            Report = new Report();
        }

        [Required(ErrorMessageResourceType = typeof(Resources.Reports.CustomerReport), ErrorMessageResourceName = "StartDateValidation")]
        public string StartDate { get; set; }
        [Required(ErrorMessageResourceType = typeof(Resources.Reports.CustomerReport), ErrorMessageResourceName = "EndDateValidation")]
        public string EndDate { get; set; }

        public IEnumerable<WebsiteModels.Employee> Employees { get; set; }
        public IEnumerable<WebsiteModels.Quotation> Quotations { get; set; }
        public IEnumerable<WebsiteModels.Invoice> Invoices { get; set; }
        public long EmployeeId { get; set; }
        public int QuotationsCount { get; set; }
        public int InvoicesCount { get; set; }
        public long ReportId { get; set; }
        public string EmployeeNameE { get; set; }
        public string EmployeeNameA { get; set; }


        //Grpah related Data
        public long GrpahStartTimeStamp { get; set; }
        public long GrpahEndTimeStamp { get; set; }
        public List<GraphItem> GraphItems { get; set; }
        public string ImageSrc { get; set; }
        public GraphLabelDataValues[] QuotationDataSet { get; set; }
        public GraphLabelDataValues[] InvoiceDataSet { get; set; }
        public Report Report { get; set; }
        public bool IsPdf { get; set; }
    }
}
