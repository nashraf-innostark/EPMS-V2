using System;
using System.Globalization;
using System.Linq;
using EPMS.Models.DomainModels;
using EPMS.WebModels.ViewModels.PhysicalCount;

namespace EPMS.WebModels.ModelMappers
{
    public static class PhysicalCountMapper
    {
        public static WebsiteModels.PhysicalCountListModel CreateListFromServerToClient(this PhysicalCount source)
        {
            return new WebsiteModels.PhysicalCountListModel
            {
                PCId = source.PCId
            };
        }
        public static WebsiteModels.PhysicalCountModel CreateFromServerToClient(this PhysicalCount source)
        {
            return new WebsiteModels.PhysicalCountModel
            {
                PCId = source.PCId,
                RecCreatedBy = source.RecCreatedBy,
                RecLastUpdatedBy = source.RecLastUpdatedBy,
                RecCreatedDate = source.RecCreatedDate.ToString("dd/MM/yyyy", new CultureInfo("en")),
                RecLastUpdatedDate = source.RecLastUpdatedDate,
                NotesEn = source.NotesEn,
                NotesAr = source.NotesAr,

                WarehouseId = source.PhysicalCountItems.Any() ? source.PhysicalCountItems.FirstOrDefault().WarehouseId : 0
            };
        }
        public static PhysicalCount CreateFromClientToServer(this PhysicalCountViewModel source)
        {
            return new PhysicalCount
            {
                PCId = source.PhysicalCount.PCId,
                RecCreatedBy = source.PhysicalCount.RecCreatedBy,
                RecLastUpdatedBy = source.PhysicalCount.RecLastUpdatedBy,
                RecCreatedDate = DateTime.ParseExact(source.PhysicalCount.RecCreatedDate, "dd/MM/yyyy", new CultureInfo("en")),
                RecLastUpdatedDate = source.PhysicalCount.RecLastUpdatedDate,
                NotesEn = source.PhysicalCount.NotesEn,
                NotesAr = source.PhysicalCount.NotesAr,

                PhysicalCountItems = source.PhysicalCountItems.Select(x=>x.CreateFromClientToServer()).ToList()
            };
        }
    }
}