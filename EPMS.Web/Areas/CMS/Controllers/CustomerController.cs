using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using EPMS.Implementation.Identity;
using EPMS.Interfaces.IServices;
using EPMS.Models.DomainModels;
using EPMS.Models.IdentityModels.ViewModels;
using EPMS.Web.Controllers;
using EPMS.Web.ModelMappers;
using EPMS.Web.ViewModels.Common;
using EPMS.Web.ViewModels.Customer;
using EPMS.WebBase.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;

namespace EPMS.Web.Areas.CMS.Controllers
{
    [Authorize]
    [SiteAuthorize(PermissionKey = "CS", IsModule = true)]
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
        [SiteAuthorize(PermissionKey = "CustomerIndex")]
        public ActionResult Index()
        {
            return View(new CustomerViewModel
            {
                CustomerList = customerService.GetAll().Select(x => x.CreateFromServerToClient())
            });
        }

        [SiteAuthorize(PermissionKey = "CustomerProfile")]
        public ActionResult Details(long? id)
        {
            CustomerViewModel customerViewModel = new CustomerViewModel();
            if (id == null)
            {
                if (Session["RoleName"].ToString() != "Customer")
                {
                    return RedirectToAction("Index", "Customer");
                }
                id = Convert.ToInt64(Session["CustomerID"].ToString());
            }
            if (Session["RoleName"].ToString() == "Admin" || id == Convert.ToInt64(Session["CustomerID"].ToString()))
            {
                customerViewModel = customerService.FindCustomerById((long)id).CreateFromServerToClientVM();
                ViewBag.UserRole = Session["RoleName"].ToString();
                ViewBag.ReturnUrl = Request.UrlReferrer;
                return View(customerViewModel);
            }
            return RedirectToAction("Index", "Dashboard", new {area = ""});
            
        }
        
        [HttpPost]
        [SiteAuthorize(PermissionKey = "CustomerProfile")]
        public ActionResult Details(CustomerViewModel customerViewModel)
        {
            AspNetUser result = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(customerViewModel.User.Id);
            var customerToUpdate = customerViewModel.Customer.CreateFrom();
            if (customerService.UpdateCustomer(customerToUpdate))
            {
                TempData["message"] = new MessageViewModel { Message = "Customer Updated", IsUpdated = true };
                result.Email = customerViewModel.User.Email;
                aspNetUserService.UpdateUser(result);
                return RedirectToAction("Details");
            }
            return View(customerViewModel);
            
        }
        
        #endregion
    }
}