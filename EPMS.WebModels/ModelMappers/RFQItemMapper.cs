﻿using EPMS.Models.DomainModels;

namespace EPMS.WebModels.ModelMappers
{
    public static class RFQItemMapper
    {
        public static RFQItem CreateFromClientToServer(this WebsiteModels.RFQItem source)
        {
            return new RFQItem
            {
                ItemId = source.ItemId,
                RFQId = source.RFQId,
                ItemDetails = source.ItemDetails,
                ItemQuantity = source.ItemQuantity,
                UnitPrice = source.UnitPrice,
                TotalPrice = source.TotalPrice,
                RecCreatedBy = source.RecCreatedBy,
                RecCreatedDate = source.RecCreatedDate,
                RecLastUpdatedBy = source.RecLastUpdatedBy,
                RecLastUpdatedDate = source.RecLastUpdatedDate,
            };
        }

        public static WebsiteModels.RFQItem CreateFromServerToClient(this RFQItem source)
        {
            return new WebsiteModels.RFQItem
            {
                ItemId = source.ItemId,
                RFQId = source.RFQId,
                ItemDetails = source.ItemDetails,
                ItemQuantity = source.ItemQuantity,
                UnitPrice = source.UnitPrice,
                TotalPrice = source.TotalPrice,
                RecCreatedBy = source.RecCreatedBy,
                RecCreatedDate = source.RecCreatedDate,
                RecLastUpdatedBy = source.RecLastUpdatedBy,
                RecLastUpdatedDate = source.RecLastUpdatedDate,
            };
        }
    }
}