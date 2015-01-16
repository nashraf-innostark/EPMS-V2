using EPMS.Models.ResponseModels;
using EPMS.Web.DashboardModels;

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
    }
}