using System.Linq;
using System.Web.Mvc;
using EPMS.Interfaces.IServices;
using EPMS.Web.Controllers;
using EPMS.Web.ModelMappers;
using EPMS.Web.ViewModels.JobOffered;
using EPMS.Web.ViewModels.JobTitle;

namespace EPMS.Web.Areas.HR.Controllers
{
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
        public ActionResult Index()
        {
            return View(new JobOfferedViewModel
            {
                JobTitleList = jobTitleService.GetAll().Select(x => x.CreateFrom()),
                JobOfferedList = jobOfferedService.GetAll().Select(x => x.CreateFrom())
            });
        }

        public ActionResult Create(long? id)
        {
            JobOfferedViewModel jobOfferedViewModel = new JobOfferedViewModel();
            if (id != null)
            {
                jobOfferedViewModel.JobTitle = jobTitleService.FindJobTitleById((long)id).CreateFrom();
            }
            jobOfferedViewModel.JobTitleList = jobTitleService.GetAll().Select(x => x.CreateFrom());
            return View(jobOfferedViewModel);
        }

        [HttpPost]
        public ActionResult Create(JobOfferedViewModel jobOfferedViewModel)
        {
            return View(jobOfferedViewModel);
        }

        #endregion
    }
}