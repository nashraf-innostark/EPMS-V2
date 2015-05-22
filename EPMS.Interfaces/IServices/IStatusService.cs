using System.Collections.Generic;
using EPMS.Models.DomainModels;

namespace EPMS.Interfaces.IServices
{
    public interface  IStatusService
    {
        IEnumerable<Status> GetAll();
        Status FindStatusById(long id);
        bool AddStatus(Status status);
        bool UpdateStatus(Status status);
        void DeleteStatus(Status status);
    }
}
