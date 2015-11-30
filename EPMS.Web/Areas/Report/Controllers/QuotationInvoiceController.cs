using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EPMS.Interfaces.IServices;
using EPMS.Models.RequestModels.Reports;
using EPMS.Web.Controllers;
using EPMS.WebModels.ModelMappers;
using EPMS.WebModels.ModelMappers.PMS;
using EPMS.WebModels.ViewModels.Reports;
using EPMS.WebModels.WebsiteModels;

namespace EPMS.Web.Areas.Report.Controllers
{
    public class QuotationInvoiceController : BaseController
    {
        #region Private

        private readonly IEmployeeService employeeService;
        private readonly IReportService reportService;

        #endregion

        #region Constructor

        public QuotationInvoiceController(IEmployeeService employeeService, IReportService reportService)
        {
            this.employeeService = employeeService;
            this.reportService = reportService;
        }

        #endregion

        #region Public

        #region Create
        public ActionResult Create()
        {
            QuotationInvoiceViewModel viewModel = new QuotationInvoiceViewModel
            {
                Employees = employeeService.GetAll().Select(x => x.CreateFromServerToClient())
            };
            return View(viewModel);
        }

        #endregion

        #region Detail

        public ActionResult Detail(QuotationInvoiceViewModel quotationInvoiceViewModel)
        {
            QuotationInvoiceDetailRequest request = new QuotationInvoiceDetailRequest
            {
                EmployeeId = quotationInvoiceViewModel.EmployeeId,
                ReportId = quotationInvoiceViewModel.ReportId,
                RequesterRole = "Admin",
                RequesterId = Session["UserID"].ToString(),
                StartDate = quotationInvoiceViewModel.StartDate != null ? DateTime.ParseExact(quotationInvoiceViewModel.StartDate, "dd/MM/yyyy", new CultureInfo("en")) : new DateTime(),
                EndDate = quotationInvoiceViewModel.EndDate != null ? DateTime.ParseExact(quotationInvoiceViewModel.EndDate, "dd/MM/yyyy", new CultureInfo("en")) : new DateTime(),
            };

            var refrel = Request.UrlReferrer;
            if (refrel != null && refrel.ToString().Contains("Report/QuotationInvoice/Create"))
                request.IsCreate = true;
            var response = reportService.SaveAndGetQuotationInvoiceReport(request);
            quotationInvoiceViewModel.Quotations = response.Quotations.Select(x => x.CreateFromServerToClientLv());
            quotationInvoiceViewModel.EmployeeNameE = response.EmployeeNameE;
            quotationInvoiceViewModel.EmployeeNameA = response.EmployeeNameA;
            quotationInvoiceViewModel.InvoicesCount = response.InvoicesCount;
            quotationInvoiceViewModel.QuotationsCount = response.QuotationsCount;
            quotationInvoiceViewModel.StartDate = response.StartDate;
            quotationInvoiceViewModel.EndDate = response.EndDate;

            SetGraphData(quotationInvoiceViewModel);
            ViewBag.QueryString = "?ReportId=" + quotationInvoiceViewModel.ReportId;


            return View(quotationInvoiceViewModel);
        }

        #endregion

        #region AllDetail
        public ActionResult AllDetail(QuotationInvoiceViewModel quotationInvoiceViewModel)
        {
            QuotationInvoiceDetailRequest request = new QuotationInvoiceDetailRequest
            {
                EmployeeId = quotationInvoiceViewModel.EmployeeId,
                ReportId = quotationInvoiceViewModel.ReportId,
                RequesterRole = "Admin",
                RequesterId = Session["UserID"].ToString(),
                StartDate = quotationInvoiceViewModel.StartDate != null ? DateTime.ParseExact(quotationInvoiceViewModel.StartDate, "dd/MM/yyyy", new CultureInfo("en")) : new DateTime(),
                EndDate = quotationInvoiceViewModel.EndDate != null ? DateTime.ParseExact(quotationInvoiceViewModel.EndDate, "dd/MM/yyyy", new CultureInfo("en")) : new DateTime(),
            };

            var refrel = Request.UrlReferrer;
            if (refrel != null && refrel.ToString().Contains("Report/QuotationInvoice/Create"))
                request.IsCreate = true;
            var response = reportService.SaveAndGetQuotationInvoiceReport(request);
            quotationInvoiceViewModel.InvoicesCount = response.InvoicesCount;
            quotationInvoiceViewModel.QuotationsCount = response.QuotationsCount;

            return View(quotationInvoiceViewModel);
        }

        #endregion


        #region Graph

        private QuotationInvoiceViewModel SetGraphData(QuotationInvoiceViewModel detailVeiwModel)
        {
            var firstQuotation = detailVeiwModel.Quotations.OrderByDescending(x=>x.RecCreatedDt).FirstOrDefault();
            var fTotalCost = firstQuotation != null ? firstQuotation.QuotationItemDetails.Sum(x => x.TotalPrice) : 0;
            var lastQuotation = detailVeiwModel.Quotations.OrderBy(x => x.RecCreatedDt).FirstOrDefault();
            var lTotalCost = lastQuotation != null ? lastQuotation.QuotationItemDetails.Sum(x => x.TotalPrice) : 0;

            if (firstQuotation != null)
            {
                detailVeiwModel.GrpahStartTimeStamp = GetJavascriptTimestamp((DateTime) firstQuotation.RecCreatedDt);
                detailVeiwModel.GrpahEndTimeStamp = GetJavascriptTimestamp((DateTime) lastQuotation.RecCreatedDt);

                detailVeiwModel.GraphItems.Add(new GraphItem
                {
                    ItemLabel = "Quotation",
                    ItemValue = new List<GraphLabel>
                    {
                        new GraphLabel
                        {
                            label = "Quotation",
                            data = new List<GraphLabelData>
                            {
                                new GraphLabelData
                                {
                                    dataValue = new List<GraphLabelDataValues>
                                    {
                                        new GraphLabelDataValues
                                        {
                                            TimeStamp = detailVeiwModel.GrpahStartTimeStamp,
                                            Value = fTotalCost
                                        },
                                        new GraphLabelDataValues
                                        {
                                            TimeStamp = detailVeiwModel.GrpahEndTimeStamp,
                                            Value = lTotalCost
                                        }
                                    }
                                }
                            }
                        }
                    }
                });
            }
            return detailVeiwModel;
        }

        private string SetGraphImage(long reportId)
        {
            string curFile = Server.MapPath(ConfigurationManager.AppSettings["ReportImage"]) + "report_" + reportId + ".png";
            if (System.IO.File.Exists(curFile))
            {
                return "../.." + ConfigurationManager.AppSettings["ReportImage"] + "report_" + reportId + ".png";
            }
            return null;
        }

        private static long GetJavascriptTimestamp(DateTime input)
        {
            TimeSpan span = new TimeSpan(DateTime.Parse("1/1/1970").Ticks);
            DateTime time = input.Subtract(span);
            return (time.Ticks / 10000);
        }

        #endregion

        #endregion
    }
}