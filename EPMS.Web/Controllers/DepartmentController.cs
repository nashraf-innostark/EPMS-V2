using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EPMS.Interfaces.IServices;
using EPMS.Models.RequestModels;
using EPMS.Web.ModelMappers;
using EPMS.Web.ViewModels.Common;
using EPMS.Web.ViewModels.Department;

namespace EPMS.Web.Controllers
{
    public class DepartmentController : BaseController
    {
        private readonly IDepartmentService oDepartmentService;


        #region Constructor

        public DepartmentController(IDepartmentService oDepartmentService)
        {
            this.oDepartmentService = oDepartmentService;
        }

        #endregion


        // GET: Department
        public ActionResult DepartmentLV()
        {
            ViewBag.MessageVM = TempData["MessageVm"] as MessageViewModel;

            return View(new DepartmentViewModel
            {
                DepartmentList = oDepartmentService.LoadAll().Select(x=>x.CreateFrom())
            });
        }
    }
}