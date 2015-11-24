using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EPMS.Interfaces.IServices;
using EPMS.Web.Controllers;
using EPMS.WebModels.ModelMappers;
using EPMS.WebModels.ViewModels.Invoice;

namespace EPMS.Web.Areas.CMS.Controllers
{
    public class InvoiceController : BaseController
    {
        #region Private

        private readonly IInvoiceService invoiceService;

        #endregion

        #region Constructor

        public InvoiceController(IInvoiceService invoiceService)
        {
            this.invoiceService = invoiceService;
        }

        #endregion

        #region Public

        #region Index
        public ActionResult Index()
        {
            return View(new InvoiceListViewModel
            {
                Invoices = invoiceService.GetAll().Select(x => x.CreateFromServerToClient())
            });
        }

        #endregion

        #region Create

        public ActionResult Create()
        {
            return null;
        }

        #endregion

        #endregion
    }
}