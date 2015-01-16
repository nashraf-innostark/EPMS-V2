using System;
using EPMS.Models.ResponseModels;

namespace EPMS.Interfaces.IServices
{
    public interface IPayrollService
    {
        PayrollWidgetResponse LoadPayroll(long employeeId, DateTime date);
    }
}
