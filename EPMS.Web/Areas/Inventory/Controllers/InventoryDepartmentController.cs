using System.Linq;
using System.Web.Mvc;
using EPMS.Interfaces.IServices;
using EPMS.Models.RequestModels;
using EPMS.Web.Controllers;
using EPMS.Web.ViewModels.Common;
using EPMS.Web.ViewModels.InventoryDepartment;
using EPMS.Web.ModelMappers;

namespace EPMS.Web.Areas.Inventory.Controllers
{
    public class InventoryDepartmentController : BaseController
    {
        #region Private

        private readonly IInventoryDepartmentService departmentService;

        #endregion

        #region Constructor

        public InventoryDepartmentController(IInventoryDepartmentService departmentService)
        {
            this.departmentService = departmentService;
        }

        #endregion

        #region Public

        #region Index
        public ActionResult Index()
        {
            return View(new InventoryDepartmentViewModel
            {
                InventoryDepartments = departmentService.GetAll().Select(x=> x.CreateFromServerToClient())
            });
        }
        #endregion

        #region Create

        public ActionResult Create(long? id)
        {
            InventoryDepartmentViewModel departmentViewModel = new InventoryDepartmentViewModel();
            if (id != null)
            {
                departmentViewModel.InventoryDepartment = departmentService.FindInventoryDepartmentById((long)id).CreateFromServerToClient();
            }
            return View(departmentViewModel);
        }

        [HttpPost]
        public ActionResult Create(InventoryDepartmentViewModel departmentViewModel)
        {
            InventoryDepartmentRequest departmentToSave =
                departmentViewModel.InventoryDepartment.CreateFromClientToServer();
            departmentService.SaveDepartment(departmentToSave);
            {
                TempData["message"] = new MessageViewModel { Message = "Added", IsSaved = true };
                return RedirectToAction("Index");
            }
        }


        #endregion


        #endregion
    }
}