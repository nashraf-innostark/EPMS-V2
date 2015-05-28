using System.Web.WebPages.Scope;
using WebModels = EPMS.Web.Models;
using DomainModels = EPMS.Models.DomainModels;

namespace EPMS.Web.ModelMappers
{
    public static class StatusMapper
    {
        public static WebModels.Status CreateFromServerToClient(this DomainModels.Status source)
        {
            return new WebModels.Status
            {
                StatusId = source.StatusId,
                StatusNameEn = source.StatusNameEn,
                StatusNameAr = source.StatusNameAr,
                RecCreatedBy = source.RecCreatedBy,
                RecCreatedDt = source.RecCreatedDt,
                RecLastUpdatedBy = source.RecLastUpdatedBy,
                RecLastUpdatedDt = source.RecLastUpdatedDt,
            };
        }
    }
}