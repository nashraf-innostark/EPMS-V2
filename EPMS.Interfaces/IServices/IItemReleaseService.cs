using System.Collections;
using System.Collections.Generic;
using EPMS.Models.DomainModels;
using EPMS.Models.RequestModels;
using EPMS.Models.ResponseModels;

namespace EPMS.Interfaces.IServices
{
    public interface IItemReleaseService
    {
        IRFCreateResponse GetCreateResponse(long id);
        ItemRelease FindItemReleaseById(long id);
        IEnumerable<ItemRelease> GetAll();
        ItemReleaseResponse GetAllItemRelease(ItemReleaseSearchRequest searchRequest);
        bool AddItemRelease(ItemRelease itemRelease, List<ItemReleaseDetail> itemDetails);
        bool UpdateItemRelease(ItemRelease itemRelease, List<ItemReleaseDetail> itemDetails);
        void DeleteItemRelease(ItemRelease itemRelease);
    }
}
