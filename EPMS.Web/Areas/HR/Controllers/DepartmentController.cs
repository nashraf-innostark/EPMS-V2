using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using EPMS.Interfaces.IServices;
using EPMS.Models.DomainModels;
using EPMS.Models.RequestModels;
using EPMS.Web.ModelMappers;
using EPMS.Web.ViewModels.Common;
using EPMS.Web.ViewModels.Department;

namespace EPMS.Web.Areas.HR.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly IDepartmentService oService;

        #region Constructor

        public DepartmentController(IDepartmentService oService)
        {
            this.oService = oService;
        }

        #endregion

        // GET: HR/Department
        public ActionResult Index()
        {
            if (Request.UrlReferrer == null || Request.UrlReferrer.AbsolutePath == "/Areas/HR/Department/Index")
            {
                Session["PageMetaData"] = null;
            }

            DepartmentSearchRequest departmentSearchRequest = Session["PageMetaData"] as DepartmentSearchRequest;

            Session["PageMetaData"] = null;

            ViewBag.MessageVM = TempData["MessageVm"] as MessageViewModel;

            return View(new DepartmentViewModel
            {
                DepartmentList = oService.GetAll().Select(x => x.CreateFrom()),
                SearchRequest = departmentSearchRequest ?? new DepartmentSearchRequest()
            });
        }

       public ActionResult AddEdit(long? id)
        {
            DepartmentViewModel viewModel = new DepartmentViewModel();
            if (id != null)
            {
                viewModel.Department = oService.FindDepartmentById((long)id).CreateFrom();

            }
            return View(viewModel);
        }

        [Authorize]
        [HttpPost]
        public ActionResult AddEdit(DepartmentViewModel departmentViewModel)
        {
            try
            {
                #region Update

                if (departmentViewModel.Department.DepartmentId > 0)
                {
                    var departmentStatusToUpdate = departmentViewModel.Department.CreateFrom();
                    departmentViewModel.Department.RecLastUpdatedDt = DateTime.Now;
                    if (oService.UpdateDepartment(departmentStatusToUpdate))
                    {
                        return RedirectToAction("Index");
                    }
                }
                #endregion

                #region Add

                else
                {
                    departmentViewModel.Department.RecCreatedDt = DateTime.Now;
                    var modelToSave = departmentViewModel.Department.CreateFrom();

                    if (oService.AddDepartment(modelToSave))
                    {
                        departmentViewModel.Department.DepartmentId = modelToSave.DepartmentId;
                        return RedirectToAction("Index");
                    }
                }

                #endregion

            }
            catch (Exception e)
            {
                return RedirectToAction("AddEdit");
            }
            return View(departmentViewModel);
        }

    }
}