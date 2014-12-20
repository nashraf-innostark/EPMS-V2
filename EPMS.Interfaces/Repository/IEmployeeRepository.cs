using EPMS.Models.DomainModels;
using EPMS.Models.RequestModels;
using EPMS.Models.ResponseModels;

namespace EPMS.Interfaces.Repository
{
    public interface IEmployeeRepository : IBaseRepository<Employee, long>
    {
        EmployeeResponse GetAllEmployees(EmployeeSearchRequset employeeSearchRequset);

    }
}
