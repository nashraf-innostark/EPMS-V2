using System;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Threading;
using EPMS.Models.DomainModels;
using EPMS.Web.Models;
using Employee = EPMS.Models.DomainModels.Employee;

namespace EPMS.Web.ModelMappers
{
    public static class EmployeeMapper
    {
        public static Employee CreateFromClientToServer(this Models.Employee source)
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo("en");
            string descpEn = "";
            string descpAr = "";
            if (!String.IsNullOrEmpty(source.EmployeeDetailsE))
            {
                descpEn = source.EmployeeDetailsE.Replace("\n", "");
                descpEn = descpEn.Replace("\r", "");
            }
            if (!String.IsNullOrEmpty(source.EmployeeDetailsA))
            {
                descpAr = source.EmployeeDetailsA.Replace("\n", "");
                descpAr = descpAr.Replace("\r", "");
            }
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
                EmployeeIqamaExpiryDt = !string.IsNullOrEmpty(source.EmployeeIqamaExpiryDt) ? DateTime.ParseExact(source.EmployeeIqamaExpiryDt, "dd/MM/yyyy", new CultureInfo("en")) : (DateTime?)null,
                EmployeeDOB = !string.IsNullOrEmpty(source.EmployeeDOB) ? DateTime.ParseExact(source.EmployeeDOB, "dd/MM/yyyy", new CultureInfo("en")) : (DateTime?)null,
                EmployeeLandlineNum = source.EmployeeLandlineNum ?? "",
                MaritalStatus = source.MaritalStatus,
                EmployeeMobileNum = source.EmployeeMobileNum ?? "",
                JobTitleId = source.JobTitleId,
                EmployeeNationality = source.EmployeeNationality,
                EmployeePassportNum = source.EmployeePassportNum ?? "",
                EmployeePassportExpiryDt = !string.IsNullOrEmpty(source.EmployeePassportExpiryDt) ? DateTime.ParseExact(source.EmployeePassportExpiryDt, "dd/MM/yyyy", new CultureInfo("en")) : (DateTime?)null,
                EmployeeDetailsE = descpEn,
                EmployeeDetailsA = descpAr,
                RecCreatedBy = source.RecCreatedBy,
                RecCreatedDt = source.RecCreatedDt,
                RecLastUpdatedBy = source.RecLastUpdatedBy,
                RecLastUpdatedDt = source.RecLastUpdatedDt,
                Email = source.Email,
                EmployeeJobId = source.EmployeeJobId,
                IsActivated = source.IsActivated,
            };
            //Thread.CurrentThread.CurrentCulture = new CultureInfo("ar");
            return caseType;
        }
        public static Models.Employee CreateFromServerToClient(this Employee source)
        {
            string descpEn = "";
            string descpAr = "";
            if (!String.IsNullOrEmpty(source.EmployeeDetailsE))
            {
                descpEn = source.EmployeeDetailsE.Replace("\n", "");
                descpEn = descpEn.Replace("\r", "");
            }
            if (!String.IsNullOrEmpty(source.EmployeeDetailsA))
            {
                descpAr = source.EmployeeDetailsA.Replace("\n", "");
                descpAr = descpAr.Replace("\r", "");
            }
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
                EmployeeIqamaIssueDt = source.EmployeeIqamaIssueDt != null ? Convert.ToDateTime(source.EmployeeIqamaIssueDt).ToString("dd/MM/yyyy", new CultureInfo("en")) : string.Empty,
                EmployeeIqamaExpiryDt = source.EmployeeIqamaExpiryDt != null ? Convert.ToDateTime(source.EmployeeIqamaExpiryDt).ToString("dd/MM/yyyy", new CultureInfo("en")) : string.Empty,
                EmployeeDOB = source.EmployeeDOB != null ? Convert.ToDateTime(source.EmployeeDOB).ToString("dd/MM/yyyy", new CultureInfo("en")) : string.Empty,
                EmployeeLandlineNum = source.EmployeeLandlineNum ?? "",
                MaritalStatus = source.MaritalStatus,
                EmployeeMobileNum = source.EmployeeMobileNum ?? "",
                JobTitleId = source.JobTitleId,
                EmployeeNationality = source.EmployeeNationality,
                EmployeePassportNum = source.EmployeePassportNum ?? "",
                EmployeePassportExpiryDt = source.EmployeePassportExpiryDt != null ? Convert.ToDateTime(source.EmployeePassportExpiryDt).ToString("dd/MM/yyyy", new CultureInfo("en")) : string.Empty,
                EmployeeDetailsE = descpEn,
                EmployeeDetailsA = descpAr,
                RecCreatedBy = source.RecCreatedBy,
                RecCreatedDt = source.RecCreatedDt,
                RecLastUpdatedBy = source.RecLastUpdatedBy,
                RecLastUpdatedDt = source.RecLastUpdatedDt,
                Email = source.Email,
                IsActivated = source.IsActivated,
                EmployeeJobId = source.EmployeeJobId,
                //EmployeeRequests = source.EmployeeRequests.Select(x=>x.CreateFromServerToClient()),
                Allowances = source.Allowances.Select(x => x.CreateFromServerToClient()),
                JobTitle = (source.JobTitle != null) ? source.JobTitle.CreateFrom() : (new Models.JobTitle()),
                EmployeeFullNameE = source.EmployeeFirstNameE + " " + source.EmployeeMiddleNameE + " " + source.EmployeeLastNameE,
                EmployeeFullNameA = source.EmployeeFirstNameA + " " + source.EmployeeMiddleNameA + " " + source.EmployeeLastNameA,
                PrevJobTitleId = (source.JobTitle != null) ? source.JobTitle.JobTitleId : 0,
            };

        }
        public static Models.Employee CreateFromServerToClientForTask(this Employee source)
        {
            string descpEn = "";
            string descpAr = "";
            if (!String.IsNullOrEmpty(source.EmployeeDetailsE))
            {
                descpEn = source.EmployeeDetailsE.Replace("\n", "");
                descpEn = descpEn.Replace("\r", "");
            }
            if (!String.IsNullOrEmpty(source.EmployeeDetailsA))
            {
                descpAr = source.EmployeeDetailsA.Replace("\n", "");
                descpAr = descpAr.Replace("\r", "");
            }
            Models.Employee employee = new Models.Employee
            {
                EmployeeId = source.EmployeeId,
                EmployeeImagePath = source.EmployeeImagePath,
                EmployeeIqama = source.EmployeeIqama,
                EmployeeIqamaIssueDt = source.EmployeeIqamaIssueDt != null ? Convert.ToDateTime(source.EmployeeIqamaIssueDt).ToString("dd/MM/yyyy", new CultureInfo("en")) : string.Empty,
                EmployeeIqamaExpiryDt = source.EmployeeIqamaExpiryDt != null ? Convert.ToDateTime(source.EmployeeIqamaExpiryDt).ToString("dd/MM/yyyy", new CultureInfo("en")) : string.Empty,
                EmployeeDOB = source.EmployeeDOB != null ? Convert.ToDateTime(source.EmployeeDOB).ToString("dd/MM/yyyy", new CultureInfo("en")) : string.Empty,
                EmployeeLandlineNum = source.EmployeeLandlineNum ?? "",
                MaritalStatus = source.MaritalStatus,
                EmployeeMobileNum = source.EmployeeMobileNum ?? "",
                JobTitleId = source.JobTitleId,
                EmployeeNationality = source.EmployeeNationality,
                EmployeePassportNum = source.EmployeePassportNum ?? "",
                EmployeePassportExpiryDt = source.EmployeePassportExpiryDt != null ? Convert.ToDateTime(source.EmployeePassportExpiryDt).ToString("dd/MM/yyyy", new CultureInfo("en")) : string.Empty,
                EmployeeDetailsE = descpEn,
                EmployeeDetailsA = descpAr,
                RecCreatedBy = source.RecCreatedBy,
                RecCreatedDt = source.RecCreatedDt,
                RecLastUpdatedBy = source.RecLastUpdatedBy,
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
                EmployeeIqamaExpiryDt = source.EmployeeIqamaExpiryDt != null ? Convert.ToDateTime(source.EmployeeIqamaExpiryDt.ToString()).ToShortDateString() : string.Empty,
                EmployeeImagePath = ConfigurationManager.AppSettings["EmployeeImage"] + (string.IsNullOrEmpty(source.EmployeeImagePath) ? "profile.jpg" : source.EmployeeImagePath)
            };
        }
        public static Models.Employee CreateFromServerToClientWithImage(this Employee source)
        {
            string descpEn = "";
            string descpAr = "";
            if (!String.IsNullOrEmpty(source.EmployeeDetailsE))
            {
                descpEn = source.EmployeeDetailsE.Replace("\n", "");
                descpEn = descpEn.Replace("\r", "");
                descpEn = descpEn.Replace("\t", "");
            }
            if (!String.IsNullOrEmpty(source.EmployeeDetailsA))
            {
                descpAr = source.EmployeeDetailsA.Replace("\n", "");
                descpAr = descpAr.Replace("\r", "");
                descpAr = descpAr.Replace("\t", "");
            }
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
                EmployeeIqamaIssueDt = source.EmployeeIqamaIssueDt != null ? Convert.ToDateTime(source.EmployeeIqamaIssueDt).ToString("dd/MM/yyyy", new CultureInfo("en")) : string.Empty,
                EmployeeIqamaExpiryDt = source.EmployeeIqamaExpiryDt != null ? Convert.ToDateTime(source.EmployeeIqamaExpiryDt).ToString("dd/MM/yyyy", new CultureInfo("en")) : string.Empty,
                EmployeeDOB = source.EmployeeDOB != null ? Convert.ToDateTime(source.EmployeeDOB).ToString("dd/MM/yyyy", new CultureInfo("en")) : string.Empty,
                EmployeeLandlineNum = source.EmployeeLandlineNum ?? "",
                MaritalStatus = source.MaritalStatus,
                EmployeeMobileNum = source.EmployeeMobileNum ?? "",
                JobTitleId = source.JobTitleId,
                EmployeeNationality = source.EmployeeNationality,
                EmployeePassportNum = source.EmployeePassportNum ?? "",
                EmployeePassportExpiryDt = source.EmployeePassportExpiryDt != null ? Convert.ToDateTime(source.EmployeePassportExpiryDt).ToString("dd/MM/yyyy", new CultureInfo("en")) : string.Empty,
                EmployeeDetailsE = descpEn,
                EmployeeDetailsA = descpAr,
                RecCreatedBy = source.RecCreatedBy,
                RecCreatedDt = source.RecCreatedDt,
                RecLastUpdatedBy = source.RecLastUpdatedBy,
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
        public static EmployeeForDropDownList CreateFromServerToClientForDropDownList(this Employee source)
        {
            return new EmployeeForDropDownList
            {
                EmployeeId = source.EmployeeId,
                EmployeeNameE = source.EmployeeFirstNameE + " " + source.EmployeeMiddleNameE + " " + source.EmployeeLastNameE,
                EmployeeNameA = source.EmployeeFirstNameA + " " + source.EmployeeMiddleNameA + " " + source.EmployeeLastNameA,
                Email = source.Email,
            };
        }
        public static EmployeeForDropDownList CreateForIrfRequesterDropDownList(this Employee source)
        {
            var userId = source.AspNetUsers.FirstOrDefault(x => x.EmployeeId == source.EmployeeId);
            if(userId != null)
                return new EmployeeForDropDownList
                {
                    UserId = userId.Id,
                    EmployeeId = source.EmployeeId,
                    EmployeeNameE = source.EmployeeFirstNameE + " " + source.EmployeeMiddleNameE + " " + source.EmployeeLastNameE,
                    EmployeeNameA = source.EmployeeFirstNameA + " " + source.EmployeeMiddleNameA + " " + source.EmployeeLastNameA,
                    Email = source.Email,
                };
            return new EmployeeForDropDownList();
        }

        public static ContactList CreateForContactList(this Employee source)
        {
            return new ContactList
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

        public static Models.EmployeeJobHistory CreateFromServerToClientForJobHistory(this JobHistory source)
        {
            return new Models.EmployeeJobHistory
            {
                JobTitle = source.JobTitle,
                BasicSalary = source.BasicSalary,
                SalaryWithAllowances = source.SalaryWithAllowances,
                From = source.From,
                To = source.To,
                TotalSalaryReceived = source.TotalSalaryReceived
            };
        }
    }
}