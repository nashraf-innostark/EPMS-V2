using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using EPMS.Implementation.Services;
using EPMS.Models.RequestModels;
using EPMS.Models.ResponseModels;
using EPMS.Web.Controllers;
using EPMS.WebModels.ModelMappers;
using EPMS.WebModels.ViewModels.Employee;
using EPMS.WebModels.ViewModels.Reports;
using EPMS.WebModels.WebsiteModels;
using Microsoft.AspNet.Identity;

namespace EPMS.Web.Areas.Report.Controllers
{
    public class ProjectsAndTasksController : BaseController
    {
        public ProjectsAndTasksController()
        {
            
        }
        public ActionResult Index()
        {
            var projectsReports = new ProjectsReportsListViewModel();

            return View(projectsReports);
        }
        [HttpPost]
        public ActionResult Index(ProjectReportSearchRequest searchRequest)
        {
            //searchRequest.SearchString = Request["search"];
            //var projectsAndTasksResponse = EmployeeService.GetAllEmployees(searchRequest);
            //IEnumerable<Employee> employeeList =
            //    employees.Employeess.Select(x => x.CreateFromServerToClientWithImage()).ToList();
            //EmployeeViewModel employeeViewModel = new EmployeeViewModel
            //{
            //    aaData = employeeList,
            //    iTotalRecords = Convert.ToInt32(employees.TotalRecords),
            //    iTotalDisplayRecords = Convert.ToInt32(employees.TotalDisplayRecords),
            //    sEcho = searchRequest.sEcho
            //};
            //return Json(employeeViewModel, JsonRequestBehavior.AllowGet);
        }
    }
}