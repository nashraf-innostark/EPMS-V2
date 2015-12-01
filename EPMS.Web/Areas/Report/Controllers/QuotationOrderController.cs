using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;
using EPMS.Interfaces.IServices;
using EPMS.Models.RequestModels.Reports;
using EPMS.Web.Controllers;
using EPMS.WebModels.ModelMappers;
using EPMS.WebModels.ModelMappers.Reports;
using EPMS.WebModels.ViewModels.Reports;

namespace EPMS.Web.Areas.Report.Controllers
{
    public class QuotationOrderController : BaseController
    {
        #region Private

        private readonly ICustomerService customerService;
        private readonly IReportService reportService;

        #endregion

        #region Constructor

        public QuotationOrderController(ICustomerService customerService, IReportService reportService)
        {
            this.customerService = customerService;
            this.reportService = reportService;
        }

        #endregion

        #region Public

        #region Create
        public ActionResult Create()
        {
            QuotationOrderReportCreateViewModel viewModel = new QuotationOrderReportCreateViewModel
            {
                Customers = customerService.GetAll().Select(x => x.CreateForDashboard()).ToList()
            };
            return View(viewModel);
        }
        [HttpPost]
        public ActionResult Create(QuotationOrderReportCreateViewModel createViewModel)
        {
            var request = new QOReportCreateOrDetailsRequest
            {
                CustomerId = createViewModel.CustomerId,
                RequesterRole = Session["RoleName"].ToString(),
                RequesterId = Session["UserID"].ToString(),
                From = Convert.ToDateTime(createViewModel.StartDate),
                To = Convert.ToDateTime(createViewModel.EndDate),
            };
            var reportId = reportService.SaveQOReport(request);
            return RedirectToAction("Details", new { ReportId = reportId });
        }
        #endregion

        #region Detail

        public ActionResult Detail(long? ReportId)
        {
            if (ReportId == null || ReportId <= 0) return View("Create");

            var response = reportService.GetQOReport((long)ReportId);

            QuotationOrderDetailViewModel detailViewModel=new QuotationOrderDetailViewModel();
            detailViewModel.QuotationOrderReports = response.Select(x => x.CreateReportFromServerToClient()).ToList();

            SetGraphData(detailViewModel);
            ViewBag.QueryString = "?ReportId=" + ReportId;


            return View(detailViewModel);
        }

        #endregion

        #region AllDetail
        public ActionResult DetailsAll(long? ReportId)
        {
            if (ReportId == null || ReportId <= 0) return View("Create");

            var response = reportService.GetQOReport((long)ReportId);

            QuotationOrderDetailViewModel detailViewModel = new QuotationOrderDetailViewModel();
            detailViewModel.QuotationOrderReports = response.Select(x => x.CreateReportFromServerToClient()).ToList();

            ViewBag.QueryString = "?ReportId=" + ReportId;

            return View(detailViewModel);
        }

        #endregion


        #region Graph

        private QuotationOrderDetailViewModel SetGraphData(QuotationOrderDetailViewModel detailVeiwModel)
        {
            var quotationGroups =
                detailVeiwModel.QuotationOrderReports.SelectMany(x => x.ReportQuotationOrderItems).ToArray();

            var firstQuotation = quotationGroups.OrderBy(x => x.ReportQuotOrderItemId).FirstOrDefault();
            var lastQuotation = quotationGroups.OrderByDescending(x => x.ReportQuotOrderItemId).FirstOrDefault();



            if (firstQuotation != null)
            {
                detailVeiwModel.GraphStartTimeStamp = firstQuotation.MonthTimeStamp;
                detailVeiwModel.GraphEndTimeStamp = lastQuotation.MonthTimeStamp;

                detailVeiwModel.GraphItems.Add(new GraphItem
                {
                    ItemLabel = "Orders",
                    ItemValue = new List<GraphLabel>
                    {
                        new GraphLabel
                        {
                            label = "Orders",
                            data = new List<GraphLabelData>
                            {
                                new GraphLabelData
                                {
                                    dataValue=new List<GraphLabelDataValues>()
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
            }
            var data = detailVeiwModel.GraphItems[0].ItemValue[0].data[0].dataValue.ToArray();
            detailVeiwModel.DataSet = data;
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