using System.Collections.Generic;
using EPMS.Models.DomainModels;

namespace EPMS.Interfaces.IServices
{
    public interface IComplaintService
    {
        Complaint FindComplaintById(long id);
        bool AddComplaint(Complaint complaint);
        bool UpdateComplaint(Complaint complaint);
        IEnumerable<Complaint> LoadAllComplaints();
        IEnumerable<Complaint> LoadAllComplaintsByCustomerId(long id);
        IEnumerable<Complaint> LoadComplaintsForDashboard(string requester);
    }
}
