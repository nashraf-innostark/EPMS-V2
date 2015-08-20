﻿using System;
using EPMS.Models.DashboardModels;
using EPMS.Models.DomainModels;
using Employee = EPMS.Models.DomainModels.Employee;

namespace EPMS.WebModels.ModelMappers
{
    public static class ItemReleaseMapper
    {
        public static WebsiteModels.ItemRelease CreateFromServerToClient(this ItemRelease source)
        {
            var requester = (source.ReqCreatedBy !=null && source.ReqCreatedBy.Employee != null) ? source.ReqCreatedBy.Employee : new Employee();
            var manager = (source.Manager !=null && source.Manager.Employee != null) ? source.Manager.Employee : new Employee();
            return new WebsiteModels.ItemRelease
            {
                ItemReleaseId = source.ItemReleaseId,
                RFIId = source.RFIId,
                FormNumber = source.FormNumber,
                OrderNo = source.OrderNo,
                QuantityReleased = source.QuantityReleased,
                DeliveryInfo = source.DeliveryInfo,
                DeliveryInfoArabic = source.DeliveryInfoArabic,
                RequesterId = source.RequesterId,
                CreatedBy = source.CreatedBy,
                ShipmentDetails = source.ShipmentDetails,
                Status = source.Status,
                RecCreatedBy = source.RecCreatedBy,
                RecCreatedDate = source.RecCreatedDate,
                RecUpdatedBy = source.RecUpdatedBy,
                RecUpdatedDate = source.RecUpdatedDate,
                Notes = source.Notes,
                NotesAr = source.NotesAr,
                ManagerId = source.ManagerId,
                RequesterNameE = requester.EmployeeFirstNameE + " " + requester.EmployeeMiddleNameE + " " + requester.EmployeeLastNameE,
                RequesterNameA = requester.EmployeeFirstNameA + " " + requester.EmployeeMiddleNameA + " " + requester.EmployeeLastNameA,
                ManagerName = manager.EmployeeFirstNameE + " " + manager.EmployeeMiddleNameE + " " + manager.EmployeeLastNameE,
                ManagerNameAr = manager.EmployeeFirstNameA + " " + manager.EmployeeMiddleNameA + " " + manager.EmployeeLastNameA,
            };
        }
        public static ItemRelease CreateFromClientToServer(this WebsiteModels.ItemRelease source)
        {
            var deliveryInfoE = source.DeliveryInfo;
            if (!string.IsNullOrEmpty(deliveryInfoE))
            {
                deliveryInfoE = deliveryInfoE.Replace("\r", "");
                deliveryInfoE = deliveryInfoE.Replace("\t", "");
                deliveryInfoE = deliveryInfoE.Replace("\n", "");
            }
            var deliveryInfoA = source.DeliveryInfoArabic;
            if (!string.IsNullOrEmpty(deliveryInfoA))
            {
                deliveryInfoA = deliveryInfoA.Replace("\r", "");
                deliveryInfoA = deliveryInfoA.Replace("\t", "");
                deliveryInfoA = deliveryInfoA.Replace("\n", "");
            }
            var notesE = source.Notes;
            if (!string.IsNullOrEmpty(notesE))
            {
                notesE = notesE.Replace("\r", "");
                notesE = notesE.Replace("\t", "");
                notesE = notesE.Replace("\n", "");
            }
            var notesA = source.NotesAr;
            if (!string.IsNullOrEmpty(notesA))
            {
                notesA = notesA.Replace("\r", "");
                notesA = notesA.Replace("\t", "");
                notesA = notesA.Replace("\n", "");
            }
            return new ItemRelease
            {
                ItemReleaseId = source.ItemReleaseId,
                RFIId = source.RFIId,
                FormNumber = source.FormNumber,
                OrderNo = source.OrderNo,
                QuantityReleased = source.QuantityReleased,
                DeliveryInfo = deliveryInfoE,
                DeliveryInfoArabic = deliveryInfoA,
                RequesterId = source.RequesterId,
                CreatedBy = source.CreatedBy,
                ShipmentDetails = source.ShipmentDetails,
                Status = source.Status,
                RecCreatedBy = source.RecCreatedBy,
                RecCreatedDate = source.RecCreatedDate,
                RecUpdatedBy = source.RecUpdatedBy,
                RecUpdatedDate = source.RecUpdatedDate,
                ManagerId = source.ManagerId,
                Notes = notesE,
                NotesAr = notesA,
            };
        }
        public static ItemReleaseStatus CreateForStatus(this WebsiteModels.ItemRelease source)
        {
            var notesE = source.Notes;
            if (!string.IsNullOrEmpty(notesE))
            {
                notesE = notesE.Replace("\r", "");
                notesE = notesE.Replace("\t", "");
                notesE = notesE.Replace("\n", "");
            }
            var notesA = source.NotesAr;
            if (!string.IsNullOrEmpty(notesA))
            {
                notesA = notesA.Replace("\r", "");
                notesA = notesA.Replace("\t", "");
                notesA = notesA.Replace("\n", "");
            }
            return new ItemReleaseStatus
            {
                ItemReleaseId = source.ItemReleaseId,
                Status = source.Status ?? 3,
                RecUpdatedBy = source.RecUpdatedBy,
                RecUpdatedDate = source.RecUpdatedDate,
                ManagerId = source.ManagerId,
                Notes = notesE,
                NotesAr = notesA,
            };
        }

        public static IRFWidget CreateWidget(this ItemRelease source)
        {
            var empName = (source.Requester != null && source.Requester.Employee != null)
                ? source.Requester.Employee.EmployeeFirstNameE + " " + source.Requester.Employee.EmployeeMiddleNameE +
                  " " + source.Requester.Employee.EmployeeLastNameE
                : string.Empty;
            var rfi = new IRFWidget
            {
                Id = source.ItemReleaseId,
                Status = Convert.ToInt32(source.Status),
                RequesterName = empName,
                RequesterNameShort = empName.Length > 7 ? empName.Substring(0, 7) : empName,
                RecCreatedDate = source.RecCreatedDate.ToShortDateString()
            };
            return rfi;
        }
    }
}