using System.Linq;
using System.Web.Mvc;
using EPMS.Interfaces.IServices;
using EPMS.Models.DomainModels;
using EPMS.Models.IdentityModels.ViewModels;
using EPMS.Web.Controllers;
using EPMS.Web.ModelMappers;
using EPMS.Web.ViewModels.Customer;
using Microsoft.AspNet.Identity;

namespace EPMS.Web.Areas.CMS.Controllers
{
    public class CustomerController : BaseController
    {
        #region Private

        private readonly ICustomerService customerService;

        #endregion

        #region Constructor

        public CustomerController(ICustomerService customerService)
        {
            this.customerService = customerService;
        }

        #endregion

        #region Public

        public ActionResult Index()
        {
            return View(new CustomerViewModel
            {
                CustomerList = customerService.GetAll().Select(x => x.CreateFrom())
            });
        }

        #endregion
    }
}