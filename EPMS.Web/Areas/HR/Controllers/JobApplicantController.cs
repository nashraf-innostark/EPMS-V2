using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EPMS.Interfaces.IServices;
using EPMS.Models.DomainModels;
using EPMS.Web.Controllers;
using EPMS.Web.ModelMappers;
using EPMS.Web.ViewModels.Common;
using EPMS.Web.ViewModels.JobApplicant;
using EPMS.Web.ViewModels.JobOffered;
using Microsoft.AspNet.Identity;

namespace EPMS.Web.Areas.HR.Controllers
{
    public class JobApplicantController : BaseController
    {
        #region Private

        private readonly IJobOfferedService jobOfferedService;
        private readonly IJobTitleService jobTitleService;
        private readonly IJobApplicantService jobApplicantService;

        #endregion

        #region Constructor

        public JobApplicantController(IJobOfferedService jobOfferedService, IJobTitleService jobTitleService, IJobApplicantService jobApplicantService)
        {
            this.jobOfferedService = jobOfferedService;
            this.jobTitleService = jobTitleService;
            this.jobApplicantService = jobApplicantService;
        }
        
        #endregion

        #region Public

        public ActionResult Jobs()
        {
            return View(new JobApplicantViewModel
            {
                JobTitleList = jobTitleService.GetAll().Select(x => x.CreateFrom()),
                JobOfferedList = jobOfferedService.GetAll().Select(x => x.CreateFrom())
            });
        }
        
        public ActionResult Apply(long? id)
        {
            JobApplicantViewModel jobOfferedViewModel = new JobApplicantViewModel();
            if (id != null)
            {
                jobOfferedViewModel.JobOffered = jobOfferedService.FindJobOfferedById((long)id).CreateFrom();
            }
            jobOfferedViewModel.JobTitleList = jobTitleService.GetAll().Select(x => x.CreateFrom());
            return View(jobOfferedViewModel);
        }

        [HttpPost]
        public ActionResult Apply(JobApplicantViewModel jobApplicantViewModel)
        {
            try
            {

                #region Add
                {
                    jobApplicantViewModel.JobApplicant.RecCreatedBy = User.Identity.GetUserId();
                    jobApplicantViewModel.JobApplicant.RecCreatedDt = DateTime.Now;
                    JobApplicant modelToSave = jobApplicantViewModel.JobApplicant.CreateFrom();
                    modelToSave.JobOfferedId = jobApplicantViewModel.JobOffered.JobOfferedId;

                    if (jobApplicantService.AddJobApplicant(modelToSave))
                    {
                        TempData["message"] = new MessageViewModel { Message = "Job Offer has been saved.", IsSaved = true };
                        jobApplicantViewModel.JobOffered.JobOfferedId = modelToSave.JobOfferedId;
                        return RedirectToAction("Index");
                    }
                }

                #endregion

            }
            catch (Exception e)
            {
                TempData["message"] = new MessageViewModel { Message = e.Message, IsError = true };
                return RedirectToAction("Jobs", e);
            }

            return View(jobApplicantViewModel);
        }

        #endregion
    }
}