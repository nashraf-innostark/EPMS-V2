using EPMS.Interfaces.IServices;
using EPMS.Interfaces.Repository;
using EPMS.Models.DomainModels;

namespace EPMS.Implementation.Services
{
    public class CompanyBankService : ICompanyBankService
    {
        private readonly ICompanyBankRepository bankRepository;

        #region Constructor

        public CompanyBankService(ICompanyBankRepository bankRepository)
        {
            this.bankRepository = bankRepository;
        }

        #endregion

        public bool AddDetail(CompanyBankDetail bank)
        {
            bankRepository.Add(bank);
            bankRepository.SaveChanges();
            return true;
        }

        public bool UpdateDetail(CompanyBankDetail bank)
        {
            bankRepository.Update(bank);
            bankRepository.SaveChanges();
            return true;
        }
    }
}
