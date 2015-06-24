﻿using System.Collections.Generic;
using EPMS.Models.DomainModels;

namespace EPMS.Interfaces.Repository
{
    public interface IItemWarehouseRepository : IBaseRepository<ItemWarehouse, long>
    {
        IEnumerable<ItemWarehouse> GetItemsByVariationId(long variationId);
    }
}