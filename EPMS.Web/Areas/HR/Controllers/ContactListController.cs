using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using EPMS.Interfaces.IServices;
using EPMS.Models.ResponseModels;
using EPMS.Web.ModelMappers;
using EPMS.Web.ViewModels.Employee;
using EPMS.WebBase.Mvc;
using ContactList = EPMS.Web.Models.ContactList;

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
            ContactListResponse response = customerService.GetContactListResponse();
            var empList = response.Employees.Where(x =>  !string.IsNullOrEmpty(x.Email) || !string.IsNullOrEmpty(x.EmployeeMobileNum)).Select(x => x.CreateForContactList());
            var customerList = response.Customers.Select(x => x.CreateForContactList());
            var applicantList = response.JobApplicants.Where(x => !string.IsNullOrEmpty(x.Email) || !string.IsNullOrEmpty(x.MobileNumber)).Select(x => x.CreateForContactList());
            contactList.AddRange(empList);
            contactList.AddRange(customerList);
            contactList.AddRange(applicantList);
            return View(contactList);
        }

        #endregion
    }
}