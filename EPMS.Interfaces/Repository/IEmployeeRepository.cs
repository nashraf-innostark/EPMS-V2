using System;
using System.Collections.Generic;
using System.Linq;
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
        IEnumerable<Employee> GetRecentEmployees(string requester);

        Employee FindForPayroll(long id, DateTime currTime);
        IQueryable<string> FindEmployeeEmailById(List<long> employeeId);
        
    }
}
