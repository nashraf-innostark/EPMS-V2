using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using EPMS.Interfaces.IServices;
using EPMS.Web.ModelMappers;
using EPMS.Web.Models;
using EPMS.Web.ViewModels.Employee;
using EPMS.WebBase.Mvc;

namespace EPMS.Web.Areas.HR.Controllers
{
    [Authorize]
    [SiteAuthorize(PermissionKey = "CL", IsModule = true)]
    public class ContactListController : Controller
    {
        #region Private

        private readonly IEmployeeService employeeService;
        private readonly ICustomerService customerService;
        private readonly IJobApplicantService jobApplicantService;

        #endregion
        
        #region Constructor

        public ContactListController(IEmployeeService employeeService, ICustomerService customerService, IJobApplicantService jobApplicantService)
        {
            this.employeeService = employeeService;
            this.customerService = customerService;
            this.jobApplicantService = jobApplicantService;
        }

        #endregion

        #region Public

        [SiteAuthorize(PermissionKey = "CL")]
        public ActionResult Index()
        {
            var contactList = new List<ContactList>();
            var empList = employeeService.GetAll().Where(x =>  !string.IsNullOrEmpty(x.Email) || !string.IsNullOrEmpty(x.EmployeeMobileNum)).Select(x => x.CreateForContactList());
            var customerList = customerService.GetAll().Select(x => x.CreateForContactList());
            var applicantList = jobApplicantService.GetAll().Where(x => !string.IsNullOrEmpty(x.ApplicantEmail) || !string.IsNullOrEmpty(x.ApplicantMobile)).Select(x => x.CreateForContactList());
            contactList.AddRange(empList);
            contactList.AddRange(customerList);
            contactList.AddRange(applicantList);
            return View(contactList);
        }

        #endregion
    }
}