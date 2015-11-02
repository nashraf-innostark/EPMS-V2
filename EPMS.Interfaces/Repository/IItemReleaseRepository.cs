using System;
using System.Collections.Generic;
using EPMS.Models.DomainModels;
using EPMS.Models.RequestModels;
using EPMS.Models.ResponseModels;

namespace EPMS.Interfaces.Repository
{
    public interface IItemReleaseRepository : IBaseRepository<ItemRelease, long>
    {
        ItemReleaseResponse GetAllItemRelease(ItemReleaseSearchRequest searchRequest);
        IEnumerable<ItemRelease> GetRecentIRFs(int status, string requester, DateTime date);
        string GetLastFormNumber();
        IEnumerable<ItemRelease> GetItemReleaseByOrder(long orderId);
    }
}
