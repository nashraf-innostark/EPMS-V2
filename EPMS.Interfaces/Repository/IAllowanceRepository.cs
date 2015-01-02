using System;
using System.Collections.Generic;
using EPMS.Models.DomainModels;

namespace EPMS.Interfaces.Repository
{
    public interface IAllowanceRepository : IBaseRepository<Allowance, int>
    {
        IEnumerable<Allowance> FindForPayroll(long id, DateTime currTime);
    }
}
