using System.Linq;
using System.Web;
using System.Web.Mvc;
using EPMS.Implementation.Identity;
using EPMS.Interfaces.IServices;
using EPMS.Models.DomainModels;
using EPMS.Models.IdentityModels.ViewModels;
using EPMS.Web.Controllers;
using EPMS.Web.ModelMappers;
using EPMS.Web.ViewModels.Common;
using EPMS.Web.ViewModels.Customer;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;

namespace EPMS.Web.Areas.CMS.Controllers
{
    public class CustomerController : BaseController
    {
        #region Private
        
        private readonly ICustomerService customerService;
        private IAspNetUserService aspNetUserService;

        #endregion

        #region Constructor

        public CustomerController(ICustomerService customerService, IAspNetUserService aspNetUserService)
        {
            this.customerService = customerService;
            this.aspNetUserService = aspNetUserService;
        }

        #endregion

        #region Public

        public ActionResult Index()
        {
            return View(new CustomerViewModel
            {
                CustomerList = customerService.GetAll().Select(x => x.CreateFromServerToClient())
            });
        }

        public ActionResult Details(long id)
        {
            CustomerViewModel customerViewModel = new CustomerViewModel();
                customerViewModel = customerService.FindCustomerById(id).CreateFromServerToClientVM();
            return View(customerViewModel);
        }
        [HttpPost]

        public ActionResult Details(CustomerViewModel customerViewModel)
        {
            AspNetUser result = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(customerViewModel.User.Id);
            var customerToUpdate = customerViewModel.Customer.CreateFrom();
            if (customerService.UpdateCustomer(customerToUpdate))
            {
                TempData["message"] = new MessageViewModel { Message = "Customer Updated", IsUpdated = true };
                result.Email = customerViewModel.User.Email;
                aspNetUserService.UpdateUser(result);
                return RedirectToAction("Index");
            }
            return View(customerViewModel);
            
        }
        
        #endregion
    }
}