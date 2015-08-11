﻿using System;
﻿using System.Collections.Generic;
﻿using System.Configuration;
﻿using System.Globalization;
﻿using System.IO;
﻿using System.Linq;
﻿using System.Net;
﻿using System.Web;
﻿using System.Web.Mvc;
﻿using EPMS.Interfaces.IServices;
﻿using EPMS.Models.RequestModels;
﻿using EPMS.Models.ResponseModels;
﻿using EPMS.Web.Controllers;
﻿using EPMS.WebBase.Mvc;
﻿using EPMS.WebModels.ModelMappers;
﻿using EPMS.WebModels.ViewModels.Common;
﻿using EPMS.WebModels.ViewModels.JobApplicant;
﻿using EPMS.WebModels.WebsiteModels;
﻿using Microsoft.AspNet.Identity;
﻿using JobApplicant = EPMS.Models.DomainModels.JobApplicant;

namespace EPMS.Web.Areas.HR.Controllers
{
    public class JobApplicantController : BaseController
    {
        #region Private

        private readonly IJobOfferedService jobOfferedService;
        private readonly IJobTitleService jobTitleService;
        private readonly IJobApplicantService jobApplicantService;
        private readonly IDepartmentService departmentService;

        #endregion

        #region Constructor

        public JobApplicantController(IJobOfferedService jobOfferedService, IJobTitleService jobTitleService, IJobApplicantService jobApplicantService, IDepartmentService departmentService)
        {
            this.jobOfferedService = jobOfferedService;
            this.jobTitleService = jobTitleService;
            this.jobApplicantService = jobApplicantService;
            this.departmentService = departmentService;
        }

        #endregion

        #region Public

        [AllowAnonymous]
        public ActionResult Jobs()
        {
            JobApplicantViewModel viewModel = new JobApplicantViewModel
            {
                JobOfferedList = jobOfferedService.GetAll().Select(x => x.CreateFromServerToClientForJobs())
            };
            ViewBag.ApplyMessage = TempData["Messages"];
            ViewBag.MessageVM = null;
            return View(viewModel);
        }
        [AllowAnonymous]
        public ActionResult Apply(long? id)
        {
            JobApplicantViewModel jobApplicantViewModel = new JobApplicantViewModel();
            if (id == null)
            {
                return RedirectToAction("Jobs");
            }
            JobApplicantResponse response = jobOfferedService.GetJobOfferedResponse((long)id);
            jobApplicantViewModel.Departments = response.Departments.Select(x => x.CreateFrom());
            jobApplicantViewModel.JobApplicant.JobOfferedId = (long)id;
            return View(jobApplicantViewModel);
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateInput(false)]
        public ActionResult Apply(JobApplicantViewModel jobApplicantViewModel)
        {
            try
            {

                #region Add
                {
                    jobApplicantViewModel.JobApplicant.CreatedBy = User.Identity.GetUserId();
                    jobApplicantViewModel.JobApplicant.CreatedDate = DateTime.Now;
                    jobApplicantViewModel.JobApplicant.LastUpdatedBy = User.Identity.GetUserId();
                    jobApplicantViewModel.JobApplicant.LastUpdatedDate = DateTime.Now;
                    foreach (var applicantQualification in jobApplicantViewModel.Qualifications.ToList())
                    {
                        if (!IsNotNullOrEmptyQualification(applicantQualification))
                        {
                            jobApplicantViewModel.Qualifications.Remove(applicantQualification);
                        }
                        applicantQualification.CreatedBy = User.Identity.GetUserId();
                        applicantQualification.CreatedDate = DateTime.Now;
                        applicantQualification.LastUpdatedBy = User.Identity.GetUserId();
                        applicantQualification.LastUpdatedDate = DateTime.Now;
                    }
                    foreach (var applicantExperience in jobApplicantViewModel.Experiences.ToList())
                    {
                        if (!IsNotNullOrEmptyExperience(applicantExperience))
                        {
                            jobApplicantViewModel.Experiences.Remove(applicantExperience);
                        }
                        applicantExperience.CreatedBy = User.Identity.GetUserId();
                        applicantExperience.CreatedDate = DateTime.Now;
                        applicantExperience.LastUpdatedBy = User.Identity.GetUserId();
                        applicantExperience.LastUpdatedDate = DateTime.Now;
                    }
                    JobApplicant modelToSave = jobApplicantViewModel.JobApplicant.CreateFrom(jobApplicantViewModel.Qualifications.ToList(), jobApplicantViewModel.Experiences.ToList());
                    if (jobApplicantService.AddJobApplicant(modelToSave))
                    {
                        ViewBag.MessageVM = new MessageViewModel { Message = EPMS.WebModels.Resources.HR.JobApplicant.SuccessJobApplicant, IsSaved = true };
                        jobApplicantViewModel.Departments = departmentService.GetAll().Select(x => x.CreateFrom());
                        jobApplicantViewModel.JobApplicant.AcceptAgreement = false;
                        return View(jobApplicantViewModel);
                    }
                }

                #endregion

            }
            catch (Exception e)
            {
                ViewBag.MessageVM = new MessageViewModel { Message = e.Message, IsError = true };
                return View(jobApplicantViewModel);
            }
            jobApplicantViewModel.Departments = departmentService.GetAll().Select(x => x.CreateFrom());
            ViewBag.MessageVM = new MessageViewModel { Message = EPMS.WebModels.Resources.HR.JobApplicant.ErrorJobApplicant, IsError = true };
            return View(jobApplicantViewModel);
        }
        public ActionResult UploadCv()
        {
            HttpPostedFileBase userCv = Request.Files[0];
            var filename = "";
            try
            {
                //Save File to Folder
                if ((userCv != null))
                {
                    filename = (DateTime.Now.ToString(CultureInfo.InvariantCulture).Replace(".", "") + userCv.FileName).Replace("/", "").Replace("-", "").Replace(":", "").Replace(" ", "").Replace("+", "");
                    var filePathOriginal = Server.MapPath(ConfigurationManager.AppSettings["ApplicantCv"]);
                    string savedFileName = Path.Combine(filePathOriginal, filename);
                    userCv.SaveAs(savedFileName);
                }
            }
            catch (Exception exp)
            {
                return Json(new { response = "Failed to upload. Error: " + exp.Message, status = (int)HttpStatusCode.BadRequest }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { filename = filename, size = userCv.ContentLength / 1024 + "KB", response = "Successfully uploaded!", status = (int)HttpStatusCode.OK }, JsonRequestBehavior.AllowGet);
        }
        [SiteAuthorize(PermissionKey = "JobApplicant")]
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
        public ActionResult JobApplicantList(JobApplicantSearchRequest jobApplicantSearchRequest)
        {
            jobApplicantSearchRequest.SearchString = Request["search"];
            var jobApplicants = jobApplicantService.GetJobApplicants(jobApplicantSearchRequest);

            List<ApplicantModel> jobApplicantList =
                jobApplicants.JobApplicants.Select(x => x.CreateFromApplicant()).ToList();

            JobApplicantListViewModel jobApplicantListViewModel = new JobApplicantListViewModel
            {
                aaData = jobApplicantList,
                iTotalRecords = Convert.ToInt32(jobApplicants.TotalCount),
                iTotalDisplayRecords = Convert.ToInt32(jobApplicantList.Count()),
                sEcho = jobApplicantSearchRequest.sEcho,
            };
            return Json(jobApplicantListViewModel, JsonRequestBehavior.AllowGet);
        }
        [SiteAuthorize(PermissionKey = "ApplicantDetail")]
        public ActionResult ApplicantDetail(long id)
        {
            JobApplicantViewModel jobApplicantViewModel = new JobApplicantViewModel();
            if (id > 0)
            {
                var applicant = jobApplicantService.FindJobApplicantById(id);
                if (applicant != null)
                {
                    jobApplicantViewModel.Departments = departmentService.GetAll().Select(x => x.CreateFrom());
                    jobApplicantViewModel.JobApplicant = applicant.CreateJobApplicant();
                    jobApplicantViewModel.Qualifications =
                        applicant.ApplicantQualifications.Select(x => x.CreateFromServerToClient()).ToList();
                    jobApplicantViewModel.Experiences =
                        applicant.ApplicantExperiences.Select(x => x.CreateFromServerToClient()).ToList();
                    return View(jobApplicantViewModel);
                }
            }
            return RedirectToAction("JobApplicantList");

        }
        public FileResult Download(string fileName)
        {
            byte[] fileBytes = System.IO.File.ReadAllBytes(Server.MapPath(ConfigurationManager.AppSettings["ApplicantCv"] + fileName));
            return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, fileName);
        }
        public bool IsNotNullOrEmptyQualification(ApplicantQualification qualification)
        {
            if (!string.IsNullOrEmpty(qualification.Certificate) || !string.IsNullOrEmpty(qualification.Field) ||
                !string.IsNullOrEmpty(qualification.PlaceOfStudy) ||
                !string.IsNullOrEmpty(qualification.CollegeSchoolName) || !string.IsNullOrEmpty(qualification.NoOfYears) ||
                !string.IsNullOrEmpty(qualification.Notes))
            {
                return true;
            }
            return false;
        }

        public bool IsNotNullOrEmptyExperience(ApplicantExperience experience)
        {
            if (!string.IsNullOrEmpty(experience.CompanyName) || !string.IsNullOrEmpty(experience.JobTitle) ||
                !string.IsNullOrEmpty(experience.Position) || experience.Salary != null ||
                !string.IsNullOrEmpty(experience.TypeOfWork) || !string.IsNullOrEmpty(experience.ReasonToLeave))
            {
                return true;
            }
            return false;
        }
        #endregion
    }
}