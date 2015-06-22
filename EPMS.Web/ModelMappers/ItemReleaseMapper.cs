using System;
using System.Linq;
using EPMS.Models.DomainModels;

namespace EPMS.Web.ModelMappers
{
    public static class ItemReleaseMapper
    {
        public static Models.ItemRelease CreateFromServerToClient(this ItemRelease source)
        {
            return new Models.ItemRelease
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
                RequesterNameE = source.Customer.CustomerNameE,
                RequesterNameA = source.Customer.CustomerNameA,
                RecCreatedBy = source.RecCreatedBy,
                RecCreatedDate = source.RecCreatedDate,
                RecUpdatedBy = source.RecUpdatedBy,
                RecUpdatedDate = source.RecUpdatedDate,
                Notes = source.Notes,
                NotesAr = source.NotesAr,
                ManagerId = source.ManagerId,
                ItemReleaseQuantities = source.ItemReleaseQuantities.Select(x=>x.CreateFromServerToClient()).ToList()
            };
        }
        public static ItemRelease CreateFromClientToServer(this Models.ItemRelease source)
        {
            var deliveryInfoE = source.DeliveryInfo;
            deliveryInfoE = deliveryInfoE.Replace("\r", "");
            deliveryInfoE = deliveryInfoE.Replace("\t", "");
            deliveryInfoE = deliveryInfoE.Replace("\n", "");
            var deliveryInfoA = source.DeliveryInfoArabic;
            deliveryInfoA = deliveryInfoA.Replace("\r", "");
            deliveryInfoA = deliveryInfoA.Replace("\t", "");
            deliveryInfoA = deliveryInfoA.Replace("\n", "");
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
    }
}