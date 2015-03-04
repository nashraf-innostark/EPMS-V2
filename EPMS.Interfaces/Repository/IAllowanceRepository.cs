using System;
using EPMS.Models.DomainModels;

namespace EPMS.Interfaces.Repository
{
    public interface IAllowanceRepository : IBaseRepository<Allowance, int>
    {
        Allowance FindForAllownce(long employeeId, DateTime currTime);
    }
}
