﻿using System.Linq;
using EPMS.Models.DomainModels;
using EPMS.Web.ViewModels.TIR;

namespace EPMS.Web.ModelMappers
{
    public static class TransferItemMapper
    {
        public static Models.TIR CreateFromServerToClient(this TIR source)
        {
            var direction = Resources.Shared.Common.TextDirection;
            return new Models.TIR
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
                Date = source.RecCreatedDate.ToShortDateString(),
                RequesterName = direction == "ltr" ?
                source.AspNetUser.Employee.EmployeeFirstNameE + " " + source.AspNetUser.Employee.EmployeeMiddleNameE + " " + source.AspNetUser.Employee.EmployeeLastNameE
                : source.AspNetUser.Employee.EmployeeFirstNameA + " " + source.AspNetUser.Employee.EmployeeMiddleNameA + " " + source.AspNetUser.Employee.EmployeeLastNameA,
            };
        }
        public static TIR CreateFromClientToServer(this Models.TIR source)
        {
            var notesE = source.NotesE;
            if (!string.IsNullOrEmpty(notesE))
            {
                notesE = notesE.Replace("\r", "");
                notesE = notesE.Replace("\t", "");
                notesE = notesE.Replace("\n", "");
            }
            var notesA = source.NotesA;
            if (!string.IsNullOrEmpty(notesA))
            {
                notesA = notesA.Replace("\r", "");
                notesA = notesA.Replace("\t", "");
                notesA = notesA.Replace("\n", "");
            }
            return new TIR
            {
                Id = source.Id,
                FormNumber = source.FormNumber,
                DefectivenessE = source.DefectivenessE,
                DefectivenessA = source.DefectivenessA,
                NotesE = notesE,
                NotesA = notesA,
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
            var notesE = source.Tir.NotesE;
            if (!string.IsNullOrEmpty(notesE))
            {
                notesE = notesE.Replace("\r", "");
                notesE = notesE.Replace("\t", "");
                notesE = notesE.Replace("\n", "");
            }
            var notesA = source.Tir.NotesA;
            if (!string.IsNullOrEmpty(notesA))
            {
                notesA = notesA.Replace("\r", "");
                notesA = notesA.Replace("\t", "");
                notesA = notesA.Replace("\n", "");
            }
            return new TIR
            {
                Id = source.Tir.Id,
                FormNumber = source.Tir.FormNumber,
                DefectivenessE = source.Tir.DefectivenessE,
                DefectivenessA = source.Tir.DefectivenessA,
                NotesE = notesE,
                NotesA = notesA,
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