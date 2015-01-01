using System.Linq;
using System.Web.Mvc;
using EPMS.Interfaces.IServices;
using EPMS.Web.Controllers;
using EPMS.Web.ModelMappers;
using EPMS.Web.ViewModels.Common;
using EPMS.Web.ViewModels.Department;
using EPMS.Web.ViewModels.Recruitment;

namespace EPMS.Web.Areas.HR.Controllers
{
    [Authorize]
    public class RecruitmentController : BaseController
    {
        #region Private

        private readonly IRecruitmentService recruitmentService;

        #endregion

        #region Constructor

        public RecruitmentController(IRecruitmentService oService)
        {
            recruitmentService = oService;
        }

        #endregion
        // GET: HR/Recruitment
        public ActionResult Index()
        {
            RecruitmentViewModel viewModel=new RecruitmentViewModel();
            viewModel.JobsOffered = recruitmentService.LoadAllJobs().Select(x => x.CreateFromServerToClient());
            ViewBag.MessageVM = TempData["MessageVm"] as MessageViewModel;
            return View(viewModel);
        }
    }
}