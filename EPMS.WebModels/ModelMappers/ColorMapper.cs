using System.Globalization;

namespace EPMS.WebModels.ModelMappers
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
                RecCreatedDt = source.RecCreatedDt.ToString("dd/MM/yyyy", new CultureInfo("en")),
                RecLastUpdatedBy = source.RecLastUpdatedBy,
                RecLastUpdatedDt = source.RecLastUpdatedDt,
            };
        }
    }
}