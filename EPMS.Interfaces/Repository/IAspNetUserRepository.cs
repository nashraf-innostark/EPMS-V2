using EPMS.Models.DomainModels;

namespace EPMS.Interfaces.Repository
{
    public interface IAspNetUserRepository : IBaseRepository<AspNetUser, string>
    {
        string GetUserIdByEmployeeId(long employeeId);
        string GetUserIdByCustomerId(long customerId);
    }
}
