using System.Linq;
using EPMS.Models.DomainModels;
using EPMS.Web.ViewModels.Dashboard;

namespace EPMS.Web.ModelMappers
{
    public static class EmployeeRequestMapper
    {
        #region Employee Request Mappers
            public static EmployeeRequest CreateFromClientToServer(this Models.Request source)
            {
                return new EmployeeRequest
                {
                    RequestId = source.RequestId,
                    EmployeeId = source.EmployeeId,
                    IsMonetary = source.IsMonetary,
                    RequestTopic = source.RequestTopic,
                    RequestDate = source.RequestDate,
                    RecCreatedBy = source.RecCreatedBy,
                    RecCreatedDt = source.RecCreatedDt,
                    RecLastUpdatedBy = source.RecLastUpdatedBy,
                    RecLastUpdatedDt = source.RecLastUpdatedDt
                };
            }

            public static Models.Request CreateFromServerToClient(this EmployeeRequest source)
            {
                return new Models.Request
                {
                    RequestId = source.RequestId,
                    EmployeeId = source.EmployeeId,
                    EmployeeJobId = source.Employee.EmployeeJobId,
                    EmployeeNameA = source.Employee.EmployeeNameA,
                    EmployeeNameE = source.Employee.EmployeeNameE,
                    DepartmentNameA = source.Employee.JobTitle.Department.DepartmentNameA,
                    DepartmentNameE = source.Employee.JobTitle.Department.DepartmentNameE,
                    IsMonetary = source.IsMonetary,
                    RequestTopic = source.RequestTopic,
                    RequestDate = source.RequestDate,
                    RequestDateString = source.RequestDate.ToShortDateString(),
                    RecCreatedBy = source.RecCreatedBy,
                    RecCreatedDt = source.RecCreatedDt,
                    RecLastUpdatedBy = source.RecLastUpdatedBy,
                    RecLastUpdatedDt = source.RecLastUpdatedDt,
                    //Employee = source.Employee.CreateFromServerToClient(),
                    RequestDetail = source.RequestDetails.OrderByDescending(x=>x.RowVersion).FirstOrDefault().CreateFromServerToClient()
                };
            }
            public static EmployeeRequestsViewModel CreateForDashboard(this EmployeeRequest source)
            {
                return new EmployeeRequestsViewModel
                {
                    RequestId = source.RequestId,
                    EmployeeId = source.EmployeeId,
                    EmployeeNameE = source.Employee.EmployeeNameE,
                    RequestTopic = source.RequestTopic,
                    RequestDateString = source.RequestDate.ToShortDateString()
                };
            }
        #endregion

        #region Employee Request Detail Mappers
            public static Models.RequestDetail CreateFromServerToClient(this RequestDetail source)
            {
                return new Models.RequestDetail
                {
                    RequestDetailId = source.RequestDetailId,
                    RequestId = source.RequestId,
                    RequestDesc = source.RequestDesc,
                    RequestReply = source.RequestReply,
                    LoanAmount = source.LoanAmount,
                    LoanDate = source.LoanDate,
                    InstallmentAmount = source.InstallmentAmount,
                    NumberOfMonths = source.NumberOfMonths,
                    FirstInstallmentDate = source.FirstInstallmentDate,
                    LastInstallmentDate = source.LastInstallmentDate,
                    IsApproved = source.IsApproved,
                    IsReplied = source.IsReplied,
                    RowVersion = source.RowVersion,
                    RecCreatedBy = source.RecCreatedBy,
                    RecCreatedDt = source.RecCreatedDt,
                    RecLastUpdatedBy = source.RecLastUpdatedBy,
                    RecLastUpdatedDt = source.RecLastUpdatedDt
                };
            }
            public static Models.RequestDetail CreateFromServerToClientPayroll(this RequestDetail source)
            {
                return new Models.RequestDetail
                {
                    RequestDetailId = source.RequestDetailId,
                    RequestId = source.RequestId,
                    InstallmentAmount = source.InstallmentAmount,
                };
            }
            public static RequestDetail CreateFromClientToServer(this Models.RequestDetail source)
            {
                return new RequestDetail
                {
                    RequestDetailId = source.RequestDetailId,
                    RequestId = source.RequestId,
                    RequestDesc = source.RequestDesc,
                    RequestReply = source.RequestReply,
                    LoanAmount = source.LoanAmount,
                    LoanDate = source.LoanDate,
                    InstallmentAmount = source.InstallmentAmount,
                    NumberOfMonths = source.NumberOfMonths,
                    FirstInstallmentDate = source.FirstInstallmentDate,
                    LastInstallmentDate = source.LastInstallmentDate,
                    IsApproved = source.IsApproved,
                    IsReplied = source.IsReplied,
                    RowVersion = source.RowVersion,
                    RecCreatedBy = source.RecCreatedBy,
                    RecCreatedDt = source.RecCreatedDt,
                    RecLastUpdatedBy = source.RecLastUpdatedBy,
                    RecLastUpdatedDt = source.RecLastUpdatedDt
                };
            }
        #endregion

        #region Payroll Mapper
            public static Models.Payroll CreateFromServerToClientPayroll(this EmployeeRequest source)
            {
                return new Models.Payroll
                {
                    RequestId = source.RequestId,
                    EmployeeId = source.EmployeeId,
                    IsMonetary = source.IsMonetary,
                    RequestTopic = source.RequestTopic,
                    RequestDate = source.RequestDate,
                    RequestDetails = source.RequestDetails.Select(x => x.CreateFromServerToClientPayroll())
                };
            }
        #endregion
    }
}