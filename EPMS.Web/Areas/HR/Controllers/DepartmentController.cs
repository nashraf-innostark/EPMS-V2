using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using EPMS.Implementation.Services;
using EPMS.Interfaces.IServices;
using EPMS.Models.DomainModels;
using EPMS.Models.RequestModels;
using EPMS.Web.Controllers;
using EPMS.Web.ModelMappers;
using EPMS.Web.ViewModels.Common;
using EPMS.Web.ViewModels.Department;
using EPMS.Web.ViewModels.Employee;

namespace EPMS.Web.Areas.HR.Controllers
{
    public class DepartmentController : BaseController
    {
        private readonly IDepartmentService oService;
        private readonly IEmployeeService empService;

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

            return View(new DepartmentListViewModel
            {
                DepartmentList = oService.GetAll().Select(x => x.CreateFrom()),
                SearchRequest = departmentSearchRequest ?? new DepartmentSearchRequest()
            });
        }

        public ActionResult Create(int? id)
        {
            DepartmentListViewModel listViewModel = new DepartmentListViewModel();
            if (id != null)
            {
                listViewModel.Department = oService.FindDepartmentById(id).CreateFrom();

            }

            return View(listViewModel);
        }

        [HttpPost]
        public ActionResult Create(DepartmentListViewModel departmentListViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(departmentListViewModel);
            }
            try
            {
                #region Update

                if (departmentListViewModel.Department.DepartmentId > 0)
                {
                    var departmentToUpdate = departmentListViewModel.Department.CreateFrom();
                    departmentListViewModel.Department.RecLastUpdatedDt = DateTime.Now;
                    if (oService.UpdateDepartment(departmentToUpdate))
                    {
                        return RedirectToAction("Index");
                    }
                }
                #endregion

                #region Add

                else
                {
                    departmentListViewModel.Department.RecCreatedDt = DateTime.Now;
                    var modelToSave = departmentListViewModel.Department.CreateFrom();

                    if (oService.AddDepartment(modelToSave))
                    {
                        departmentListViewModel.Department.DepartmentId = modelToSave.DepartmentId;
                        return RedirectToAction("Index");
                    }
                }

                #endregion

            }
            catch (Exception e)
            {
                return RedirectToAction("Create");
            }
            return View(departmentListViewModel);
        }

        public ActionResult Details(int? id)
        {
            DepartmentListViewModel detailViewModel = new DepartmentListViewModel();
            if (id != null)
            {
                detailViewModel.Department = oService.FindDepartmentById(id).CreateFrom();
                detailViewModel.EmployeeList =
                    oService.FindEmployeeByDeprtmentId(id.Value).Select(employee => employee.CreateFrom());

            }
            return View(detailViewModel);
        }

    }
}