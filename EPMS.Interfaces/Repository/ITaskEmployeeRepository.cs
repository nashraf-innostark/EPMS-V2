using EPMS.Models.DomainModels;

namespace EPMS.Interfaces.Repository
{
    public interface ITaskEmployeeRepository : IBaseRepository<TaskEmployee, long>
    {
        int CountTasksByEmployeeId(long id);
    }
}
