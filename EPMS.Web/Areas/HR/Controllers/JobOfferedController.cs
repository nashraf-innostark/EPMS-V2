using System;
using System.Linq;
using System.Web.Mvc;
using EPMS.Interfaces.IServices;
using EPMS.Web.Controllers;
using EPMS.Web.ModelMappers;
using EPMS.Web.ViewModels.Common;
using EPMS.Web.ViewModels.JobOffered;
using EPMS.Web.ViewModels.JobTitle;
using EPMS.WebBase.Mvc;
using Microsoft.AspNet.Identity;

namespace EPMS.Web.Areas.HR.Controllers
{
    [Authorize]
    public class JobOfferedController : BaseController
    {
        #region Private

        private readonly IJobOfferedService jobOfferedService;
        private readonly IJobTitleService jobTitleService;

        #endregion

        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="jobOfferedService"></param>
        /// <param name="jobTitleService"></param>
        public JobOfferedController(IJobOfferedService jobOfferedService, IJobTitleService jobTitleService)
        {
            this.jobOfferedService = jobOfferedService;
            this.jobTitleService = jobTitleService;
        }

        #endregion

        #region Public

        // GET: Job Offered ListView Action Method
        [SiteAuthorize(PermissionKey = "RecruitmentIndex")]
        public ActionResult Index()
        {
            return View(new JobOfferedViewModel
            {
                JobOfferedList = jobOfferedService.GetAll().Select(x => x.CreateFrom())
            });
        }

        [HttpGet]
        public JsonResult GetJobTitles(long deptId)
        {
            var jobTitles = jobTitleService.GetJobTitlesByJobOfferedId(deptId).CreateFrom();
            return Json(jobTitles, JsonRequestBehavior.AllowGet);
        }

        [SiteAuthorize(PermissionKey = "RecruitmentCreate")]
        public ActionResult Create(long? id)
        {
            JobOfferedViewModel jobOfferedViewModel = new JobOfferedViewModel();
            if (id != null)
            {
                jobOfferedViewModel.JobOffered = jobOfferedService.FindJobOfferedById((long)id).CreateFrom();
            }
            jobOfferedViewModel.JobTitleList = jobTitleService.GetAll().Select(x => x.CreateFrom());
            return View(jobOfferedViewModel);
        }

        [HttpPost]
        public ActionResult Create(JobOfferedViewModel jobOfferedViewModel)
        {
            try
            {
                #region Update

                if (jobOfferedViewModel.JobOffered.JobOfferedId > 0)
                {
                    jobOfferedViewModel.JobOffered.RecLastUpdatedBy = User.Identity.GetUserId();
                    jobOfferedViewModel.JobOffered.RecLastUpdatedDt = DateTime.Now;
                    var jobOffereToUpdate = jobOfferedViewModel.JobOffered.CreateFrom();
                    if (jobOfferedService.UpdateJobOffered(jobOffereToUpdate))
                    {
                        TempData["message"] = new MessageViewModel { Message = Resources.HR.JobOffered.UpdateJobOffered, IsUpdated = true };
                        return RedirectToAction("Index");
                    }
                }
                #endregion

                #region Add

                else
                {
                    jobOfferedViewModel.JobOffered.RecCreatedBy = User.Identity.GetUserId();
                    jobOfferedViewModel.JobOffered.RecCreatedDt = DateTime.Now;
                    var modelToSave = jobOfferedViewModel.JobOffered.CreateFrom();

                    if (jobOfferedService.AddJobOffered(modelToSave))
                    {
                        TempData["message"] = new MessageViewModel { Message = Resources.HR.JobOffered.SaveJobOffered, IsSaved = true };
                        jobOfferedViewModel.JobOffered.JobOfferedId = modelToSave.JobOfferedId;
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

            return View(jobOfferedViewModel);
        }

        #endregion
    }
}