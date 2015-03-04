using System.Collections.Generic;
using EPMS.Models.DomainModels;

namespace EPMS.Interfaces.IServices
{
    public interface IAspNetUserService
    {
        AspNetUser FindById(string id);
        IEnumerable<AspNetUser> GetAllUsers();
        bool UpdateUser(AspNetUser user);
        string GetUserIdByEmployeeId(long employeeId);
        string GetUserIdByCustomerId(long customerId);
    }
}
