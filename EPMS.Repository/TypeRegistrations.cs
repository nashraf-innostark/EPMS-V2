using System.Data.Common;
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
            unityContainer.RegisterType<DbContext, BaseDbContext>(new PerRequestLifetimeManager());
        }
    }
}
