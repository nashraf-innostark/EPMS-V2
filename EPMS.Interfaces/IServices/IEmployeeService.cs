using EPMS.Models.DomainModels;
using EPMS.Models.RequestModels;
using EPMS.Models.ResponseModels;

namespace EPMS.Interfaces.IServices
{
    /// <summary>
    /// Employee Interface for Service
    /// </summary>
    public interface IEmployeeService
    {
        bool AddEmployee(Employee employee);
        EmployeeResponse GetAllEmployees(EmployeeSearchRequset employeeSearchRequset);

        Employee FindEmployeeById(long? id);
        bool UpdateEmployee(Employee employee);
        void DeleteEmployee(Employee employee);
    }
}
