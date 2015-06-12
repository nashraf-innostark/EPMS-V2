using System.Collections.Generic;
using EPMS.Models.DomainModels;

namespace EPMS.Interfaces.Repository
{
    public interface ITIRItemRepository : IBaseRepository<TIRItem, long>
    {
        IEnumerable<TIRItem> GetTirItemsById(long id);
    }
}
