﻿using System.Collections.Generic;
using EPMS.Models.DomainModels;
using EPMS.Models.ResponseModels;

namespace EPMS.Interfaces.IServices
{
    public interface IRFQService
    {
        IEnumerable<RFQ> GetAllRfqs() ;
        IEnumerable<RFQ> GetAllRfqs(string customerId, long employeeId);
        IEnumerable<RFQ> GetPendingRfqsByCustomerId(long customerId);
        RFQ FindRfqById(long id);
        RFQResponse GetRfqResponse(long quotationId, long customerId, string from);
        RFQDetailResponse GetRfqDetailResponse(long rfqId);
        bool AddRfq(RFQ rfq);
        bool UpdateRfq(RFQ rfq);
        void DeleteRfq(long id);
        void DeleteRfqItem(long id);
    }
}
