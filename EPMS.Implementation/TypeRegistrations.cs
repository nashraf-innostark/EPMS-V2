using EPMS.Implementation.Identity;
using EPMS.Implementation.Services;
using EPMS.Interfaces.IServices;
using EPMS.Models.IdentityModels;
using EPMS.Web;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Practices.Unity;

namespace EPMS.Implementation
{
    public static class TypeRegistrations
    {
        public static void RegisterType(IUnityContainer unityContainer)
        {
            UnityConfig.UnityContainer = unityContainer;
            Repository.TypeRegistrations.RegisterType(unityContainer);
            unityContainer.RegisterType<IMenuRightsService, MenuRightsService>();
            unityContainer.RegisterType<ILogger, LoggerService>();
            unityContainer.RegisterType<IMenuRightsService, MenuRightsService>();
            unityContainer.RegisterType<IEmployeeService, EmployeeService>();
            unityContainer.RegisterType<IJobTitleService, JobTitleService>();
            unityContainer.RegisterType<IDepartmentService, DepartmentService>();
            unityContainer.RegisterType<IEmployeeRequestService, EmployeeRequestService>();
            unityContainer.RegisterType<IAllowanceService, AllowanceService>();
            unityContainer.RegisterType<IAspNetUserService, AspNetUserService>();
            unityContainer.RegisterType<IJobOfferedService, JobOfferedService>();
            unityContainer.RegisterType<IUserStore<ApplicationUser>, UserStore<ApplicationUser>>();
            unityContainer.RegisterType<IJobApplicantService, JobApplicantService>();
            unityContainer.RegisterType<IComplaintService, ComplaintService>();
            unityContainer.RegisterType<ICustomerService, CustomerService>();
            unityContainer.RegisterType<IOrdersService, OrdersService>();
            unityContainer.RegisterType<IQuotationService, QuotationService>();
            unityContainer.RegisterType<IQuotationItemService, QuotationItemService>();
            unityContainer.RegisterType<ICompanyProfileService, CompanyProfileService>();
            unityContainer.RegisterType<IPayrollService, PayrollService>();
            unityContainer.RegisterType<ICompanyDocumentService, CompanyDocumentService>();
            unityContainer.RegisterType<ICompanyBankService, CompanyBankService>();
            unityContainer.RegisterType<ICompanySocialService, CompanySocialService>();
            unityContainer.RegisterType<IProjectService, ProjectService>();
            unityContainer.RegisterType<IProjectDocumentService, ProjectDocumentService>();
            unityContainer.RegisterType<IProjectTaskService, ProjectTaskService>();
            unityContainer.RegisterType<IMeetingService, MeetingService>();
            unityContainer.RegisterType<IMeetingAttendeeService, MeetingAttendeeService>();
            unityContainer.RegisterType<IMeetingDocumentService, MeetingDocumentService>();
            unityContainer.RegisterType<ITaskEmployeeService, TaskEmployeeService>();
        }
    }
}
