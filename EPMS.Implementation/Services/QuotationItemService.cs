using System;
using System.Collections.Generic;
using EPMS.Interfaces.IServices;
using EPMS.Interfaces.Repository;
using EPMS.Models.DomainModels;

namespace EPMS.Implementation.Services
{
    public class QuotationItemService : IQuotationItemService
    {
        private readonly IQuotationItemRepository Repository;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="repository"></param>
        public QuotationItemService(IQuotationItemRepository repository)
        {
            Repository = repository;
        }
        public IEnumerable<QuotationItemDetail> GetAll()
        {
            return Repository.GetAll();
        }

        public QuotationItemDetail FindQuotationById(long id)
        {
            return Repository.Find(id);
        }

        public bool AddQuotationItem(QuotationItemDetail itemDetail)
        {
            try
            {
                Repository.Add(itemDetail);
                Repository.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool UpdateQuotationItem(QuotationItemDetail itemDetail)
        {
            try
            {
                Repository.Update(itemDetail);
                Repository.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public void DeleteQuotationItem(QuotationItemDetail itemDetail)
        {
            
        }
    }
}
