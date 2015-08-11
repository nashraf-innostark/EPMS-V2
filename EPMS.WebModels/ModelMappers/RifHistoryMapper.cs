using System.Collections.Generic;
using System.Linq;
using EPMS.Models.DomainModels;

namespace EPMS.WebModels.ModelMappers
{
    public static class RifHistoryMapper
    {
        public static RIFHistory CreateFromRifToRifHistory(this RIF source, Order order, IEnumerable<RIFItem> items)
        {
            return new RIFHistory
            {
                RIFId = source.RIFId,
                FormNumber = source.FormNumber,
                ParentId = source.RIFId,
                OrderId = source.OrderId,
                ReturningReasonE = source.ReturningReasonE,
                ReturningReasonA = source.ReturningReasonA,
                Status = source.Status == 0 ? 6 : source.Status,
                NotesE = source.NotesE,
                NotesA = source.NotesA,
                ManagerId = source.ManagerId,
                RecCreatedBy = source.RecCreatedBy,
                RecCreatedDate = source.RecCreatedDate,
                RecUpdatedBy = source.RecUpdatedBy,
                RecUpdatedDate = source.RecUpdatedDate,
                //AspNetUser = source.AspNetUser,
                Order = order,
                RIFItemHistories = items.Select(x=>x.CreateFromRifItemToRifItemHistory()).ToList(),
            };
        }
        public static RIF CreateFromRifHistoryToRif(this RIFHistory source)
        {
            return new RIF
            {
                RIFId = source.RIFId,
                FormNumber = source.FormNumber,
                OrderId = source.OrderId,
                ReturningReasonE = source.ReturningReasonE,
                ReturningReasonA = source.ReturningReasonA,
                Status = source.Status == 0 ? 6 : source.Status,
                NotesE = source.NotesE,
                NotesA = source.NotesA,
                ManagerId = source.ManagerId,
                RecCreatedBy = source.RecCreatedBy,
                RecCreatedDate = source.RecCreatedDate,
                RecUpdatedBy = source.RecUpdatedBy,
                RecUpdatedDate = source.RecUpdatedDate,
                AspNetUser = source.AspNetUser,
                Order = source.Order,
                //RIFItems = source.RIFItemHistories.Select(x => x.CreateFromRifItemHistoryToRifItem()).ToList(),
            };
        }
        public static RIF CreateFromRifHistoryToRifWithItems(this RIFHistory source)
        {
            return new RIF
            {
                RIFId = source.RIFId,
                FormNumber = source.FormNumber,
                OrderId = source.OrderId,
                ReturningReasonE = source.ReturningReasonE,
                ReturningReasonA = source.ReturningReasonA,
                Status = source.Status == 0 ? 6 : source.Status,
                NotesE = source.NotesE,
                NotesA = source.NotesA,
                ManagerId = source.ManagerId,
                RecCreatedBy = source.RecCreatedBy,
                RecCreatedDate = source.RecCreatedDate,
                RecUpdatedBy = source.RecUpdatedBy,
                RecUpdatedDate = source.RecUpdatedDate,
                AspNetUser = source.AspNetUser,
                Order = source.Order,
                RIFItems = source.RIFItemHistories.Select(x => x.CreateFromRifItemHistoryToRifItem()).ToList(),
            };
        }
    }
}
