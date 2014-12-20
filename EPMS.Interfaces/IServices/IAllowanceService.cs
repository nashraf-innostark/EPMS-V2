using System.Collections.Generic;
using EPMS.Models.DomainModels;

namespace EPMS.Interfaces.IServices
{
    public interface IAllowanceService
    {
        Allowance FindAllowanceById(long? id);

        IEnumerable<Allowance> GetAll();
        bool AddAllowance(Allowance allowance);
        bool UpdateAllowance(Allowance allowance);
        void DeleteAllowance(Allowance allowance);
    }
}
