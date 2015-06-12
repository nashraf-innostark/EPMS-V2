using System.Linq;
using EPMS.Models.DomainModels;
using EPMS.Web.ViewModels.PurchaseOrder;

namespace EPMS.Web.ModelMappers
{
    public static class PurchaseOrderMapper
    {
        public static Models.PurchaseOrder CreateFromServerToClient(this PurchaseOrder source)
        {
            var direction = Resources.Shared.Common.TextDirection;
            return new Models.PurchaseOrder
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
                RequesterName = direction == "ltr" ?
                source.AspNetUser.Employee.EmployeeFirstNameE + " " + source.AspNetUser.Employee.EmployeeMiddleNameE + " " + source.AspNetUser.Employee.EmployeeLastNameE
                : source.AspNetUser.Employee.EmployeeFirstNameA + " " + source.AspNetUser.Employee.EmployeeMiddleNameA + " " + source.AspNetUser.Employee.EmployeeLastNameA,
            };
        }
        public static PurchaseOrder CreateFromClientToServer(this Models.PurchaseOrder source)
        {
            var notesE = source.NotesE;
            if (!string.IsNullOrEmpty(notesE))
            {
                notesE = notesE.Replace("\r", "");
                notesE = notesE.Replace("\t", "");
                notesE = notesE.Replace("\n", "");
            }
            var notesA = source.NotesA;
            if (!string.IsNullOrEmpty(notesA))
            {
                notesA = notesA.Replace("\r", "");
                notesA = notesA.Replace("\t", "");
                notesA = notesA.Replace("\n", "");
            }
            return new PurchaseOrder
            {
                PurchaseOrderId = source.PurchaseOrderId,
                FormNumber = source.FormNumber,
                NotesE = notesE,
                NotesA = notesA,
                ManagerId = source.ManagerId,
                Status = source.Status,
                RecCreatedBy = source.RecCreatedBy,
                RecCreatedDate = source.RecCreatedDate,
                RecUpdatedBy = source.RecUpdatedBy,
                RecUpdatedDate = source.RecUpdatedDate,
            };
        }
        public static PurchaseOrder CreateFromClientToServer(this PurchaseOrderCreateViewModel source)
        {
            var notesE = source.Order.NotesE;
            if (!string.IsNullOrEmpty(notesE))
            {
                notesE = notesE.Replace("\r", "");
                notesE = notesE.Replace("\t", "");
                notesE = notesE.Replace("\n", "");
            }
            var notesA = source.Order.NotesA;
            if (!string.IsNullOrEmpty(notesA))
            {
                notesA = notesA.Replace("\r", "");
                notesA = notesA.Replace("\t", "");
                notesA = notesA.Replace("\n", "");
            }
            return new PurchaseOrder
            {
                PurchaseOrderId = source.Order.PurchaseOrderId,
                FormNumber = source.Order.FormNumber,
                NotesE = notesE,
                NotesA = notesA,
                ManagerId = source.Order.ManagerId,
                Status = source.Order.Status,
                RecCreatedBy = source.Order.RecCreatedBy,
                RecCreatedDate = source.Order.RecCreatedDate,
                RecUpdatedBy = source.Order.RecUpdatedBy,
                RecUpdatedDate = source.Order.RecUpdatedDate,
                PurchaseOrderItems = source.PoItems.Select(x => x.CreateFromClientToServer(source.Order.PurchaseOrderId, source.Order.RecCreatedBy, source.Order.RecCreatedDate, source.Order.RecUpdatedDate)).ToList()
            };
        }
    }
}