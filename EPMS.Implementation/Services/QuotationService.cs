using System;
using System.Collections.Generic;
using EPMS.Interfaces.IServices;
using EPMS.Interfaces.Repository;
using EPMS.Models.DomainModels;

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

        public Quotation FindQuotationById(long? id)
        {
            return Repository.Find(Convert.ToInt32(id));
        }

        public bool AddQuotation(Quotation quotation)
        {
            return true;
        }

        public bool UpdateQuotation(Quotation quotation)
        {
            return true;
        }

        public void DeleteQuotation(Quotation quotation)
        {
            
        }
    }
}
