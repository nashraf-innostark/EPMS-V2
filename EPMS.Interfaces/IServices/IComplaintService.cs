using System.Collections.Generic;
using EPMS.Models.DomainModels;

namespace EPMS.Interfaces.IServices
{
    public interface IComplaintService
    {
        Complaint FindComplaintById(long id);
        bool AddComplaint(Complaint complaint);
        bool UpdateComplaint(Complaint complaint);
        IEnumerable<Complaint> GetAllComplaints();
        IEnumerable<Complaint> GetAllComplaintsByCustomerId(long id);
    }
}
