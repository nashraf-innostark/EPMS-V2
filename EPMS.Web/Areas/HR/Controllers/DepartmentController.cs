using System;
using System.Linq;
using System.Web.Mvc;
using EPMS.Interfaces.IServices;
using EPMS.Models.ResponseModels;
using EPMS.Web.Controllers;
using EPMS.Web.ModelMappers;
using EPMS.Web.ViewModels.Common;
using EPMS.Web.ViewModels.Department;
using EPMS.WebBase.Mvc;
using Microsoft.AspNet.Identity;

namespace EPMS.Web.Areas.HR.Controllers
{
    [Authorize]
    [SiteAuthorize(PermissionKey = "HRS", IsModule = true)]
    public class DepartmentController : BaseController
    {
        #region Private

        private readonly IDepartmentService oService;
        private readonly IEmployeeService empService;

        #endregion

        #region Constructor

        public DepartmentController(IDepartmentService oService)
        {
            this.oService = oService;
        }

        #endregion

        #region Public
        // GET: HR/Department
        [SiteAuthorize(PermissionKey = "DepartmentIndex")]
        public ActionResult Index()
        {
            ViewBag.MessageVM = TempData["MessageVm"] as MessageViewModel;

            return View(new DepartmentListViewModel
            {
                DepartmentList = oService.GetAll().Select(x => x.CreateFrom()),
            });
        }

        [SiteAuthorize(PermissionKey = "DepartmentCreate")]
        public ActionResult Create(long? id)
        {
            DepartmentListViewModel detailViewModel = new DepartmentListViewModel();
            if (id != null)
            {
                DepartmentResponse response = oService.FindDepartmentResponseByDepartmentId((long)id);
                detailViewModel.Department = response.Department.CreateFrom();
                detailViewModel.EmployeeList = response.Employees.Select(employee => employee.CreateFromServerToClient());
            }
            return View(detailViewModel);
        }

        [HttpPost]
        public ActionResult Create(DepartmentListViewModel departmentListViewModel)
        {
            //if (!ModelState.IsValid)
            //{
            //    return View(departmentListViewModel);
            //}
            try
            {
                #region Update

                if (departmentListViewModel.Department.DepartmentId > 0)
                {
                    departmentListViewModel.Department.RecLastUpdatedBy = User.Identity.GetUserId();
                    departmentListViewModel.Department.RecLastUpdatedDt = DateTime.Now;
                    var departmentToUpdate = departmentListViewModel.Department.CreateFrom();
                    if (oService.UpdateDepartment(departmentToUpdate))
                    {
                        TempData["message"] = new MessageViewModel { Message = Resources.HR.Department.UpdateDepartment, IsUpdated = true };
                        return RedirectToAction("Index");
                    }
                }
                #endregion

                #region Add

                else
                {
                    departmentListViewModel.Department.RecCreatedBy = User.Identity.GetUserId();
                    departmentListViewModel.Department.RecCreatedDt = DateTime.Now;
                    var modelToSave = departmentListViewModel.Department.CreateFrom();

                    if (oService.AddDepartment(modelToSave))
                    {
                        TempData["message"] = new MessageViewModel { Message = Resources.HR.Department.SaveDepartment, IsSaved = true };
                        departmentListViewModel.Department.DepartmentId = modelToSave.DepartmentId;
                        return RedirectToAction("Index");
                    }
                }

                #endregion

            }
            catch (Exception e)
            {
                TempData["message"] = new MessageViewModel { Message = e.Message, IsError = true };
                return RedirectToAction("Create", e);
            }
            return View(departmentListViewModel);
        }
        #endregion

    }
}