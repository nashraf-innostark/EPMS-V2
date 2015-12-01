using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web.Mvc;
using EPMS.Interfaces.IServices;
using EPMS.Models.RequestModels.Reports;
using EPMS.Web.Controllers;
using EPMS.WebModels.ModelMappers;
using EPMS.WebModels.ModelMappers.Reports;
using EPMS.WebModels.ViewModels.Reports;
using Rotativa;

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
            return RedirectToAction(createViewModel.CustomerId > 0 ? "Details" : "DetailsAll", new { ReportId = reportId });
        }
        #endregion

        #region Detail

        public ActionResult Details(long? ReportId)
        {
            if (ReportId == null || ReportId <= 0) return View("Create");

            var response = reportService.GetQOReport((long)ReportId);

            QuotationOrderDetailViewModel detailViewModel=new QuotationOrderDetailViewModel();
            detailViewModel.QuotationOrderReports = response.Select(x => x.CreateReportFromServerToClient()).ToList();

            if(detailViewModel.QuotationOrderReports.SelectMany(x=>x.ReportQuotationOrderItems.Where(y=>y.IsQuotationReport)).Any())
                SetRFQsGraphData(detailViewModel);
            if (detailViewModel.QuotationOrderReports.SelectMany(x => x.ReportQuotationOrderItems.Where(y => y.IsOrderReport)).Any())
                SetOrdersGraphData(detailViewModel);

            detailViewModel.ReportId = (long)ReportId;

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
                for (int i = 0; i < ordersGroups.Count(); i++)
                {
                    detailVeiwModel.GraphItems[0].ItemValue[0].data[0].dataValue.Add(new GraphLabelDataValues
                    {
                        TimeStamp = Convert.ToInt64(ordersGroups[i].MonthTimeStamp),
                        Value = Convert.ToDecimal(ordersGroups[i].TotalPrice)
                    });
                }
            }
            var data = detailVeiwModel.GraphItems[0].ItemValue[0].data[0].dataValue.ToArray();
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