using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EPMS.Interfaces.IServices;
using EPMS.Models.ResponseModels;
using EPMS.Web.Controllers;
using EPMS.WebModels.ModelMappers;
using EPMS.WebModels.ViewModels.Common;
using EPMS.WebModels.ViewModels.Receipt;
using EPMS.WebModels.WebsiteModels;
using Microsoft.AspNet.Identity;

namespace EPMS.Web.Areas.CMS.Controllers
{
    public class ReceiptController : BaseController
    {
        #region Private

        private readonly IReceiptService receiptService;

        #endregion

        #region Constructor

        public ReceiptController(IReceiptService receiptService)
        {
            this.receiptService = receiptService;
        }

        #endregion

        #region Public

        #region Index

        public ActionResult Index()
        {
            var role = Session["RoleName"].ToString().ToLower();
            var userId = User.Identity.GetUserId();
            ViewBag.MessageVM = (MessageViewModel)TempData["message"];

            return View(new ReceiptListViewModel
            {
                Receipts = role == "customer"
                    ? receiptService.GetAll(userId).Select(x => x.CreateFromServerToClient())
                    : receiptService.GetAll().Select(x => x.CreateFromServerToClient())
            });
        }

        #endregion

        #region Detail

        public ActionResult Detail(long id)
        {
            ReceiptViewModel viewModel = new ReceiptViewModel();
            ReceiptResponse response = receiptService.GetReceiptDetails(id);

            viewModel.Receipt = response.Receipt.CreateFromServerToClient();
            viewModel.Invoice = response.Invoice.CreateFromServerToClient();
            viewModel.Quotation = response.Quotation.CreateForInvoice();
            viewModel.Customer = response.Customer.CreateFromServerToClient();
            viewModel.CompanyProfile = response.CompanyProfile != null ? response.CompanyProfile.CreateFromServerToClientForQuotation() : new CompanyProfile();

            ViewBag.LogoPath = ConfigurationManager.AppSettings["CompanyLogo"] + viewModel.CompanyProfile.CompanyLogoPath;
            ViewBag.EmployeeName = User.Identity.Name;

            ViewBag.MessageVM = (MessageViewModel) TempData["message"];
            return View(viewModel);
        }

        #endregion

        #endregion
    }
}