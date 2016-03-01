using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web.Mvc;
using EPMS.Interfaces.IServices;
using EPMS.Models.RequestModels.Reports;
using EPMS.Web.Controllers;
using EPMS.WebBase.Mvc;
using EPMS.WebModels.ModelMappers;
using EPMS.WebModels.ModelMappers.Reports;
using EPMS.WebModels.ViewModels.Reports;
using Rotativa;

namespace EPMS.Web.Areas.Report.Controllers
{
    [SiteAuthorize(PermissionKey = "CustomerServiceReport", IsModule = true)]
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
        [SiteAuthorize(PermissionKey = "RFQOrderPlacedReportCreate")]
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
            return RedirectToAction(createViewModel.CustomerId > 0 ? "Details" : "DetailsAll", new { ReportId = reportId });
        }
        #endregion

        #region Detail
        [SiteAuthorize(PermissionKey = "RFQOrderPlacedReportView")]
        public ActionResult Details(long? ReportId)
        {
            if (ReportId == null || ReportId <= 0) return View("Create");

            var response = reportService.GetQOReport((long)ReportId);

            QuotationOrderDetailViewModel detailViewModel=new QuotationOrderDetailViewModel();
            detailViewModel.QuotationOrderReports = response.Select(x => x.CreateReportFromServerToClient()).ToList();


            //call GraphData Setter in the following order
            if(detailViewModel.QuotationOrderReports.SelectMany(x=>x.ReportQuotationOrderItems.Where(y=>y.IsQuotationReport)).Any())
                SetRFQsGraphData(detailViewModel);
            if (detailViewModel.QuotationOrderReports.SelectMany(x => x.ReportQuotationOrderItems.Where(y => y.IsOrderReport)).Any())
                SetOrdersGraphData(detailViewModel);
            //Set graph timeline marking points
            detailViewModel.GraphStartTimeStamp = GetJavascriptTimestamp(response.FirstOrDefault().Report.ReportFromDate).ToString();
            detailViewModel.GraphEndTimeStamp = GetJavascriptTimestamp(response.FirstOrDefault().Report.ReportToDate).ToString();

            detailViewModel.ReportId = (long)ReportId;

            return View(detailViewModel);
        }

        #endregion

        #region AllDetail
        [SiteAuthorize(PermissionKey = "RFQOrderPlacedReportView")]
        public ActionResult DetailsAll(long? ReportId)
        {
            if (ReportId == null || ReportId <= 0) return View("Create");

            var response = reportService.GetQOReport((long)ReportId);

            QuotationOrderDetailViewModel detailViewModel = new QuotationOrderDetailViewModel();
            detailViewModel.QuotationOrderReports = response.Select(x => x.CreateReportFromServerToClient()).ToList();

            detailViewModel.ReportId = (long)ReportId;

            return View(detailViewModel);
        }

        #endregion


        #region Graphs
        private static void SetRFQsGraphData(QuotationOrderDetailViewModel detailVeiwModel)
        {
            var quotationGroups =
                detailVeiwModel.QuotationOrderReports.SelectMany(x => x.ReportQuotationOrderItems.Where(y=>y.IsQuotationReport)).ToArray();

            var firstQuotation = quotationGroups.OrderBy(x => x.ReportQuotOrderItemId).FirstOrDefault();
            var lastQuotation = quotationGroups.OrderByDescending(x => x.ReportQuotOrderItemId).FirstOrDefault();

            if (firstQuotation != null)
            {
                //detailVeiwModel.GraphStartTimeStamp = firstQuotation.MonthTimeStamp;
                detailVeiwModel.GraphItems.Add(new GraphItem
                {
                    ItemLabel = "RFQs",
                    ItemValue = new List<GraphLabel>
                    {
                        new GraphLabel
                        {
                            label = "RFQs",
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
                        Value = Convert.ToDecimal(detailVeiwModel.QuotationOrderReports.FirstOrDefault().NoOfRFQ)
                    });
                }
                if (quotationGroups.Count() == 1)
                {
                    //Ending Points on graph
                    detailVeiwModel.GraphItems[0].ItemValue[0].data[0].dataValue.Add(new GraphLabelDataValues
                    {
                        TimeStamp = Convert.ToInt64(lastQuotation.MonthTimeStamp) + 100000,//Adding 1 minute and some seconds
                        Value = Convert.ToDecimal(detailVeiwModel.QuotationOrderReports.FirstOrDefault().NoOfRFQ)
                    });
                }
                
            }
            var data = detailVeiwModel.GraphItems[0].ItemValue[0].data[0].dataValue.ToArray();
            detailVeiwModel.RFQsDataSet = data;
        }
        private static void SetOrdersGraphData(QuotationOrderDetailViewModel detailVeiwModel)
        {
            var ordersGroups =
                detailVeiwModel.QuotationOrderReports.SelectMany(x => x.ReportQuotationOrderItems.Where(y=>y.IsOrderReport)).ToArray();

            var firstQuotation = ordersGroups.OrderBy(x => x.ReportQuotOrderItemId).FirstOrDefault();
            var lastQuotation = ordersGroups.OrderByDescending(x => x.ReportQuotOrderItemId).FirstOrDefault();



            if (firstQuotation != null)
            {

                //detailVeiwModel.GraphEndTimeStamp = lastQuotation.MonthTimeStamp;
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
                for (int i = 0; i < ordersGroups.Count(); i++)
                {
                    detailVeiwModel.GraphItems[1].ItemValue[0].data[0].dataValue.Add(new GraphLabelDataValues
                    {
                        TimeStamp = Convert.ToInt64(ordersGroups[i].MonthTimeStamp),
                        Value = Convert.ToDecimal(detailVeiwModel.QuotationOrderReports.FirstOrDefault().NoOfOrders)
                    });
                }
                //Ending Points on graph
                if (ordersGroups.Count() == 1)
                {
                    detailVeiwModel.GraphItems[1].ItemValue[0].data[0].dataValue.Add(new GraphLabelDataValues
                    {
                        TimeStamp = Convert.ToInt64(lastQuotation.MonthTimeStamp) + 100000,//Adding 1 minute and some seconds
                        Value = Convert.ToDecimal(detailVeiwModel.QuotationOrderReports.FirstOrDefault().NoOfOrders)
                    });
                }
                
            }
            var data = detailVeiwModel.GraphItems[1].ItemValue[0].data[0].dataValue.ToArray();
            detailVeiwModel.OrdersDataSet = data;
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
        [AllowAnonymous]
        public ActionResult GeneratePdf(long? ReportId)
        {
            //Dictionary<string, string> cookies = (Dictionary<string, string>)Session["Cookies"];
            return new ActionAsPdf("DetailsAsPDF", new { ReportId = ReportId }) { FileName = "RFQs & Orders Report.pdf" };
        }
        [AllowAnonymous]
        public ActionResult DetailsAsPDF(long? ReportId)
        {
            if (ReportId == null || ReportId <= 0) return View("Create");

            var response = reportService.GetQOReport((long)ReportId);

            QuotationOrderDetailViewModel detailViewModel = new QuotationOrderDetailViewModel();
            detailViewModel.QuotationOrderReports = response.Select(x => x.CreateReportFromServerToClient()).ToList();
            if (detailViewModel.QuotationOrderReports.SelectMany(x => x.ReportQuotationOrderItems.Where(y => y.IsQuotationReport)).Any())
                SetRFQsGraphData(detailViewModel);
            if (detailViewModel.QuotationOrderReports.SelectMany(x => x.ReportQuotationOrderItems.Where(y => y.IsOrderReport)).Any())
                SetOrdersGraphData(detailViewModel);

            ViewBag.QueryString = "?ReportId=" + ReportId;
            var status = SetGraphImage((long)ReportId);
            detailViewModel.ImageSrc = SetGraphImage((long)ReportId) != null ? status : "";
            return View(detailViewModel);
        }


        [AllowAnonymous]
        public ActionResult GeneratePdfAll(long? ReportId)
        {
            //Dictionary<string, string> cookies = (Dictionary<string, string>)Session["Cookies"];
            return new ActionAsPdf("DetailsAllAsPDF", new { ReportId = ReportId }) { FileName = "RFQs & Orders Report.pdf" };
        }
        [AllowAnonymous]
        public ActionResult DetailsAllAsPDF(long? ReportId)
        {
            if (ReportId == null || ReportId <= 0) return View("Create");

            var response = reportService.GetQOReport((long)ReportId);

            QuotationOrderDetailViewModel detailViewModel = new QuotationOrderDetailViewModel();
            detailViewModel.QuotationOrderReports = response.Select(x => x.CreateReportFromServerToClient()).ToList();
         
            return View(detailViewModel);
        }
        #endregion

        #endregion
    }
}