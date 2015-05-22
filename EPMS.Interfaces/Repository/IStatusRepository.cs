using EPMS.Models.DomainModels;

namespace EPMS.Interfaces.Repository
{
    public interface IStatusRepository : IBaseRepository<Status, long>
    {
        bool StatusExists(Status status);
    }
}
