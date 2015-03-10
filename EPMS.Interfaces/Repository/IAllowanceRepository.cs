using System;
using System.Collections.Generic;
using EPMS.Models.DomainModels;

namespace EPMS.Interfaces.Repository
{
    public interface IAllowanceRepository : IBaseRepository<Allowance, int>
    {
        Allowance FindAllownce(long employeeId, DateTime currTime);
        Allowance FindLastAllownce(long employeeId);
        IEnumerable<Allowance> FindAllownceFromTo(long employeeId, DateTime from, DateTime to);
    }
}
