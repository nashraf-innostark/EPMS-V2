using System;
using System.Globalization;
using System.Linq;
using EPMS.Models.DomainModels;
using EPMS.WebModels.WebsiteModels;
using Report = EPMS.Models.DomainModels.Report;

namespace EPMS.WebModels.ModelMappers.Reports
{
    public static class CustomerReportMapper
    {
        public static WebsiteModels.Report CreateReportFromServerToClient(this Report source)
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
            if (source.EmployeeId != null)
            {
                report.EmployeeId = source.EmployeeId;
                report.ReportCategoryItemTitle = System.Threading.Thread.CurrentThread.CurrentCulture.ToString() == "en"
                    ? source.Employee.EmployeeFirstNameE
                    : source.Employee.EmployeeFirstNameA;
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
            report.ReportQuotationInvoices = source.ReportQuotationInvoices;
            return report;
        }

        public static QuotationOrderReport CreateReportFromServerToClient(this ReportQuotationOrder source)
        {
            return new QuotationOrderReport
            {
                CustomerNameA = source.CustomerNameA,
                CustomerNameE = source.CustomerNameE,
                NoOfOrders = source.NoOfOrders,
                NoOfRFQ = source.NoOfRFQ,
                ReportFromDateString = System.Threading.Thread.CurrentThread.CurrentCulture.ToString() == "en"
                    ? Convert.ToDateTime(source.Report.ReportFromDate).ToString("dd/MM/yyyy", new CultureInfo("en"))
                    : DateTime.ParseExact(source.Report.ReportFromDate.ToShortDateString(), "dd/MM/yyyy", new CultureInfo("ar")).ToString(),
                ReportToDateString = System.Threading.Thread.CurrentThread.CurrentCulture.ToString() == "en"
                ? Convert.ToDateTime(source.Report.ReportToDate).ToString("dd/MM/yyyy", new CultureInfo("en"))
                : DateTime.ParseExact(source.Report.ReportToDate.ToShortDateString(), "dd/MM/yyyy", new CultureInfo("ar")).ToString(),
                ReportQuotationOrderItems = source.ReportQuotationOrderItems
            };
        }
    }
}
