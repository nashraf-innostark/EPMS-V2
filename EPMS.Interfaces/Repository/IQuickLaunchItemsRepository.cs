﻿using System.Collections.Generic;
using EPMS.Models.DomainModels;

namespace EPMS.Interfaces.Repository
{
    public interface IQuickLaunchItemsRepository : IBaseRepository<QuickLaunchItem, long>
    {
        IEnumerable<QuickLaunchItem> FindItemsbyEmployeeId(long? employeeId);
    }
}
