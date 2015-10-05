using System;
using System.Collections.Generic;
using EPMS.Interfaces.IServices;
using EPMS.Interfaces.Repository;
using EPMS.Models.DomainModels;
using EPMS.Models.ResponseModels;

namespace EPMS.Implementation.Services
{
    public class RFQService : IRFQService
    {
        private readonly IRFQRepository rfqRepository;

        public RFQService(IRFQRepository rfqRepository)
        {
            this.rfqRepository = rfqRepository;
        }

        public IEnumerable<RFQ> GetAllRfqs()
        {
            return rfqRepository.GetAll();
        }

        public RFQ FindRfqById(long id)
        {
            return rfqRepository.Find(id);
        }

        public RFQResponse GetRfqResponse(long quotationId, long customerId, string from)
        {
            RFQResponse response = new RFQResponse();
            if (quotationId > 0)
            {
                var rfq = rfqRepository.Find(quotationId);
                if (rfq != null)
                {
                    response.Rfq = rfq;
                }
            }
            return response;
        }

        public bool AddRfq(RFQ rfq)
        {
            try
            {
                rfqRepository.Add(rfq);
                rfqRepository.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool UpdateRfq(RFQ rfq)
        {
            try
            {
                rfqRepository.Update(rfq);
                rfqRepository.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
