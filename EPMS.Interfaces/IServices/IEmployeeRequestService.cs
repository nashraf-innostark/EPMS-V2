using EPMS.Models.DomainModels;

namespace EPMS.Interfaces.IServices
{
    public interface IEmployeeRequestService
    {
        bool AddRequest(EmployeeRequest model);
    }
}
