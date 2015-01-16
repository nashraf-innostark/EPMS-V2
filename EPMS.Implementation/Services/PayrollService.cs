using System;
using System.Linq;
using EPMS.Interfaces.IServices;
using EPMS.Interfaces.Repository;
using EPMS.Models.ResponseModels;

namespace EPMS.Implementation.Services
{
    public class PayrollService:IPayrollService
    {
        private readonly IEmployeeService employeeService;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="employeeService"></param>
        public PayrollService(IEmployeeService employeeService)
        {
            this.employeeService = employeeService;
        }

        public PayrollWidgetResponse LoadPayroll(long employeeId,DateTime date)
        {
            PayrollWidgetResponse payrollWidget = new PayrollWidgetResponse();
            PayrollResponse payrollResponse=employeeService.FindEmployeeForPayroll(employeeId,date);
            
            payrollWidget.BasicSalary = Convert.ToDouble(payrollResponse.Employee.JobTitle.BasicSalary);
            payrollWidget.Allowances = payrollResponse.Allowance!=null?Convert.ToDouble(payrollResponse.Allowance.Allowance1 + payrollResponse.Allowance.Allowance2 +
                                       payrollResponse.Allowance.Allowance3 + payrollResponse.Allowance.Allowance4 +
                                       payrollResponse.Allowance.Allowance5):0;
            // get employee requests
            if (payrollResponse.Requests != null)
            {
                // get Employee request details
                foreach (var reqDetail in payrollResponse.Requests)
                {
                    if (reqDetail.RequestDetails.FirstOrDefault() != null)
                        payrollWidget.Deductions += Math.Ceiling(reqDetail.RequestDetails.FirstOrDefault().InstallmentAmount ?? 0);
                }
            }
            payrollWidget.Total = (payrollWidget.BasicSalary + payrollWidget.Allowances) - payrollWidget.Deductions;
            return payrollWidget;
        }
    }
}