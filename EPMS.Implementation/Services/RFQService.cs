using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
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
        private readonly ICustomerRepository customerRepository;

        public RFQService(IRFQRepository rfqRepository, IRFQItemRepository rfqItemRepository, ICompanyProfileRepository companyProfileRepository, IShoppingCartRepository cartRepository, ICustomerRepository customerRepository)
        {
            this.rfqRepository = rfqRepository;
            this.rfqItemRepository = rfqItemRepository;
            this.companyProfileRepository = companyProfileRepository;
            this.cartRepository = cartRepository;
            this.customerRepository = customerRepository;
        }

        public IEnumerable<RFQ> GetAllRfqs()
        {
            return rfqRepository.GetAll();
        }

        public IEnumerable<RFQ> GetPendingRfqsByCustomerId(long customerId)
        {
            return rfqRepository.GetPendingRfqsByCustomerId(customerId);
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
                rfq.SerialNumber = GetRfqSerialNumber();
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

        // Get Serial Number
        public string GetRfqSerialNumber()
        {
            string year = DateTime.Now.ToString("yyyy");
            string month = DateTime.Now.ToString("MM");
            string day = DateTime.Now.ToString("dd");
            var result = rfqRepository.GetAll().OrderByDescending(x=>x.RecCreatedDate);
            if (result.FirstOrDefault() != null)
            {
                var rfq = result.FirstOrDefault();
                string oId = rfq.SerialNumber.Substring(rfq.SerialNumber.Length - 5, 5);
                int id = Convert.ToInt32(oId) + 1;
                int len = id.ToString(CultureInfo.InvariantCulture).Length;
                string zeros = "";
                switch (len)
                {
                    case 1:
                        zeros = "0000";
                        break;
                    case 2:
                        zeros = "000";
                        break;
                    case 3:
                        zeros = "00";
                        break;
                    case 4:
                        zeros = "0";
                        break;
                    case 5:
                        zeros = "";
                        break;
                }
                string orderId = year + month + day + zeros + id.ToString(CultureInfo.InvariantCulture);
                return orderId;
            }
            return year + month + day + "00001";
        }
    }
}
