using System;
using System.Collections.Generic;
using EPMS.Interfaces.IServices;
using EPMS.Interfaces.Repository;
using EPMS.Models.DomainModels;
using EPMS.Models.RequestModels;
using EPMS.Models.ResponseModels;

namespace EPMS.Implementation.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository repository;
        private readonly IEmployeeRequestRepository RequestRepository;


        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="xRepository"></param>
        public EmployeeService(IEmployeeRepository xRepository, IEmployeeRequestRepository requestRepository)
        {
            repository = xRepository;
            RequestRepository = requestRepository;
        }

        #endregion

        public EmployeeResponse GetAllEmployees(EmployeeSearchRequset employeeSearchRequset)
        {
            return repository.GetAllEmployees(employeeSearchRequset);
        }

        public Employee FindEmployeeById(long? id)
        {
            if (id != null)
            {
                return repository.Find((long) id);
            }
            return null;
        }
        public PayrollResponse FindEmployeeForPayroll(long? id, DateTime currTime)
        {
            PayrollResponse response = new PayrollResponse();
            if (id != null)
            {
                response.Employee = repository.FindForPayroll((long)id, currTime);
                response.Allowance = repository.FindForAllownce((long) id, currTime);
                response.Requests = RequestRepository.GetAllMonetaryRequests(currTime, (long)id);
                return response;
            }
            return null;
        }

        public IEnumerable<Employee> GetAll()
        {
            return repository.GetAll();
        }

        /// <summary>
        /// Add Employee to DB
        /// </summary>
        /// <param name="employee"></param>
        /// <returns>EmployeeId</returns>
        public long AddEmployee(Employee employee)
        {
            repository.Add(employee);
            repository.SaveChanges();
            return employee.EmployeeId;
        }
        /// <summary>
        /// Update Employee
        /// </summary>
        /// <param name="employee">Employee Model</param>
        /// <returns></returns>
        public bool UpdateEmployee(Employee employee)
        {
            try
            {
                repository.Update(employee);
                repository.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        /// <summary>
        /// Delete Employee from DB
        /// </summary>
        /// <param name="employee">Employee to be Deleted</param>
        public void DeleteEmployee(Employee employee)
        {
            try
            {
                repository.Delete(employee);
                repository.SaveChanges();
            }
            catch (Exception)
            {   
                throw;
            }
        }
    }
}
