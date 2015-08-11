using System.Linq;
using EPMS.Models.DomainModels;

namespace EPMS.WebModels.ModelMappers
{
    public static class PoHistoryMapper
    {
        public static PurchaseOrderHistory CreateFromPoToPoHistory(this PurchaseOrder source)
        {
            return new PurchaseOrderHistory
            {
                PurchaseOrderId = source.PurchaseOrderId,
                ParentId = source.PurchaseOrderId,
                FormNumber = source.FormNumber,
                NotesE = source.NotesE,
                NotesA = source.NotesA,
                ManagerId = source.ManagerId,
                Status = source.Status,
                RecCreatedBy = source.RecCreatedBy,
                RecCreatedDate = source.RecCreatedDate,
                RecUpdatedBy = source.RecUpdatedBy,
                RecUpdatedDate = source.RecUpdatedDate,
                PurchaseOrderItemHistories = source.PurchaseOrderItems.Select(x => x.CreateFromPoItemToPoItemHistory()).ToList(),
            };
        }
        public static PurchaseOrder CreateFromPoHistoryToPo(this PurchaseOrderHistory source)
        {
            return new PurchaseOrder
            {
                PurchaseOrderId = source.PurchaseOrderId,
                FormNumber = source.FormNumber,
                NotesE = source.NotesE,
                NotesA = source.NotesA,
                ManagerId = source.ManagerId,
                Status = source.Status,
                RecCreatedBy = source.RecCreatedBy,
                RecCreatedDate = source.RecCreatedDate,
                RecUpdatedBy = source.RecUpdatedBy,
                RecUpdatedDate = source.RecUpdatedDate,
                PurchaseOrderItems = source.PurchaseOrderItemHistories.Select(x => x.CreateFromPoItemHistoryToPoItem()).ToList(),
                AspNetUser = source.AspNetUser,
                Manager = source.Manager,
            };
        }
    }
}
