﻿using System.Collections.Generic;
using System.Linq;
using EPMS.Models.DomainModels;

namespace EPMS.Models.ModelMapers
{
    public static class DIFHistoryMapper
    {
        public static DIFHistory CreateFromDifToDifHistory(this DIF source, IEnumerable<DIFItem> itemHistory)
        {
            var rif = new DIFHistory
            {
                Id = source.Id,
                ParentId = source.Id,
                DefectivenessE = source.DefectivenessE,
                DefectivenessA = source.DefectivenessA,
                Status = source.Status == 0 ? 6 : source.Status,
                NotesE = source.NotesE,
                NotesA = source.NotesA,
                ManagerId = source.ManagerId,
                RecCreatedBy = source.RecCreatedBy,
                RecCreatedDate = source.RecCreatedDate,
                RecUpdatedBy = source.RecUpdatedBy,
                RecUpdatedDate = source.RecUpdatedDate,
                CreatedBy = source.AspNetUser,
                Manager = source.Manager,
                DIFItemHistories = itemHistory.Select(x=>x.CreateFromDifItemToDifItemHistory()).ToList(),
            };
            return rif;
        }
        public static DIF CreateFromDifHistoryToDif(this DIFHistory source)
        {
            var rif = new DIF
            {
                Id = source.ParentId,
                DefectivenessE = source.DefectivenessE,
                DefectivenessA = source.DefectivenessA,
                Status = source.Status == 0 ? 6 : source.Status,
                NotesE = source.NotesE,
                NotesA = source.NotesA,
                ManagerId = source.ManagerId,
                RecCreatedBy = source.RecCreatedBy,
                RecCreatedDate = source.RecCreatedDate,
                RecUpdatedBy = source.RecUpdatedBy,
                RecUpdatedDate = source.RecUpdatedDate,
                AspNetUser = source.CreatedBy,
                Manager = source.Manager
            };
            return rif;
        }
    }
}
