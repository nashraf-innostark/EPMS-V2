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
            };
        }
        public static ItemRelease CreateFromClientToServer(this Models.ItemRelease source)
        {
            return new ItemRelease
            {
                ItemReleaseId = source.ItemReleaseId,
                RFIId = source.RFIId,
                FormNumber = source.FormNumber,
                OrderNo = source.OrderNo,
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
            };
        }
    }
}