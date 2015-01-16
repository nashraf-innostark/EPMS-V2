using EPMS.Interfaces.IServices;
using EPMS.Interfaces.Repository;
using EPMS.Models.DomainModels;

namespace EPMS.Implementation.Services
{
    public class CompanyDocumentService : ICompanyDocumentService
    {
        #region Private

        private readonly ICompanyDocumentRepository documentRepository;

        #endregion

        #region Constructor

        public CompanyDocumentService(ICompanyDocumentRepository documentRepository)
        {
            this.documentRepository = documentRepository;
        }

        #endregion
        public bool AddDetail(CompanyDocumentDetail document)
        {
            documentRepository.Add(document);
            documentRepository.SaveChanges();
            return true;
        }

        public bool UpdateDetail(CompanyDocumentDetail document)
        {
            documentRepository.Update(document);
            documentRepository.SaveChanges();
            return true;
        }
    }
}
