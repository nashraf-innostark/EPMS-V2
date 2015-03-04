using System.Linq;
using EPMS.Models.ResponseModels;
using EPMS.Web.DashboardModels;
using Complaint = EPMS.Models.DomainModels.Complaint;
using EmployeeRequest = EPMS.Models.DomainModels.EmployeeRequest;
using Project = EPMS.Models.DomainModels.Project;

namespace EPMS.Web.ModelMappers
{
    public static class DashboardMappers
    {
        public static Payroll CreatePayrollForDashboard(this PayrollWidgetResponse source)
        {
            return new Payroll
            {
                BasicSalary = source.BasicSalary,
                Allowances = source.Allowances,
                Deductions = source.Deductions,
                Total = source.Total
            };
        }
        public static DashboardModels.Project CreateForDashboardDDL(this Project source)
        {
            return new DashboardModels.Project
            {
                ProjectId = source.ProjectId,
                NameA = source.NameA,
                NameE = source.NameE
            };
        }
        public static DashboardModels.EmployeeRequest CreateForDashboard(this EmployeeRequest source)
        {
            return new DashboardModels.EmployeeRequest
            {
                RequestId = source.RequestId,
                EmployeeId = source.EmployeeId,
                EmployeeNameE = source.Employee.EmployeeFirstNameE + " " + source.Employee.EmployeeMiddleNameE + " " + source.Employee.EmployeeLastNameE,
                EmployeeNameA = source.Employee.EmployeeNameA,
                RequestTopic = source.RequestTopic,
                EmployeeNameEShort = (source.Employee.EmployeeFirstNameE + " " + source.Employee.EmployeeMiddleNameE + " " + source.Employee.EmployeeLastNameE).Length > 7 ? (source.Employee.EmployeeFirstNameE + " " + source.Employee.EmployeeMiddleNameE + " " + source.Employee.EmployeeLastNameE).Substring(0, 7) + "..." : (source.Employee.EmployeeFirstNameE + " " + source.Employee.EmployeeMiddleNameE + " " + source.Employee.EmployeeLastNameE),
                EmployeeNameAShort = source.Employee.EmployeeNameA.Length > 7 ? source.Employee.EmployeeNameA.Substring(0, 7) + "..." : source.Employee.EmployeeNameA,
                RequestTopicShort = source.RequestTopic.Length > 9 ? source.RequestTopic.Substring(0, 9) + "..." : source.RequestTopic,
                RequestDateString = source.RequestDate.ToShortDateString(),
                IsReplied = source.RequestDetails.OrderByDescending(x => x.RowVersion).FirstOrDefault().IsReplied
            };
        }
        public static DashboardModels.Complaint CreateForDashboard(this Complaint source)
        {
            return new DashboardModels.Complaint
            {
                ComplaintId = source.ComplaintId,
                ClientName = source.Customer.CustomerNameE,
                Topic = source.Topic,
                ClientNameShort = source.Customer.CustomerNameE.Length > 7 ? source.Customer.CustomerNameE.Substring(0, 7) + "..." : source.Customer.CustomerNameE,
                ClientNameAShort = source.Customer.CustomerNameA.Length > 7 ? source.Customer.CustomerNameA.Substring(0, 7) + "..." : source.Customer.CustomerNameA,
                TopicShort = source.Topic.Length > 9 ? source.Topic.Substring(0, 9) + "..." : source.Topic,
                IsReplied = source.IsReplied
            };
        }
    }
}