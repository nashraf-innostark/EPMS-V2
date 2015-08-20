﻿namespace EPMS.WebModels.ModelMappers
{
    public static class ColorMapper
    {
        public static WebsiteModels.Color CreateFromServerToClient(this Models.DomainModels.Color source)
        {
            return new WebsiteModels.Color
            {
                ColorId = source.ColorId,
                ColorNameEn = source.ColorNameEn,
                ColorNameAr = source.ColorNameAr,
                ColorCode = source.ColorCode,
                RecCreatedBy = source.RecCreatedBy,
                RecCreatedDt = source.RecCreatedDt,
                RecLastUpdatedBy = source.RecLastUpdatedBy,
                RecLastUpdatedDt = source.RecLastUpdatedDt,
            };
        }
    }
}