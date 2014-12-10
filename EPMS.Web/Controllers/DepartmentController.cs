using System.Linq;
using System.Web.Mvc;
using EPMS.Interfaces.IServices;
using EPMS.Web.ModelMappers;
using EPMS.Web.ViewModels.Common;
using EPMS.Web.ViewModels.Department;

namespace EPMS.Web.Controllers
{
    /// <summary>
    /// Controller for Department
    /// </summary>
    public class DepartmentController : BaseController
    {
        private readonly IDepartmentService oDepartmentService;

        /// <summary>
        /// Cosntructor
        /// </summary>
        /// <param name="oDepartmentService"></param>
        #region Constructor

        public DepartmentController(IDepartmentService oDepartmentService)
        {
            this.oDepartmentService = oDepartmentService;
        }

        #endregion


        // GET: Departments ListView Action Method
        public ActionResult DepartmentLV()
        {
            ViewBag.MessageVM = TempData["MessageVm"] as MessageViewModel;

            return View(new DepartmentViewModel
            {
                DepartmentList = oDepartmentService.LoadAll().Select(x=>x.CreateFrom())
            });
        }
    }
}