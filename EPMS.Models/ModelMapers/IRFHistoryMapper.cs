using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EPMS.Models.DomainModels;

namespace EPMS.Models.ModelMapers
{
    public static class IRFHistoryMapper
    {
        public static ItemReleaseHistory CreateFromIrfToIrfHistory(this ItemRelease source)
        {
            return new ItemReleaseHistory
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
                Customer = source.Customer,
                RFI = source.RFI,
                Manager = source.Manager,
                ItemReleaseDetailHistories = source.ItemReleaseDetails.Select(x=>x.CreateFromIrfDetailToIrfDetailHistory()).ToList(),
            };
        }
        public static ItemRelease CreateFromIrfHistoryToIrf(this ItemReleaseHistory source)
        {
            return new ItemRelease
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
                Customer = source.Customer,
                RFI = source.RFI,
                Manager = source.Manager,
                ItemReleaseDetails = source.ItemReleaseDetailHistories.Select(x => x.CreateFromIrfDetailHistoryToIrfDetail()).ToList(),
            };
        }
    }
}
