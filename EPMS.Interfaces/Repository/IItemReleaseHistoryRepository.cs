using System.Collections.Generic;
using EPMS.Models.DomainModels;

namespace EPMS.Interfaces.Repository
{
    public interface IItemReleaseHistoryRepository : IBaseRepository<ItemReleaseHistory, long>
    {
        IEnumerable<ItemReleaseHistory> GetIrfHistoryData();
    }
}
