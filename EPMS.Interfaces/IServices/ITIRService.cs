using System;
using System.Collections.Generic;
using EPMS.Models.DomainModels;
using EPMS.Models.RequestModels;
using EPMS.Models.ResponseModels;

namespace EPMS.Interfaces.IServices
{
    public interface ITIRService
    {
        TIRCreateResponse LoadTirResponseData(long? id);
        TIRListResponse GetAllTirs(TransferItemSearchRequest searchRequest);
        TirHistoryResponse GetTirHistoryData(long? id);
        TIR FindTirById(long id, string from);
        bool UpdateTirStatus(TransferItemStatus status);
        bool SavePO(TIR tir);
        bool AddTIR(TIR tir);
        bool UpdateTIR(TIR tir);
        void DeleteTIR(TIR tir);
        IEnumerable<TIR> GetRecentTIRs(int status, string requester, int warehouseId);
    }
}
