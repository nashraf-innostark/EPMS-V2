using System;
using System.Collections.Generic;
using EPMS.Interfaces.IServices;
using EPMS.Interfaces.Repository;
using EPMS.Models.Common;
using EPMS.Models.DomainModels;
using EPMS.Models.ResponseModels;

namespace EPMS.Implementation.Services
{
    public class RFQService : IRFQService
    {
        private readonly IRFQRepository rfqRepository;
        private readonly IRFQItemRepository rfqItemRepository;
        private readonly ICompanyProfileRepository companyProfileRepository;
        private readonly IShoppingCartRepository cartRepository;

        public RFQService(IRFQRepository rfqRepository, IRFQItemRepository rfqItemRepository, ICompanyProfileRepository companyProfileRepository, IShoppingCartRepository cartRepository)
        {
            this.rfqRepository = rfqRepository;
            this.rfqItemRepository = rfqItemRepository;
            this.companyProfileRepository = companyProfileRepository;
            this.cartRepository = cartRepository;
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
            response.Profile = companyProfileRepository.GetCompanyProfile();
            return response;
        }

        public RFQDetailResponse GetRfqDetailResponse(long rfqId)
        {
            RFQDetailResponse response = new RFQDetailResponse();
            if (rfqId != 0)
            {
                response.Rfq = rfqRepository.Find(rfqId);
            }
            response.Profile = companyProfileRepository.GetCompanyProfile();
            return response;
        }

        public bool AddRfq(RFQ rfq)
        {
            try
            {
                rfqRepository.Add(rfq);
                rfqRepository.SaveChanges();
                // Update Shopping Cart status to InProgress
                var cart = cartRepository.FindByUserCartId(rfq.RecCreatedBy);
                if (cart != null)
                {
                    cart.Status = (int) PurchaseStatus.InProgress;
                    cartRepository.Update(cart);
                    cartRepository.SaveChanges();
                }
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

        public void DeleteRfq(long id)
        {
            RFQ toDelete = rfqRepository.Find(id);
            if (toDelete != null)
            {
                rfqRepository.Delete(toDelete);
                rfqRepository.SaveChanges();
            }
        }

        public void DeleteRfqItem(long id)
        {
            RFQItem toDelete = rfqItemRepository.Find(id);
            if (toDelete != null)
            {
                rfqItemRepository.Delete(toDelete);
                rfqItemRepository.SaveChanges();
            }
        }
    }
}
