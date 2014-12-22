using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EPMS.Models.DomainModels;
using EPMS.Models.RequestModels;
using EPMS.Models.ResponseModels;

namespace EPMS.Interfaces.Repository
{
    public interface IEmployeeRepository : IBaseRepository<Employee, long>
    {
        EmployeeResponse GetAllEmployees(EmployeeSearchRequset employeeSearchRequset);

        /// <summary>
        /// Get All Employees by by Department Id
        /// </summary>
        IEnumerable<Employee> GetEmployeesByDepartmentId(long departmentId);
    }
}
