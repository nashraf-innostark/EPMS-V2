using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using EPMS.Interfaces.IServices;
using EPMS.Models.RequestModels;
using EPMS.Web.ModelMappers;
using EPMS.Web.Models;
using EPMS.Web.ViewModels.Common;
using EPMS.Web.ViewModels.JobTitle;

namespace EPMS.Web.Controllers
{
    /// <summary>
    /// TODO: Comments
    /// </summary>
    [Authorize]
    public class JobTitleController : BaseController
    {

        private readonly IJobTitleService jobTitleService;
        private readonly IDepartmentService oDepartmentService;


        #region Constructor

        public JobTitleController(IDepartmentService oDepartmentService, IJobTitleService jobTitleService)
        {
            this.oDepartmentService = oDepartmentService;
            this.jobTitleService = jobTitleService;
        }

        #endregion

        

        // GET: JobTitle
        public ActionResult JobTitleLV()
        {
            JobTitleSearchRequest jobTitleSearchRequest = Session["PageMetaData"] as JobTitleSearchRequest;

            Session["PageMetaData"] = null;

            ViewBag.MessageVM = TempData["MessageVm"] as MessageViewModel;

            IEnumerable<JobTitle> jobList = jobTitleService.LoadAll().Select(x => x.CreateFrom());

            return View(new JobTitleViewModel
            {
                DepartmentList = oDepartmentService.LoadAll().Select(x=>x.CreateFrom()),
                JobTitleList = jobList,
                SearchRequest = jobTitleSearchRequest ?? new JobTitleSearchRequest()
            });
        }

        public ActionResult ComboBox()
        {
            return View();
        }
    }
}