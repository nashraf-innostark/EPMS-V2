using System.Linq;
using EPMS.Models.DomainModels;

namespace EPMS.WebModels.ModelMappers
{
    public static class TIRHistoryMapper
    {
        public static TIRHistory CreateFromTirToTirHistory(this TIR source)
        {
            return new TIRHistory
            {
                Id = source.Id,
                ParentId = source.Id,
                FormNumber = source.FormNumber,
                DefectivenessE = source.DefectivenessE,
                DefectivenessA = source.DefectivenessA,
                NotesE = source.NotesE,
                NotesA = source.NotesA,
                ManagerId = source.ManagerId,
                Status = source.Status,
                RecCreatedBy = source.RecCreatedBy,
                RecCreatedDate = source.RecCreatedDate,
                RecUpdatedBy = source.RecUpdatedBy,
                RecUpdatedDate = source.RecUpdatedDate,
                AspNetUser = source.AspNetUser,
                AspNetUser1 = source.Manager,
                TIRItemHistories = source.TIRItems.Select(x=>x.CreateFromTirItemToTirItemHistory()).ToList(),
            };
        }
        public static TIR CreateFromTirHistoryToTir(this TIRHistory source)
        {
            return new TIR
            {
                Id = source.Id,
                FormNumber = source.FormNumber,
                DefectivenessE = source.DefectivenessE,
                DefectivenessA = source.DefectivenessA,
                NotesE = source.NotesE,
                NotesA = source.NotesA,
                ManagerId = source.ManagerId,
                Status = source.Status,
                RecCreatedBy = source.RecCreatedBy,
                RecCreatedDate = source.RecCreatedDate,
                RecUpdatedBy = source.RecUpdatedBy,
                RecUpdatedDate = source.RecUpdatedDate,
                AspNetUser = source.AspNetUser,
                Manager = source.AspNetUser1,
                TIRItems = source.TIRItemHistories.Select(x => x.CreateFromTirItemHistoryToTirItem()).ToList(),
            };
        }
    }
}
