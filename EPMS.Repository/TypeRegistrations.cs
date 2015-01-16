using System.Data.Entity;
using EPMS.Interfaces.Repository;
using EPMS.Repository.BaseRepository;
using EPMS.Repository.Repositories;
using EPMS.Web.Views.RolesAdmin;
using Microsoft.Practices.Unity;

namespace EPMS.Repository
{
    public static class TypeRegistrations
    {
        public static void RegisterType(IUnityContainer unityContainer)
        {
            unityContainer.RegisterType<IMenuRightRepository, MenuRightRepository>();
            unityContainer.RegisterType<IMenuRepository, MenuRepository>();
            unityContainer.RegisterType<IEmployeeRepository, EmployeeRepository>();
            unityContainer.RegisterType<IJobTitleRepository, JobTitleRepository>();
            unityContainer.RegisterType<IDepartmentRepository, DepartmentRepository>();
            unityContainer.RegisterType<IEmployeeRequestRepository, EmployeeRequestRepository>();
            unityContainer.RegisterType<IAllowanceRepository, AllowanceRepository>();
            unityContainer.RegisterType<IAspNetUserRepository, AspNetUserRepository>();
            unityContainer.RegisterType<IEmployeeRequestDetailRepository, EmployeeRequestDetailRepository>();
            unityContainer.RegisterType<DbContext, BaseDbContext>(new PerRequestLifetimeManager());
            unityContainer.RegisterType<IJobOfferedRepository, JobOfferedRepository>();
            unityContainer.RegisterType<IJobApplicantRepository, JobApplicantRepository>();
            unityContainer.RegisterType<IEmployeeJobHistoryRepository, EmployeeJobHistoryRepository>();
            unityContainer.RegisterType<IJobTitleHistoryRepository, JobTitleHistoryRepository>();
            unityContainer.RegisterType<IComplaintRepository, ComplaintRepository>();
            unityContainer.RegisterType<ICustomerRepository, CustomerRepository>();
            unityContainer.RegisterType<IOrdersRepository, OrdersRepository>();
            unityContainer.RegisterType<IQuotationRepository, QuotationRepository>();
            unityContainer.RegisterType<IQuotationItemRepository, QuotationItemRepository>();
            unityContainer.RegisterType<ICompanyProfileRepository, CompanyProfileRepository>();
            unityContainer.RegisterType<ICompanyBankRepository, CompanyBankRepository>();
            unityContainer.RegisterType<ICompanyDocumentRepository, CompanyDocumentRepository>();
            unityContainer.RegisterType<ICompanySocialRepository, CompanySocialRepository>();
        }
    }
}
