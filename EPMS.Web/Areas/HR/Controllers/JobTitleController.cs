using System;
using System.Linq;
using System.Web.Mvc;
using EPMS.Interfaces.IServices;
using EPMS.Web.Controllers;
using EPMS.Web.ModelMappers;
using EPMS.Web.ViewModels.JobTitle;

namespace EPMS.Web.Areas.HR.Controllers
{
    public class JobTitleController : BaseController
    {
        #region Private

        private readonly IJobTitleService jobTitleService;
        private readonly IDepartmentService departmentService;

        #endregion
        
        #region Constructor

        /// <summary>
        /// Constructor 
        /// </summary>
        /// <param name="departmentService"></param>
        /// <param name="jobTitleService"></param>
        public JobTitleController(IDepartmentService departmentService, IJobTitleService jobTitleService)
        {
            this.departmentService = departmentService;
            this.jobTitleService = jobTitleService;
        }

        #endregion

        #region Public

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

        #endregion
    }
}