using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebModels = EPMS.Web.Models;
using DomainModels = EPMS.Models.DomainModels;

namespace EPMS.Web.ModelMappers
{
    public static class InventoryItemVariationMapper
    {
        public static WebModels.InventoryItemForVariation CreateForItemVariation(this DomainModels.InventoryItem source)
        {
            return new WebModels.InventoryItemForVariation
            {
                ItemNameEn = source.ItemNameEn,
                ItemNameAr = source.ItemNameAr,
                InventoryDepartment = source.InventoryDepartment.CreateFromServerToClient()
            };
        }
    }
}