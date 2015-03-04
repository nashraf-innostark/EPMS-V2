﻿using System;
using System.Configuration;
using System.Globalization;
using System.Linq;
using EPMS.Web.Models;
using Employee = EPMS.Models.DomainModels.Employee;

namespace EPMS.Web.ModelMappers
{
    public static class EmployeeMapper
    {
        public static Employee CreateFromClientToServer(this Models.Employee source)
        {
            var caseType = new Employee
            {
                EmployeeId = source.EmployeeId,
                EmployeeFirstNameE = source.EmployeeFirstNameE,
                EmployeeFirstNameA = source.EmployeeFirstNameA,
                EmployeeMiddleNameE = source.EmployeeMiddleNameE,
                EmployeeMiddleNameA = source.EmployeeMiddleNameA,
                EmployeeLastNameE = source.EmployeeLastNameE,
                EmployeeLastNameA = source.EmployeeLastNameA,
                EmployeeImagePath = source.EmployeeImagePath,
                EmployeeIqama = source.EmployeeIqama ?? string.Empty,
                EmployeeIqamaExpiryDt = DateTime.ParseExact(source.EmployeeIqamaExpiryDt, "dd/MM/yyyy", new CultureInfo("en")),
                EmployeeDOB = DateTime.ParseExact(source.EmployeeDOB, "dd/MM/yyyy", new CultureInfo("en")),
                EmployeeLandlineNum = source.EmployeeLandlineNum ?? "",
                MaritalStatus = source.MaritalStatus,
                EmployeeMobileNum = source.EmployeeMobileNum ?? "",
                JobTitleId = source.JobTitleId,
                EmployeeNationality = source.EmployeeNationality,
                EmployeePassportNum = source.EmployeePassportNum ?? "",
                EmployeePassportExpiryDt = DateTime.ParseExact(source.EmployeePassportExpiryDt, "dd/MM/yyyy", new CultureInfo("en")),
                EmployeeDetailsE = source.EmployeeDetailsE,
                EmployeeDetailsA = source.EmployeeDetailsA,
                RecCreatedBy = source.RecCreatedBy ?? "",
                RecCreatedDt = source.RecCreatedDt,
                RecLastUpdatedBy = source.RecLastUpdatedBy ?? "",
                RecLastUpdatedDt = source.RecLastUpdatedDt,
                Email = source.Email,
                EmployeeJobId = source.EmployeeJobId,
                IsActivated = source.IsActivated,
            };
            return caseType;
        }
        public static Models.Employee CreateFromServerToClient(this Employee source)
        {
            return new Models.Employee
            {
                EmployeeId = source.EmployeeId,
                EmployeeFirstNameE = source.EmployeeFirstNameE,
                EmployeeFirstNameA = source.EmployeeFirstNameA,
                EmployeeMiddleNameE = source.EmployeeMiddleNameE,
                EmployeeMiddleNameA = source.EmployeeMiddleNameA,
                EmployeeLastNameE = source.EmployeeLastNameE,
                EmployeeLastNameA = source.EmployeeLastNameA,
                EmployeeImagePath = source.EmployeeImagePath,
                EmployeeIqama = source.EmployeeIqama,
                EmployeeIqamaIssueDt = Convert.ToDateTime(source.EmployeeIqamaIssueDt).ToString("dd/MM/yyyy", new CultureInfo("en")),
                EmployeeIqamaExpiryDt = Convert.ToDateTime(source.EmployeeIqamaExpiryDt).ToString("dd/MM/yyyy", new CultureInfo("en")),
                EmployeeDOB = Convert.ToDateTime(source.EmployeeDOB).ToString("dd/MM/yyyy", new CultureInfo("en")),
                EmployeeLandlineNum = source.EmployeeLandlineNum ?? "",
                MaritalStatus = source.MaritalStatus,
                EmployeeMobileNum = source.EmployeeMobileNum ?? "",
                JobTitleId = source.JobTitleId,
                EmployeeNationality = source.EmployeeNationality,
                EmployeePassportNum = source.EmployeePassportNum ?? "",
                EmployeePassportExpiryDt = Convert.ToDateTime(source.EmployeePassportExpiryDt).ToString("dd/MM/yyyy", new CultureInfo("en")),
                EmployeeDetailsE = source.EmployeeDetailsE,
                EmployeeDetailsA = source.EmployeeDetailsA,
                RecCreatedBy = source.RecCreatedBy ?? "",
                RecCreatedDt = source.RecCreatedDt,
                RecLastUpdatedBy = source.RecLastUpdatedBy ?? "",
                RecLastUpdatedDt = source.RecLastUpdatedDt,
                Email = source.Email,
                IsActivated = source.IsActivated,
                EmployeeJobId = source.EmployeeJobId,
                //EmployeeRequests = source.EmployeeRequests.Select(x=>x.CreateFromServerToClient()),
                Allowances = source.Allowances.Select(x => x.CreateFromServerToClient()),
                JobTitle = (source.JobTitle != null) ? source.JobTitle.CreateFrom() : (new Models.JobTitle()),
                EmployeeFullNameE = source.EmployeeFirstNameE + " " + source.EmployeeMiddleNameE + " " + source.EmployeeLastNameE,
                EmployeeFullNameA = source.EmployeeFirstNameA + " " + source.EmployeeMiddleNameA + " " + source.EmployeeLastNameA,
            };

        }
        public static Models.Employee CreateFromServerToClientForTask(this Employee source)
        {
            Models.Employee employee = new Models.Employee
            {
                EmployeeId = source.EmployeeId,
                EmployeeImagePath = source.EmployeeImagePath,
                EmployeeIqama = source.EmployeeIqama,
                EmployeeIqamaIssueDt = Convert.ToDateTime(source.EmployeeIqamaIssueDt).ToString("dd/MM/yyyy", new CultureInfo("en")),
                EmployeeIqamaExpiryDt = Convert.ToDateTime(source.EmployeeIqamaExpiryDt).ToString("dd/MM/yyyy", new CultureInfo("en")),
                EmployeeDOB = Convert.ToDateTime(source.EmployeeDOB).ToString("dd/MM/yyyy", new CultureInfo("en")),
                EmployeeLandlineNum = source.EmployeeLandlineNum ?? "",
                MaritalStatus = source.MaritalStatus,
                EmployeeMobileNum = source.EmployeeMobileNum ?? "",
                JobTitleId = source.JobTitleId,
                EmployeeNationality = source.EmployeeNationality,
                EmployeePassportNum = source.EmployeePassportNum ?? "",
                EmployeePassportExpiryDt = Convert.ToDateTime(source.EmployeePassportExpiryDt).ToString("dd/MM/yyyy", new CultureInfo("en")),
                EmployeeDetailsE = source.EmployeeDetailsE,
                EmployeeDetailsA = source.EmployeeDetailsA,
                RecCreatedBy = source.RecCreatedBy ?? "",
                RecCreatedDt = source.RecCreatedDt,
                RecLastUpdatedBy = source.RecLastUpdatedBy ?? "",
                RecLastUpdatedDt = source.RecLastUpdatedDt,
                Email = source.Email,
                IsActivated = source.IsActivated,
                EmployeeJobId = source.EmployeeJobId,
                //EmployeeRequests = source.EmployeeRequests.Select(x=>x.CreateFromServerToClient()),
                Allowances = source.Allowances.Select(x => x.CreateFromServerToClient()),
                JobTitle = (source.JobTitle != null) ? source.JobTitle.CreateFrom() : (new Models.JobTitle()),
            };
            var noOfTasks = source.TaskEmployees.Count(x => x.EmployeeId == employee.EmployeeId);
            employee.EmployeeFullNameE = source.EmployeeFirstNameE + " " + source.EmployeeMiddleNameE + " " + source.EmployeeLastNameE + " - " + noOfTasks;
            employee.EmployeeFullNameA = source.EmployeeFirstNameA + " " + source.EmployeeMiddleNameA + " " + source.EmployeeLastNameA + " - " + noOfTasks;
            return employee;
        }
        public static DashboardModels.Employee CreateForDashboard(this Employee source)
        {
            return new DashboardModels.Employee
            {
                EmployeeId = source.EmployeeId,
                EmployeeJobId = source.EmployeeJobId,
                EmployeeNameE = source.EmployeeFirstNameE + " " + source.EmployeeMiddleNameE + " " + source.EmployeeLastNameE,
                EmployeeNameA = source.EmployeeFirstNameA + " " + source.EmployeeMiddleNameA + " " + source.EmployeeLastNameA,
                EmployeeNameEShort = (source.EmployeeFirstNameE + " " + source.EmployeeMiddleNameE + " " + source.EmployeeLastNameE).Length > 14 ? (source.EmployeeFirstNameE + " " + source.EmployeeMiddleNameE + " " + source.EmployeeLastNameE).Substring(0, 14) + "..." : (source.EmployeeFirstNameE + " " + source.EmployeeMiddleNameE + " " + source.EmployeeLastNameE),
                EmployeeNameAShort = (source.EmployeeFirstNameA + " " + source.EmployeeMiddleNameA + " " + source.EmployeeLastNameA).Length > 18 ? (source.EmployeeFirstNameA + " " + source.EmployeeMiddleNameA + " " + source.EmployeeLastNameA).Substring(0, 18) + "..." : (source.EmployeeFirstNameA + " " + source.EmployeeMiddleNameA + " " + source.EmployeeLastNameA),
                EmployeeImagePath = ConfigurationManager.AppSettings["EmployeeImage"] + (string.IsNullOrEmpty(source.EmployeeImagePath) ? "profile.jpg" : source.EmployeeImagePath)
            };

        }
        public static DashboardModels.Profile CreateForDashboardProfile(this Employee source)
        {
            return new DashboardModels.Profile
            {
                EmployeeId = source.EmployeeId,
                EmployeeNameE = source.EmployeeFirstNameE + " " + source.EmployeeMiddleNameE + " " + source.EmployeeLastNameE,
                EmployeeNameA = source.EmployeeFirstNameA + " " + source.EmployeeMiddleNameA + " " + source.EmployeeLastNameA,
                EmployeeNameEShort = (source.EmployeeFirstNameE + " " + source.EmployeeMiddleNameE + " " + source.EmployeeLastNameE).Length > 14 ? (source.EmployeeFirstNameE + " " + source.EmployeeMiddleNameE + " " + source.EmployeeLastNameE).Substring(0, 14) + "..." : (source.EmployeeFirstNameE + " " + source.EmployeeMiddleNameE + " " + source.EmployeeLastNameE),
                EmployeeNameAShort = (source.EmployeeFirstNameA + " " + source.EmployeeMiddleNameA + " " + source.EmployeeLastNameA).Length > 18 ? (source.EmployeeFirstNameA + " " + source.EmployeeMiddleNameA + " " + source.EmployeeLastNameA).Substring(0, 18) + "..." : (source.EmployeeFirstNameA + " " + source.EmployeeMiddleNameA + " " + source.EmployeeLastNameA),
                EmployeeJobId = source.EmployeeJobId,
                EmployeeJobTitleE = source.JobTitle.JobTitleNameE,
                EmployeeJobTitleA = source.JobTitle.JobTitleNameA,
                EmployeeIqamaExpiryDt = Convert.ToDateTime(source.EmployeeIqamaExpiryDt.ToString()).ToShortDateString(),
                EmployeeImagePath = ConfigurationManager.AppSettings["EmployeeImage"] + (string.IsNullOrEmpty(source.EmployeeImagePath) ? "profile.jpg" : source.EmployeeImagePath)
            };
        }
        public static Models.Employee CreateFromServerToClientWithImage(this Employee source)
        {
            return new Models.Employee
            {
                EmployeeId = source.EmployeeId,
                EmployeeFirstNameE = source.EmployeeFirstNameE,
                EmployeeFirstNameA = source.EmployeeFirstNameA,
                EmployeeMiddleNameE = source.EmployeeMiddleNameE,
                EmployeeMiddleNameA = source.EmployeeMiddleNameA,
                EmployeeLastNameE = source.EmployeeLastNameE,
                EmployeeLastNameA = source.EmployeeLastNameA,
                EmployeeImagePath = ImageUrl(source.EmployeeImagePath),
                EmployeeIqama = source.EmployeeIqama,
                EmployeeIqamaIssueDt = Convert.ToDateTime(source.EmployeeIqamaIssueDt).ToString("dd/MM/yyyy", new CultureInfo("en")),
                EmployeeIqamaExpiryDt = Convert.ToDateTime(source.EmployeeIqamaExpiryDt).ToString("dd/MM/yyyy", new CultureInfo("en")),
                EmployeeDOB = Convert.ToDateTime(source.EmployeeDOB).ToString("dd/MM/yyyy", new CultureInfo("en")),
                EmployeeLandlineNum = source.EmployeeLandlineNum ?? "",
                MaritalStatus = source.MaritalStatus,
                EmployeeMobileNum = source.EmployeeMobileNum ?? "",
                JobTitleId = source.JobTitleId,
                EmployeeNationality = source.EmployeeNationality,
                EmployeePassportNum = source.EmployeePassportNum ?? "",
                EmployeePassportExpiryDt = Convert.ToDateTime(source.EmployeePassportExpiryDt).ToString("dd/MM/yyyy", new CultureInfo("en")),
                EmployeeDetailsE = source.EmployeeDetailsE,
                EmployeeDetailsA = source.EmployeeDetailsA,
                RecCreatedBy = source.RecCreatedBy ?? "",
                RecCreatedDt = source.RecCreatedDt,
                RecLastUpdatedBy = source.RecLastUpdatedBy ?? "",
                RecLastUpdatedDt = source.RecLastUpdatedDt,
                Email = source.Email,
                IsActivated = source.IsActivated,
                EmployeeJobId = source.EmployeeJobId,
                JobTitle = (source.JobTitle != null) ? source.JobTitle.CreateFrom() : (new Models.JobTitle()),
                //EmployeeRequests = source.EmployeeRequests.Select(x => x.CreateFromServerToClient()),
                Allowances = source.Allowances.Select(x => x.CreateFromServerToClient()),
                EmployeeFullNameE = source.EmployeeFirstNameE + " " + source.EmployeeMiddleNameE + " " + source.EmployeeLastNameE,
                EmployeeFullNameA = source.EmployeeFirstNameA + " " + source.EmployeeMiddleNameA + " " + source.EmployeeLastNameA,
            };

        }

        public static Models.EmployeeForDropDownList CreateFromServerToClientForDropDownList(this Employee source)
        {
            return new EmployeeForDropDownList
            {
                EmployeeId = source.EmployeeId,
                EmployeeNameE = source.EmployeeFirstNameE + " " + source.EmployeeMiddleNameE + " " + source.EmployeeLastNameE,
                EmployeeNameA = source.EmployeeFirstNameA + " " + source.EmployeeMiddleNameA + " " + source.EmployeeLastNameA,
                Email = source.Email,
            };
        }

        public static Models.ContactList CreateForContactList(this Employee source)
        {
            return new Models.ContactList
            {
                Link = "/HR/Employee/Create/" + source.EmployeeId,
                NameE = source.EmployeeFirstNameE + " " + source.EmployeeMiddleNameE + " " + source.EmployeeLastNameE,
                NameA = source.EmployeeFirstNameA + " " + source.EmployeeMiddleNameA + " " + source.EmployeeLastNameA,
                Type = "Employee",
                MobileNumber = source.EmployeeMobileNum ?? "",
                Email = source.Email,
            };

        }

        private static string ImageUrl(string imageName)
        {
            if (string.IsNullOrEmpty(imageName))
            {
                imageName = "profile.jpg";
            }
            string path = (ConfigurationManager.AppSettings["EmployeeImage"] + imageName);

            return "<img  data-mfp-src=" + path + " src=" + path + " class='mfp-image image-link cursorHand' height=70 width=100 />";
        }
    }
}