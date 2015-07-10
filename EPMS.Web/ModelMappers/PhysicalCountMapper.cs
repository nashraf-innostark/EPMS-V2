﻿using System.Linq;
using EPMS.Models.DomainModels;
using EPMS.Web.Models;
using EPMS.Web.ViewModels.PhysicalCount;

namespace EPMS.Web.ModelMappers
{
    public static class PhysicalCountMapper
    {
        public static PhysicalCountListModel CreateListFromServerToClient(this PhysicalCount source)
        {
            return new PhysicalCountListModel
            {
                PCId = source.PCId
            };
        }
        public static PhysicalCountModel CreateFromServerToClient(this PhysicalCount source)
        {
            return new PhysicalCountModel
            {
                PCId = source.PCId,
                RecCreatedBy = source.RecCreatedBy,
                RecLastUpdatedBy = source.RecLastUpdatedBy,
                RecCreatedDate = source.RecCreatedDate,
                RecLastUpdatedDate = source.RecLastUpdatedDate,
                NotesEn = source.NotesEn,
                NotesAr = source.NotesAr,

                WarehouseId = source.PhysicalCountItems.Any() ? source.PhysicalCountItems.FirstOrDefault().WarehouseId : 0
            };
        }
        public static PhysicalCount CreateFromClientToServer(this PhysicalCountViewModel source)
        {
            return new PhysicalCount
            {
                PCId = source.PhysicalCount.PCId,
                RecCreatedBy = source.PhysicalCount.RecCreatedBy,
                RecLastUpdatedBy = source.PhysicalCount.RecLastUpdatedBy,
                RecCreatedDate = source.PhysicalCount.RecCreatedDate,
                RecLastUpdatedDate = source.PhysicalCount.RecLastUpdatedDate,
                NotesEn = source.PhysicalCount.NotesEn,
                NotesAr = source.PhysicalCount.NotesAr,

                PhysicalCountItems = source.PhysicalCountItems.Select(x=>x.CreateFromClientToServer()).ToList()
            };
        }
    }
}