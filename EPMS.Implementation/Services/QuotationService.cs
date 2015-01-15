using System;
using System.Collections.Generic;
using EPMS.Interfaces.IServices;
using EPMS.Interfaces.Repository;
using EPMS.Models.DomainModels;
using EPMS.Models.RequestModels;
using EPMS.Models.ResponseModels;

namespace EPMS.Implementation.Services
{
    public class QuotationService : IQuotationService
    {
        private readonly IQuotationRepository Repository;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="repository"></param>
        public QuotationService(IQuotationRepository repository)
        {
            Repository = repository;
        }

        public IEnumerable<Quotation> GetAll()
        {
            return Repository.GetAll();
        }

        public Quotation FindQuotationById(long id)
        {
            return Repository.Find(id);
        }

        public long AddQuotation(Quotation quotation)
        {
            Repository.Add(quotation);
            Repository.SaveChanges();
            return quotation.QuotationId;
        }

        public bool UpdateQuotation(Quotation quotation)
        {
            return true;
        }

        public void DeleteQuotation(Quotation quotation)
        {
            
        }

        public QuotationResponse GetAllQuotation(QuotationSearchRequest searchRequest)
        {
            return Repository.GetAllQuotation(searchRequest);
        }
    }
}
