using System;
using System.Linq;
using System.Web.Mvc;
using EPMS.Interfaces.IServices;
using EPMS.Models.ResponseModels;
using EPMS.WebModels.ModelMappers;
using EPMS.WebModels.ViewModels.Common;
using EPMS.WebModels.ViewModels.JobTitle;
using EPMS.Web.Controllers;
using EPMS.WebBase.Mvc;
using Microsoft.AspNet.Identity;

namespace EPMS.Web.Areas.HR.Controllers
{
    [Authorize]
    [SiteAuthorize(PermissionKey = "HRS", IsModule = true)]
    public class JobTitleController : BaseController
    {
        #region Private

        private readonly IJobTitleService jobTitleService;
        private readonly IDepartmentService departmentService;
        private readonly IEmployeeService employeeService;

        #endregion
        
        #region Constructor

        /// <summary>
        /// Constructor 
        /// </summary>
        /// <param name="departmentService"></param>
        /// <param name="jobTitleService"></param>
        public JobTitleController(IDepartmentService departmentService, IJobTitleService jobTitleService, IEmployeeService employeeService)
        {
            this.departmentService = departmentService;
            this.jobTitleService = jobTitleService;
            this.employeeService = employeeService;
        }

        #endregion

        #region Public

        // GET: JobTitles ListView Action Method
        [SiteAuthorize(PermissionKey = "JobTitleIndex")]
        public ActionResult Index()
        {
            return View(new JobTitleListViewModel
            {
                JobTitleList = jobTitleService.GetAll().Select(x => x.CreateFrom())
            });
        }

        /// <summary>
        /// Add or Update Job Title Action Method
        /// </summary>
        /// <param name="id">id</param>
        /// <returns></returns>
        [SiteAuthorize(PermissionKey = "JobTitleCreate")]
        public ActionResult Create(long? id)
        {
            JobTitleViewModel viewModel = new JobTitleViewModel();
            JobTitleResponse response = id != null ? jobTitleService.GetResponseWithJobTitle((long) id) : jobTitleService.GetResponseWithJobTitle(0);
            if (id != null)
            {
                viewModel.JobTitle = response.JobTitle.CreateFrom();
            }
            viewModel.DepartmentList = response.Departments.Select(x => x.CreateFrom());
            return View(viewModel);
        }

        [HttpPost]
        [ValidateInput(false)]//this is due to CK Editor
        [SiteAuthorize(PermissionKey = "JobTitleCreate")]
        public ActionResult Create(JobTitleViewModel jobTitleViewModel)
        {
            try
            {
                #region Update

                if (jobTitleViewModel.JobTitle.JobTitleId > 0)
                {
                    jobTitleViewModel.JobTitle.RecLastUpdatedBy = User.Identity.GetUserId();
                    jobTitleViewModel.JobTitle.RecLastUpdatedDt = DateTime.Now;
                    var jobTitleToUpdate = jobTitleViewModel.JobTitle.CreateFrom();
                    if (jobTitleService.UpdateJob(jobTitleToUpdate))
                    {
                        TempData["message"] = new MessageViewModel { Message = EPMS.WebModels.Resources.HR.JobTitle.UpdateJobTitle, IsUpdated = true };
                        return RedirectToAction("Index");
                    }
                }
                #endregion

                #region Add

                else
                {
                    jobTitleViewModel.JobTitle.RecCreatedBy = User.Identity.GetUserId();
                    jobTitleViewModel.JobTitle.RecCreatedDt = DateTime.Now;
                    var modelToSave = jobTitleViewModel.JobTitle.CreateFrom();

                    if (jobTitleService.AddJob(modelToSave))
                    {
                        TempData["message"] = new MessageViewModel { Message = EPMS.WebModels.Resources.HR.JobTitle.SaveJobTitle, IsSaved = true };
                        jobTitleViewModel.JobTitle.JobTitleId = modelToSave.JobTitleId;
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
            jobTitleViewModel.DepartmentList = departmentService.GetAll().Select(x => x.CreateFrom());
            return View(jobTitleViewModel);
        }

        #endregion
    }
}