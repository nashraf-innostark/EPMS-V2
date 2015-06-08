using System.Linq;
using EPMS.Models.DomainModels;
using EPMS.Web.ViewModels.TIR;

namespace EPMS.Web.ModelMappers
{
    public static class TransferItemMapper
    {
        public static Models.TIR CreateFromServerToClient(this TIR source)
        {
            return new Models.TIR
            {
                Id = source.Id,
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
            };
        }
        public static TIR CreateFromClientToServer(this Models.TIR source)
        {
            return new TIR
            {
                Id = source.Id,
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
            };
        }
        public static TIR CreateFromClientToServer(this TransferItemCreateViewModel source)
        {
            return new TIR
            {
                Id = source.Tir.Id,
                DefectivenessE = source.Tir.DefectivenessE,
                DefectivenessA = source.Tir.DefectivenessA,
                NotesE = source.Tir.NotesE,
                NotesA = source.Tir.NotesA,
                ManagerId = source.Tir.ManagerId,
                Status = source.Tir.Status,
                RecCreatedBy = source.Tir.RecCreatedBy,
                RecCreatedDate = source.Tir.RecCreatedDate,
                RecUpdatedBy = source.Tir.RecUpdatedBy,
                RecUpdatedDate = source.Tir.RecUpdatedDate,
                TIRItems = source.TirItems.Select(x => x.CreateFromClientToServer(source.Tir.Id, source.Tir.RecCreatedBy, source.Tir.RecCreatedDate, source.Tir.RecUpdatedDate)).ToList()
            };
        }
    }
}