using System.Collections.Generic;
using EPMS.Models.DomainModels;

namespace EPMS.Interfaces.IServices
{
    public interface IQuotationService
    {
        IEnumerable<Quotation> GetAll();
        Quotation FindQuotationById(long? id);
        bool AddQuotation(Quotation quotation);
        bool UpdateQuotation(Quotation quotation);
        void DeleteQuotation(Quotation quotation);
    }
}
