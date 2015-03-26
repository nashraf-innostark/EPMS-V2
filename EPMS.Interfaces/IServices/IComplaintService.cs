using System.Collections.Generic;
using EPMS.Models.DomainModels;
using EPMS.Models.ResponseModels;

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
        ComplaintResponse GetComplaintResponse(long complaintId, long customerId, string roleName);
    }
}
