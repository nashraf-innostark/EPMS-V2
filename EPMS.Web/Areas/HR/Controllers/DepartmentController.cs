using System;
using System.Linq;
using System.Web.Mvc;
using EPMS.Interfaces.IServices;
using EPMS.Models.RequestModels;
using EPMS.Web.Controllers;
using EPMS.Web.ModelMappers;
using EPMS.Web.ViewModels.Common;
using EPMS.Web.ViewModels.Department;

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

        public ActionResult Create(long? id)
        {
            DepartmentListViewModel listViewModel = new DepartmentListViewModel();
            if (id != null)
            {
                listViewModel.Department = oService.FindDepartmentById((long)id).CreateFrom();

            }

            return View(listViewModel);
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

        public ActionResult Details(long? id)
        {
            DepartmentListViewModel detailViewModel = new DepartmentListViewModel();
            if (id != null)
            {
                detailViewModel.Department = oService.FindDepartmentById((long)id).CreateFrom();
                detailViewModel.EmployeeList =
                    oService.FindEmployeeByDeprtmentId(id.Value).Select(employee => employee.CreateFromServerToClient());

            }
            return View(detailViewModel);
        }

    }
}