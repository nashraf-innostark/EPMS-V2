using System;
using System.Globalization;
using System.Linq;
using EPMS.Models.DomainModels;

namespace EPMS.WebModels.ModelMappers
{
    public static class EmployeeRequestMapper
    {
        #region Employee Request Mappers
        public static EmployeeRequest CreateFromClientToServer(this  WebsiteModels.Request source)
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

        public static WebsiteModels.Request CreateFromServerToClient(this EmployeeRequest source)
            {
                return new WebsiteModels.Request
                {
                    RequestId = source.RequestId,
                    EmployeeId = source.EmployeeId,
                    EmployeeJobId = source.Employee.EmployeeJobId,
                    EmployeeNameA = source.Employee.EmployeeFirstNameA + " " + source.Employee.EmployeeMiddleNameA + " " + source.Employee.EmployeeLastNameA,
                    EmployeeNameE = source.Employee.EmployeeFirstNameE + " " + source.Employee.EmployeeMiddleNameE + " " + source.Employee.EmployeeLastNameE,
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
           
        #endregion

        #region Employee Request Detail Mappers
            public static WebsiteModels.RequestDetail CreateFromServerToClient(this RequestDetail source)
            {
                return new WebsiteModels.RequestDetail
                {
                    RequestDetailId = source.RequestDetailId,
                    RequestId = source.RequestId,
                    RequestDesc = source.RequestDesc,
                    RequestReply = source.RequestReply,
                    LoanAmount = source.LoanAmount,
                    LoanDate = Convert.ToDateTime(source.LoanDate).ToString("dd/MM/yyyy", new CultureInfo("en")),
                    InstallmentAmount = source.InstallmentAmount,
                    NumberOfMonths = source.NumberOfMonths,
                    FirstInstallmentDate = Convert.ToDateTime(source.FirstInstallmentDate).ToString("dd/MM/yyyy", new CultureInfo("en")),
                    LastInstallmentDate = Convert.ToDateTime(source.LastInstallmentDate).ToString("dd/MM/yyyy", new CultureInfo("en")),
                    IsApproved = source.IsApproved,
                    IsReplied = source.IsReplied,
                    RowVersion = source.RowVersion,
                    RecCreatedBy = source.RecCreatedBy,
                    RecCreatedDt = source.RecCreatedDt,
                    RecLastUpdatedBy = source.RecLastUpdatedBy,
                    RecLastUpdatedDt = source.RecLastUpdatedDt
                };
            }
            public static WebsiteModels.RequestDetail CreateFromServerToClientPayroll(this RequestDetail source)
            {
                return new WebsiteModels.RequestDetail
                {
                    RequestDetailId = source.RequestDetailId,
                    RequestId = source.RequestId,
                    InstallmentAmount = source.InstallmentAmount,
                };
            }
            public static WebsiteModels.RequestDetail CreateFromServerToClientEmpDetail(this RequestDetail source)
            {
                return new WebsiteModels.RequestDetail
                {
                    RequestDetailId = source.RequestDetailId,
                    RequestId = source.RequestId,
                    InstallmentAmount = source.InstallmentAmount,
                };
            }
            public static RequestDetail CreateFromClientToServer(this WebsiteModels.RequestDetail source)
            {
                RequestDetail requestDetail=new RequestDetail();
               
                requestDetail.RequestDetailId = source.RequestDetailId;
                requestDetail.RequestId = source.RequestId;
                requestDetail.RequestDesc = source.RequestDesc;
                requestDetail.RequestReply = source.RequestReply;
                requestDetail.LoanAmount = source.LoanAmount;
                if (source.IsMonetaryLocalFlag)
                {
                    requestDetail.LoanDate = DateTime.ParseExact(source.LoanDate, "dd/MM/yyyy", new CultureInfo("en"));
                    requestDetail.FirstInstallmentDate = DateTime.ParseExact(source.FirstInstallmentDate, "dd/MM/yyyy", new CultureInfo("en"));
                    requestDetail.LastInstallmentDate = DateTime.ParseExact(source.LastInstallmentDate, "dd/MM/yyyy", new CultureInfo("en"));
                }
                requestDetail.InstallmentAmount = source.InstallmentAmount;
                requestDetail.NumberOfMonths = source.NumberOfMonths;
                requestDetail.IsApproved = source.IsApproved;
                requestDetail.IsReplied = source.IsReplied;
                requestDetail.RowVersion = source.RowVersion;
                requestDetail.RecCreatedBy = source.RecCreatedBy;
                requestDetail.RecCreatedDt = source.RecCreatedDt;
                requestDetail.RecLastUpdatedBy = source.RecLastUpdatedBy;
                requestDetail.RecLastUpdatedDt = source.RecLastUpdatedDt;
                return requestDetail;
            }
        #endregion

        #region Payroll Mapper
            public static WebsiteModels.Payroll CreateFromServerToClientPayroll(this EmployeeRequest source)
            {
                return new WebsiteModels.Payroll
                {
                    RequestId = source.RequestId,
                    EmployeeId = source.EmployeeId,
                    IsMonetary = source.IsMonetary,
                    RequestTopic = source.RequestTopic,
                    RequestDate = source.RequestDate,
                    RequestDetails = source.RequestDetails.Select(x => x.CreateFromServerToClientPayroll())
                };
            }
            public static WebsiteModels.Payroll CreateFromServerToClientEmpDetail(this EmployeeRequest source)
            {
                WebsiteModels.Payroll retVal = new WebsiteModels.Payroll();
                retVal.RequestId = source.RequestId;
                retVal.EmployeeId = source.EmployeeId;
                retVal.IsMonetary = source.IsMonetary;
                retVal.RequestTopic = source.RequestTopic;
                retVal.RequestDate = source.RequestDate;
                retVal.RequestDetails =
                    source.RequestDetails.Where(x => x.RowVersion == 1)
                        .Select(x => x.CreateFromServerToClientEmpDetail());
                return retVal;
            }
        #endregion
    }
}