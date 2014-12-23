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
        private readonly IDepartmentService departmentService;

        /// <summary>
        /// Constructor 
        /// </summary>
        /// <param name="departmentService"></param>
        /// <param name="jobTitleService"></param>
        #region Constructor

        public JobTitleController(IDepartmentService departmentService, IJobTitleService jobTitleService)
        {
            this.departmentService = departmentService;
            this.jobTitleService = jobTitleService;
        }

        #endregion



        // GET: JobTitles ListView Action Method
        public ActionResult Index()
        {
            return View(new JobTitleViewModel
            {
                DepartmentList = departmentService.GetAll().Select(x => x.CreateFrom()),
                JobTitleList = jobTitleService.GetAll().Select(x => x.CreateFrom())
            });
        }

        /// <summary>
        /// Add or Update Job Title Action Method
        /// </summary>
        /// <param name="id">id</param>
        /// <returns></returns>
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
        public ActionResult Create(JobTitleViewModel jobTitleViewModel)
        {
            //if (!ModelState.IsValid)
            //{
            //    jobTitleViewModel.DepartmentList = departmentService.GetAll().Select(x => x.CreateFrom());
                
            //    return View(jobTitleViewModel);
            //}
            try
            {
                #region Update

                if (jobTitleViewModel.JobTitle.JobTitleId > 0)
                {
                    var jobTitleToUpdate = jobTitleViewModel.JobTitle.CreateFrom();
                    jobTitleViewModel.JobTitle.RecLastUpdatedDt = DateTime.Now;
                    if (jobTitleService.UpdateJob(jobTitleToUpdate))
                    {
                        return RedirectToAction("Index");
                    }
                }
                #endregion

                #region Add

                else
                {
                    jobTitleViewModel.JobTitle.RecCreatedDt = DateTime.Now;
                    var modelToSave = jobTitleViewModel.JobTitle.CreateFrom();

                    if (jobTitleService.AddJob(modelToSave))
                    {
                        jobTitleViewModel.JobTitle.JobTitleId = modelToSave.JobTitleId;
                        return RedirectToAction("Index");
                    }
                }

                #endregion

            }
            catch (Exception e)
            {
                return RedirectToAction("Create");
            }
            
            return View(jobTitleViewModel);
        }
    }
}