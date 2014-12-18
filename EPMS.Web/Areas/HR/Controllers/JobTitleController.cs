using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using EPMS.Interfaces.IServices;
using EPMS.Models.RequestModels;
using EPMS.Web.Controllers;
using EPMS.Web.ModelMappers;
using EPMS.Web.ViewModels.Common;
using EPMS.Web.ViewModels.JobTitle;
using EPMS.Web.Models;

namespace EPMS.Web.Areas.HR.Controllers
{
    public class JobTitleController : BaseController
    {
        private readonly IJobTitleService jobTitleService;
        private readonly IDepartmentService DepartmentService;

        /// <summary>
        /// Constructor 
        /// </summary>
        /// <param name="departmentService"></param>
        /// <param name="jobTitleService"></param>
        #region Constructor

        public JobTitleController(IDepartmentService departmentService, IJobTitleService jobTitleService)
        {
            this.DepartmentService = departmentService;
            this.jobTitleService = jobTitleService;
        }

        #endregion



        // GET: JobTitles ListView Action Method
        public ActionResult JobTitleLV()
        {
            JobTitleSearchRequest jobTitleSearchRequest = Session["PageMetaData"] as JobTitleSearchRequest;

            Session["PageMetaData"] = null;

            ViewBag.MessageVM = TempData["MessageVm"] as MessageViewModel;

            IEnumerable<JobTitle> jobList = jobTitleService.GetAll().Select(x => x.CreateFrom());

            return View(new JobTitleViewModel
            {
                DepartmentList = DepartmentService.GetAll().Select(x => x.CreateFrom()),
                JobTitleList = jobList,
                SearchRequest = jobTitleSearchRequest ?? new JobTitleSearchRequest()
            });
        }

        public ActionResult ComboBox()
        {
            return View();
        }

        /// <summary>
        /// Add or Update Job Title Action Method
        /// </summary>
        /// <param name="id">id</param>
        /// <returns></returns>
        public ActionResult AddEdit(int? id)
        {
            JobTitleViewModel viewModel = new JobTitleViewModel();
            //if (id != null)
            //{
            //    viewModel.Employee = oEmployeeService.FindEmployeeById(id).CreateFrom();
            //}
            return View(viewModel);
        }
        public int AddData(JobTitleViewModel viewModel)
        {
            viewModel.JobTitle.RecCreatedDt = DateTime.Now;
            viewModel.JobTitle.RecCreatedBy = User.Identity.Name;
            var jobToSave = viewModel.JobTitle.CreateFrom();
            if (jobTitleService.AddJob(jobToSave))
            {
                TempData["message"] = new MessageViewModel { Message = "Employee has been Added", IsSaved = true };
                return 1;
            }
            return 0;
        }
    }
}