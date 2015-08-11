using System;
using System.Collections.Generic;
using EPMS.Models.DomainModels;
using EPMS.Models.RequestModels;
using EPMS.Models.ResponseModels;

namespace EPMS.Interfaces.IServices
{
    public interface IItemReleaseService
    {
        IRFCreateResponse GetCreateResponse(long id);
        ItemRelease FindItemReleaseById(long id, string from);
        IEnumerable<ItemRelease> GetAll();
        IrfHistoryResponse GetIrfHistoryData(long? parentId);
        ItemReleaseResponse GetAllItemRelease(ItemReleaseSearchRequest searchRequest);
        bool AddItemRelease(ItemRelease itemRelease, List<ItemReleaseDetail> itemDetails);
        bool UpdateItemReleaseStatus(ItemReleaseStatus releaseStatus);
        bool UpdateItemRelease(ItemRelease itemRelease, List<ItemReleaseDetail> itemDetails);
        void DeleteItemRelease(ItemRelease itemRelease);
        IEnumerable<ItemRelease> GetRecentIRFs(int status, string requester, DateTime date);
        IEnumerable<ItemRelease> GetItemReleaseByOrder(long orderId);
    }
}
