using EPMS.Models.DomainModels;
using EPMS.Models.RequestModels;
using EPMS.Models.ResponseModels;

namespace EPMS.Interfaces.IServices
{
    public interface ITIRService
    {
        TIRCreateResponse LoadTirResponseData(long? id);
        TIRListResponse GetAllTirs(TransferItemSearchRequest searchRequest);
        TirHistoryResponse GetTirHistoryData();
        TIR Find(long id);
        bool UpdateTirStatus(TransferItemStatus status);
        bool SavePO(TIR tir);
        bool AddTIR(TIR tir);
        bool UpdateTIR(TIR tir);
        void DeleteTIR(TIR tir);
    }
}
