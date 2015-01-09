using EPMS.Models.DomainModels;

namespace EPMS.Interfaces.IServices
{
    public interface IComplaintService
    {
        Complaint FindComplaintById(long id);
    }
}
