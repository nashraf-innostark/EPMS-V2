using EPMS.Interfaces.IServices;
using EPMS.Interfaces.Repository;

namespace EPMS.Implementation.Services
{
    public class PreRequisitTaskService : IPreRequisitTaskService
    {
        private readonly IPreRequisitTaskRepository RequisitTaskRepository;

        public PreRequisitTaskService(IPreRequisitTaskRepository requisitTaskRepository)
        {
            RequisitTaskRepository = requisitTaskRepository;
        }
    }
}
