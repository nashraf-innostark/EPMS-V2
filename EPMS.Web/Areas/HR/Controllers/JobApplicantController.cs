﻿using System;
﻿using System.Collections.Generic;
﻿using System.Configuration;
﻿using System.Globalization;
﻿using System.IO;
﻿using System.Linq;
﻿using System.Net;
﻿using System.Web;
﻿using System.Web.Mvc;
﻿using EPMS.Implementation.Identity;
﻿using EPMS.Implementation.Services;
﻿using EPMS.Interfaces.IServices;
﻿using EPMS.Models.DomainModels;
﻿using EPMS.Models.RequestModels;
﻿using EPMS.Models.ResponseModels;
﻿using EPMS.Web.Controllers;
using EPMS.Web.ModelMappers;
﻿using EPMS.Web.Models;
﻿using EPMS.Web.ViewModels.Common;
using EPMS.Web.ViewModels.JobApplicant;
﻿using Microsoft.AspNet.Identity;
﻿using Microsoft.AspNet.Identity.Owin;
﻿using JobApplicant = EPMS.Models.DomainModels.JobApplicant;

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
                    jobApplicantViewModel.JobApplicant.RecLastUpdatedBy = User.Identity.GetUserId();
                    jobApplicantViewModel.JobApplicant.RecLastUpdatedDt = DateTime.Now;
                    JobApplicant modelToSave = jobApplicantViewModel.JobApplicant.CreateFrom();
                    modelToSave.JobOfferedId = jobApplicantViewModel.JobOffered.JobOfferedId;

                    if (jobApplicantService.AddJobApplicant(modelToSave))
                    {
                        TempData["message"] = new MessageViewModel { Message = "Job Applicant has been submitted.", IsSaved = true };
                        jobApplicantViewModel.JobOffered.JobOfferedId = modelToSave.JobOfferedId;
                        return RedirectToAction("Jobs");
                    }
                }

                #endregion

            }
            catch (Exception e)
            {
                TempData["message"] = new MessageViewModel { Message = e.Message, IsError = true };
                return RedirectToAction("Apply", e);
            }

            return View(jobApplicantViewModel);
        }

        public ActionResult JobApplicantList()
        {
            JobApplicantSearchRequest jobApplicantSearchRequest = new JobApplicantSearchRequest();
            JobApplicantListViewModel jobApplicantListViewModel = new JobApplicantListViewModel
            {
                SearchRequest = jobApplicantSearchRequest
            };
            return View(jobApplicantListViewModel);
        }
        
        [HttpPost]
        public ActionResult JobApplicantList(JQueryDataTableParamModel param)
        {
            
            JobApplicantSearchRequest jobApplicantSearchRequest = new JobApplicantSearchRequest();
            jobApplicantSearchRequest.SearchString = Request["search"];
            var jobApplicants = jobApplicantService.GetJobApplicantList(jobApplicantSearchRequest);

            List<ApplicantModel> jobApplicantList =
                jobApplicants.JobApplicants.Select(x => x.CreateFromApplicant()).ToList();

            JobApplicantListViewModel jobApplicantListViewModel = new JobApplicantListViewModel
            {
                aaData = jobApplicantList,
                iTotalRecords = Convert.ToInt32(jobApplicants.TotalCount),
                iTotalDisplayRecords = Convert.ToInt32(jobApplicantList.Count()),
                sEcho = param.sEcho,
            };
            return Json(jobApplicantListViewModel, JsonRequestBehavior.AllowGet);
        }

        public ActionResult UploadUserPhoto()
        {
            HttpPostedFileBase userCv = Request.Files[0];
            try
            {
                //Save File to Folder
                if ((userCv != null))
                {
                    var filename = (DateTime.Now.ToString(CultureInfo.InvariantCulture).Replace(".", "") + userCv.FileName).Replace("/", "").Replace("-", "").Replace(":", "").Replace(" ", "").Replace("+", "");
                    var filePathOriginal = Server.MapPath(ConfigurationManager.AppSettings["ApplicantCv"]);
                    string savedFileName = Path.Combine(filePathOriginal, filename);
                    userCv.SaveAs(savedFileName);
                }
            }
            catch (Exception exp)
            {
                return Json(new { response = "Failed to upload. Error: " + exp.Message, status = (int)HttpStatusCode.BadRequest }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { filename = userCv.FileName, size = userCv.ContentLength / 1024 + "KB", response = "Successfully uploaded!", status = (int)HttpStatusCode.OK }, JsonRequestBehavior.AllowGet);
        }

        #endregion
    }
}