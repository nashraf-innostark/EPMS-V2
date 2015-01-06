using System;
using System.Linq;
using System.Web.Mvc;
using EPMS.Interfaces.IServices;
using EPMS.Web.Controllers;
using EPMS.Web.ModelMappers;
using EPMS.Web.ViewModels.Common;
using EPMS.Web.ViewModels.JobTitle;
using EPMS.WebBase.Mvc;
using Microsoft.AspNet.Identity;

namespace EPMS.Web.Areas.HR.Controllers
{
    [Authorize]
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
            if (id != null)
            {
                viewModel.JobTitle = jobTitleService.FindJobTitleById((long)id).CreateFrom();
            }
            viewModel.DepartmentList = departmentService.GetAll().Select(x => x.CreateFrom());
            return View(viewModel);
        }

        [HttpPost]
        [ValidateInput(false)]//this is due to CK Editor
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
                        TempData["message"] = new MessageViewModel { Message = "Job Title has been updated.", IsUpdated = true };
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
                        TempData["message"] = new MessageViewModel { Message = "Job Title has been saved.", IsSaved = true };
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
            
            return View(jobTitleViewModel);
        }

        #endregion
    }
}