using System.Collections.Generic;
using EPMS.Models.DomainModels;
using EPMS.Models.RequestModels.Reports;

namespace EPMS.Interfaces.Repository
{
    public interface ICustomerRepository : IBaseRepository<Customer, long>
    {
        IEnumerable<Customer> GetCustomerReportList(CustomerReportDetailRequest request);
        Customer GetCustomerByEmployeeId(long employeeId);
    }
}
