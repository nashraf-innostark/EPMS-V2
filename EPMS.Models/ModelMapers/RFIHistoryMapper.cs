using System;
using System.Collections.Generic;
using System.Linq;
using EPMS.Models.DomainModels;

namespace EPMS.Models.ModelMapers
{
    public static class RFIHistoryMapper
    {
        public static RFIHistory CreateFromRfiToRfiHistory(this RFI source, IEnumerable<RFIItem> rfiItems)
        {
            var rfi = new RFIHistory
            {
                RFIId = source.RFIId,
                ParentId = source.RFIId,
                FormNumber = source.FormNumber,
                OrderId = source.OrderId,
                UsageE = source.UsageE,
                UsageA = source.UsageA,
                Status = source.Status,
                NotesE = source.NotesE,
                NotesA = source.NotesA,
                ManagerId = source.ManagerId,
                RecCreatedBy = source.RecCreatedBy,
                RecCreatedDate = DateTime.Now,
                RecUpdatedBy = source.RecUpdatedBy,
                RecUpdatedDate = DateTime.Now,

                RFIItemHistories = rfiItems.Select(x => x.CreateRfiItemToRfiItemHistory()).ToList(),
                AspNetUser = source.AspNetUser,
                Order = source.Order
            };
            return rfi;
        }
        public static RFI CreateFromRfiHistoryToRfi(this RFIHistory source)
        {
            var rfi = new RFI
            {
                RFIId = source.RFIId,
                FormNumber = source.FormNumber,
                OrderId = source.OrderId,
                UsageE = source.UsageE,
                UsageA = source.UsageA,
                Status = source.Status ?? 6,
                NotesE = source.NotesE,
                NotesA = source.NotesA,
                ManagerId = source.ManagerId,
                RecCreatedBy = source.RecCreatedBy,
                RecCreatedDate = source.RecCreatedDate,
                RecUpdatedBy = source.RecUpdatedBy,
                RecUpdatedDate = source.RecUpdatedDate,

                RFIItems = source.RFIItemHistories.Select(x => x.CreateRfiItemHistoryToRfiItem()).ToList(),
                AspNetUser = source.AspNetUser,
                Order = source.Order,
            };
            return rfi;
        }
        public static RFI CreateFromRfiHistoryToRfiBase(this RFIHistory source)
        {
            var rfi = new RFI
            {
                RFIId = source.RFIId,
                FormNumber = source.FormNumber,
                OrderId = source.OrderId,
                UsageE = source.UsageE,
                UsageA = source.UsageA,
                Status = source.Status ?? 6,
                NotesE = source.NotesE,
                NotesA = source.NotesA,
                ManagerId = source.ManagerId,
                RecCreatedBy = source.RecCreatedBy,
                RecCreatedDate = source.RecCreatedDate,
                RecUpdatedBy = source.RecUpdatedBy,
                RecUpdatedDate = source.RecUpdatedDate,

                RFIItems = source.RFIItemHistories.Select(x => x.CreateRfiItemHistoryToRfiItem()).ToList(),
                AspNetUser = source.AspNetUser,
                Order = source.Order,
            };
            return rfi;
        }
    }
}
