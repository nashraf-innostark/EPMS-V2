using EPMS.Models.DomainModels;

namespace EPMS.Web.ModelMappers
{
    public static class UserMapper
    {
        public static void UpdateUserTo(this AspNetUser target, AspNetUser source)
        {
            target.Email = source.Email;
            target.EmailConfirmed = source.EmailConfirmed;
            target.EmployeeId = source.EmployeeId;
        }
    }
}