using System.Collections.Generic;
using EPMS.Models.DomainModels;

namespace EPMS.Interfaces.Repository
{
    public interface IComplaintRepository : IBaseRepository<Complaint, long>
    {
        IEnumerable<Complaint> GetAllComplaintsByCustomerId(long id);
    }
}
