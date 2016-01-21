using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using EPMS.Models.DashboardModels;
using EPMS.Models.DomainModels;
using EPMS.Models.RequestModels;
using EPMS.WebModels.ViewModels.PurchaseOrder;

namespace EPMS.WebModels.ModelMappers
{
    public static class PurchaseOrderMapper
    {
        public static WebsiteModels.PurchaseOrder CreateFromServerToClient(this PurchaseOrder source)
        {
            var direction = Resources.Shared.Common.TextDirection;
            var purchaseOrder = new WebsiteModels.PurchaseOrder
            {
                PurchaseOrderId = source.PurchaseOrderId,
                FormNumber = source.FormNumber,
                NotesE = source.NotesE,
                NotesA = source.NotesA,
                ManagerId = source.ManagerId,
                Status = source.Status,
                RecCreatedBy = source.RecCreatedBy,
                RecCreatedDate = source.RecCreatedDate.ToString("dd/MM/yyyy", new CultureInfo("en")),
                RecCreatedDateStr = source.RecCreatedDate.ToString("dd/MM/yyyy", new CultureInfo("en")),
                RecUpdatedBy = source.RecUpdatedBy,
                RecUpdatedDate = source.RecUpdatedDate,
                RecCreatedDateString = Convert.ToDateTime(source.RecCreatedDate).ToString("dd/MM/yyyy", new CultureInfo("en")) + "-" + Convert.ToDateTime(source.RecCreatedDate).ToString("dd/MM/yyyy", new CultureInfo("ar")),
                RequesterName = direction == "ltr" ?
                source.AspNetUser.Employee.EmployeeFirstNameE + " " + source.AspNetUser.Employee.EmployeeMiddleNameE + " " + source.AspNetUser.Employee.EmployeeLastNameE :
                source.AspNetUser.Employee.EmployeeFirstNameA + " " + source.AspNetUser.Employee.EmployeeMiddleNameA + " " + source.AspNetUser.Employee.EmployeeLastNameA,
                EmpJobId = source.AspNetUser.Employee.EmployeeJobId
            };
            if (source.Manager != null)
            {
                purchaseOrder.ManagerName = direction == "ltr" ?
                source.Manager.Employee.EmployeeFirstNameE + " " + source.Manager.Employee.EmployeeMiddleNameE + " " + source.Manager.Employee.EmployeeLastNameE
                : source.Manager.Employee.EmployeeFirstNameA + " " + source.Manager.Employee.EmployeeMiddleNameA + " " + source.Manager.Employee.EmployeeLastNameA;
                
            }
            return purchaseOrder;
        }
        public static PurchaseOrder CreateFromClientToServer(this WebsiteModels.PurchaseOrder source)
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
                RecCreatedDate = DateTime.ParseExact(source.RecCreatedDate, "dd/MM/yyyy", new CultureInfo("en")),
                RecUpdatedBy = source.RecUpdatedBy,
                RecUpdatedDate = source.RecUpdatedDate,
            };
        }
        public static PurchaseOrder CreateFromClientToServer(this WebsiteModels.PurchaseOrder source, IList<WebsiteModels.PurchaseOrderItem> poItems)
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
            var po = new PurchaseOrder
            {
                PurchaseOrderId = source.PurchaseOrderId,
                FormNumber = source.FormNumber,
                NotesE = notesE,
                NotesA = notesA,
                ManagerId = source.ManagerId,
                Status = source.Status,
                RecCreatedBy = source.RecCreatedBy,
                RecCreatedDate = DateTime.ParseExact(source.RecCreatedDate, "dd/MM/yyyy", new CultureInfo("en")),
                RecUpdatedBy = source.RecUpdatedBy,
                RecUpdatedDate = source.RecUpdatedDate,
            };
            po.PurchaseOrderItems = poItems.Select(x => x.CreateFromClientToServer(po.PurchaseOrderId, po.RecCreatedBy, po.RecCreatedDate, po.RecUpdatedDate)).ToList();
            return po;
        }
        public static PurchaseOrderStatus CreateForStatus(this WebsiteModels.PurchaseOrder source)
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
            return new PurchaseOrderStatus
            {
                PurchaseOrderId = source.PurchaseOrderId,
                Notes = notesE,
                NotesAr = notesA,
                ManagerId = source.ManagerId,
                Status = source.Status,
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
            var po = new PurchaseOrder
            {
                PurchaseOrderId = source.Order.PurchaseOrderId,
                FormNumber = source.Order.FormNumber,
                NotesE = notesE,
                NotesA = notesA,
                ManagerId = source.Order.ManagerId,
                Status = source.Order.Status,
                RecCreatedBy = source.Order.RecCreatedBy,
                RecCreatedDate = DateTime.ParseExact(source.Order.RecCreatedDate, "dd/MM/yyyy", new CultureInfo("en")),
                RecUpdatedBy = source.Order.RecUpdatedBy,
                RecUpdatedDate = source.Order.RecUpdatedDate,
                
            };
            po.PurchaseOrderItems = source.PoItems.Select(x => x.CreateFromClientToServer(po.PurchaseOrderId, po.RecCreatedBy, po.RecCreatedDate, po.RecUpdatedDate)).ToList();
            return po;
        }
        public static PurchaseOrder CreateFromClientToServer(this PurchaseOrderDetailsViewModel source)
        {
            var notesE = source.PurchaseOrder.NotesE;
            if (!string.IsNullOrEmpty(notesE))
            {
                notesE = notesE.Replace("\r", "");
                notesE = notesE.Replace("\t", "");
                notesE = notesE.Replace("\n", "");
            }
            var notesA = source.PurchaseOrder.NotesA;
            if (!string.IsNullOrEmpty(notesA))
            {
                notesA = notesA.Replace("\r", "");
                notesA = notesA.Replace("\t", "");
                notesA = notesA.Replace("\n", "");
            }
            var po = new PurchaseOrder
            {
                PurchaseOrderId = source.PurchaseOrder.PurchaseOrderId,
                FormNumber = source.PurchaseOrder.FormNumber,
                NotesE = notesE,
                NotesA = notesA,
                ManagerId = source.PurchaseOrder.ManagerId,
                Status = source.PurchaseOrder.Status,
                RecCreatedBy = source.PurchaseOrder.RecCreatedBy,
                RecCreatedDate = DateTime.ParseExact(source.PurchaseOrder.RecCreatedDate, "dd/MM/yyyy", new CultureInfo("en")),
                RecUpdatedBy = source.PurchaseOrder.RecUpdatedBy,
                RecUpdatedDate = source.PurchaseOrder.RecUpdatedDate,
            };
            po.PurchaseOrderItems = source.OrderItems.Select(x => x.CreateFromClientToServer(po.PurchaseOrderId, po.RecCreatedBy, po.RecCreatedDate, po.RecUpdatedDate)).ToList();
            return po;
        }

        public static POWidget CreateWidget(this PurchaseOrder source)
        {
            var empName = (source.AspNetUser != null && source.AspNetUser.Employee != null)
                ? source.AspNetUser.Employee.EmployeeFirstNameE + " " + source.AspNetUser.Employee.EmployeeMiddleNameE +
                  " " + source.AspNetUser.Employee.EmployeeLastNameE
                : string.Empty;
            var rfi = new POWidget
            {
                Id = source.PurchaseOrderId,
                FormNumber = source.FormNumber,
                Status = source.Status,
                RequesterName = empName,
                RequesterNameShort = empName.Length > 7 ? empName.Substring(0, 7) : empName,
                RecCreatedDate = source.RecCreatedDate.ToShortDateString()
            };
            return rfi;
        }
    }
}