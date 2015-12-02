using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;
using System.Web.WebPages.Scope;
using EPMS.Interfaces.IServices;
using EPMS.Models.RequestModels.Reports;
using EPMS.Web.Controllers;
using EPMS.WebModels.ModelMappers;
using EPMS.WebModels.ModelMappers.Reports;
using EPMS.WebModels.ViewModels.Reports;
using Rotativa;

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

        [HttpPost]
        public ActionResult Create(QuotationInvoiceViewModel createViewModel)
        {
            var request = new QuotationInvoiceDetailRequest
            {
                EmployeeId = createViewModel.EmployeeId,
                RequesterRole = Session["RoleName"].ToString(),
                RequesterId = Session["UserID"].ToString(),
                StartDate = Convert.ToDateTime(createViewModel.StartDate),
                EndDate = Convert.ToDateTime(createViewModel.EndDate),
            };
            var reportId = reportService.SaveQIReport(request);
            return RedirectToAction(createViewModel.EmployeeId > 0 ? "Detail" : "AllDetail", new {ReportId = reportId});
        }

        #endregion

        #region Detail

        public ActionResult Detail(long? ReportId)
        {
            if (ReportId == null || ReportId <= 0) return View("Create");

            var response = reportService.GetQIReport((long) ReportId);
            QuotationInvoiceDetailViewModel viewModel = new QuotationInvoiceDetailViewModel();

            viewModel.ReportQuotationInvoices = response.Select(x => x.CreateReportFromServerToClient()).ToList();

            SetGraphData(viewModel);

            viewModel.ReportId = (long) ReportId;

            return View(viewModel);
        }

        #endregion

        #region AllDetail

        public ActionResult AllDetail(long? ReportId)
        {
            if (ReportId == null || ReportId <= 0) return View("Create");

            var response = reportService.GetQIReport((long)ReportId);

            QuotationInvoiceDetailViewModel detailViewModel = new QuotationInvoiceDetailViewModel();
            detailViewModel.ReportQuotationInvoices = response.Select(x => x.CreateReportFromServerToClient()).ToList();

            detailViewModel.ReportId = (long)ReportId;

            return View(detailViewModel);
        }

        #endregion


        #region Graph

        private static void SetGraphData(QuotationInvoiceDetailViewModel detailVeiwModel)
        {
            if (
                detailVeiwModel.ReportQuotationInvoices.SelectMany(
                    x => x.ReportQuotationInvoiceItems.Where(y => y.IsQuotationItem)).Any())
            {
                var quotationGroups =
                    detailVeiwModel.ReportQuotationInvoices.SelectMany(
                        x => x.ReportQuotationInvoiceItems.Where(y => y.IsQuotationItem)).ToArray();


                var firstQuotation = quotationGroups.OrderBy(x => x.ReportQuotationInvoiceId).FirstOrDefault();
                var lastQuotation = quotationGroups.OrderByDescending(x => x.ReportQuotationInvoiceId).FirstOrDefault();


                if (firstQuotation != null)
                {
                    detailVeiwModel.GraphStartTimeStamp = firstQuotation.MonthTimeStamp;
                    detailVeiwModel.GraphEndTimeStamp = lastQuotation.MonthTimeStamp;

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
                                        dataValue = new List<GraphLabelDataValues>()

                                    }
                                }
                            }
                        }
                    });
                    for (int i = 0; i < quotationGroups.Count(); i++)
                    {
                        detailVeiwModel.GraphItems[0].ItemValue[0].data[0].dataValue.Add(new GraphLabelDataValues
                        {
                            TimeStamp = Convert.ToInt64(quotationGroups[i].MonthTimeStamp),
                            Value = Convert.ToDecimal(quotationGroups[i].TotalPrice)
                        });
                    }
                    if (quotationGroups.Count() == 1)
                    {
                        //Ending Points on graph
                        detailVeiwModel.GraphItems[0].ItemValue[0].data[0].dataValue.Add(new GraphLabelDataValues
                        {
                            TimeStamp = Convert.ToInt64(lastQuotation.MonthTimeStamp) + 100000,//Adding 1 minute and some seconds
                            Value = Convert.ToDecimal(lastQuotation.TotalPrice)
                        });
                    }
                }

                var quotationDataSet = detailVeiwModel.GraphItems[0].ItemValue[0].data[0].dataValue.ToArray();
                detailVeiwModel.QuotationDataSet = quotationDataSet;
            }

            if (
                detailVeiwModel.ReportQuotationInvoices.SelectMany(
                    x => x.ReportQuotationInvoiceItems.Where(y => y.IsInvoiceItem)).Any())
            {
                var invoiceGroups =
                    detailVeiwModel.ReportQuotationInvoices.SelectMany(
                        x => x.ReportQuotationInvoiceItems.Where(y => y.IsInvoiceItem)).ToArray();


                var firstInvoice = invoiceGroups.OrderBy(x => x.ReportQuotationInvoiceId).FirstOrDefault();
                var lastInvoice = invoiceGroups.OrderByDescending(x => x.ReportQuotationInvoiceId).FirstOrDefault();

                if (firstInvoice != null)
                {
                    detailVeiwModel.GraphStartTimeStamp = firstInvoice.MonthTimeStamp;
                    detailVeiwModel.GraphEndTimeStamp = lastInvoice.MonthTimeStamp;

                    detailVeiwModel.GraphItems.Add(new GraphItem
                    {
                        ItemLabel = "Invoice",
                        ItemValue = new List<GraphLabel>
                        {
                            new GraphLabel
                            {
                                label = "Invoice",
                                data = new List<GraphLabelData>
                                {
                                    new GraphLabelData
                                    {
                                        dataValue = new List<GraphLabelDataValues>()

                                    }
                                }
                            }
                        }
                    });
                    for (int i = 0; i < invoiceGroups.Count(); i++)
                    {
                        detailVeiwModel.GraphItems[1].ItemValue[0].data[0].dataValue.Add(new GraphLabelDataValues
                        {
                            TimeStamp = Convert.ToInt64(invoiceGroups[i].MonthTimeStamp),
                            Value = Convert.ToDecimal(invoiceGroups[i].TotalPrice)
                        });
                    }
                    if (invoiceGroups.Count() == 1)
                    {
                        //Ending Points on graph
                        detailVeiwModel.GraphItems[0].ItemValue[0].data[0].dataValue.Add(new GraphLabelDataValues
                        {
                            TimeStamp = Convert.ToInt64(lastInvoice.MonthTimeStamp) + 100000,//Adding 1 minute and some seconds
                            Value = Convert.ToDecimal(lastInvoice.TotalPrice)
                        });
                    }
                }

                var invoiceDataSet = detailVeiwModel.GraphItems[1].ItemValue[0].data[0].dataValue.ToArray();
                detailVeiwModel.InvoiceDataSet = invoiceDataSet;
            }

        }

        private string SetGraphImage(long reportId)
        {
            string curFile = Server.MapPath(ConfigurationManager.AppSettings["ReportImage"]) + "report_" + reportId +
                             ".png";
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
            return (time.Ticks/10000);
        }

        #endregion

        #region Generate PDF

        [AllowAnonymous]
        public ActionResult GeneratePdf(long? ReportId)
        {
            return new ActionAsPdf("ReportAsPDF", new { ReportId = ReportId }) { FileName = "Quotations & Invoices Report.pdf" };
        }

        [AllowAnonymous]
        public ActionResult ReportAsPdf(long? ReportId)
        {
            if (ReportId == null || ReportId <= 0) return View("Create");

            var response = reportService.GetQIReport((long)ReportId);

            QuotationInvoiceDetailViewModel detailViewModel = new QuotationInvoiceDetailViewModel();
            detailViewModel.ReportQuotationInvoices = response.Select(x => x.CreateReportFromServerToClient()).ToList();
            
                SetGraphData(detailViewModel);

            ViewBag.QueryString = "?ReportId=" + ReportId;
            var status = SetGraphImage((long)ReportId);
            detailViewModel.ImageSrc = SetGraphImage((long)ReportId) != null ? status : "";
            return View(detailViewModel);
        }

        [AllowAnonymous]
        public ActionResult GeneratePdfAll(long? ReportId)
        {
            return new ActionAsPdf("DetailsAllAsPDF", new { ReportId = ReportId }) { FileName = "Quotations & Invoices Report.pdf" };
        }
        [AllowAnonymous]
        public ActionResult DetailsAllAsPDF(long? ReportId)
        {
            if (ReportId == null || ReportId <= 0) return View("Create");

            var response = reportService.GetQIReport((long)ReportId);

            QuotationInvoiceDetailViewModel detailViewModel = new QuotationInvoiceDetailViewModel();
            detailViewModel.ReportQuotationInvoices = response.Select(x => x.CreateReportFromServerToClient()).ToList();

            return View(detailViewModel);
        }

        #endregion

        #endregion
    }
}