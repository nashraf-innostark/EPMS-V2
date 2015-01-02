﻿using System;
using System.Collections.Generic;
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
        EmployeeResponse GetAllEmployees(EmployeeSearchRequset employeeSearchRequset);
        Employee FindEmployeeById(long? id);
        PayrollResponse FindEmployeeForPayroll(long? id, DateTime currTime);

        IEnumerable<Employee> GetAll(); 
        long AddEmployee(Employee employee);
        bool UpdateEmployee(Employee employee);
        void DeleteEmployee(Employee employee);
    }
}
