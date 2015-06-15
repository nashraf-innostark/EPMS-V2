using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
                OrderId = source.OrderId,
                UsageE = source.UsageE,
                UsageA = source.UsageA,
                Status = source.Status,
                NotesE = source.NotesE,
                NotesA = source.NotesA,
                ManagerId = source.ManagerId,
                RecCreatedBy = source.RecCreatedBy,
                RecCreatedDate = source.RecCreatedDate,
                RecUpdatedBy = source.RecUpdatedBy,
                RecUpdatedDate = source.RecUpdatedDate,

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
