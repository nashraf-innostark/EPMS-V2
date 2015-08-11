using System;
using System.Globalization;
using System.Linq;
using EPMS.Models.DashboardModels;
using EPMS.Models.DomainModels;
using EPMS.WebModels.ViewModels.TIR;

namespace EPMS.WebModels.ModelMappers
{
    public static class TransferItemMapper
    {
        public static WebsiteModels.TIR CreateFromServerToClient(this TIR source)
        {
            var direction = Resources.Shared.Common.TextDirection;
            var retVal = new WebsiteModels.TIR
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
                RecCreatedDateString = Convert.ToDateTime(source.RecCreatedDate).ToString("dd/MM/yyyy", new CultureInfo("en")) + "-" + Convert.ToDateTime(source.RecCreatedDate).ToString("dd/MM/yyyy", new CultureInfo("ar")),
            };
            if (source.AspNetUser != null)
            {
                retVal.RequesterName = direction == "ltr" ?
                source.AspNetUser.Employee.EmployeeFirstNameE + " " + source.AspNetUser.Employee.EmployeeMiddleNameE + " " + source.AspNetUser.Employee.EmployeeLastNameE
                : source.AspNetUser.Employee.EmployeeFirstNameA + " " + source.AspNetUser.Employee.EmployeeMiddleNameA + " " + source.AspNetUser.Employee.EmployeeLastNameA;
            }
            if (source.Manager != null)
            {
                retVal.ManagerName = direction == "ltr" ? 
                source.Manager.Employee.EmployeeFirstNameE + " " + source.Manager.Employee.EmployeeMiddleNameE + " " + source.Manager.Employee.EmployeeLastNameE 
                : source.Manager.Employee.EmployeeFirstNameA + " " + source.Manager.Employee.EmployeeMiddleNameA + " " + source.Manager.Employee.EmployeeLastNameA;
            }
            return retVal;
        }
        public static TIR CreateFromClientToServer(this WebsiteModels.TIR source)
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
                //TIRItems = source.TirItems.Select<TIRItem, TIRItem>(x => x.CreateFromClientToServer(source.Tir.Id, source.Tir.RecCreatedBy, source.Tir.RecCreatedDate, source.Tir.RecUpdatedDate)).ToList()
            };
        }

        public static TIRWidget CreateWidget(this TIR source)
        {
            var empName = (source.AspNetUser != null && source.AspNetUser.Employee != null)
                ? source.AspNetUser.Employee.EmployeeFirstNameE + " " + source.AspNetUser.Employee.EmployeeMiddleNameE +
                  " " + source.AspNetUser.Employee.EmployeeLastNameE
                : string.Empty;
            var rfi = new TIRWidget
            {
                Id = source.Id,
                Status = source.Status,
                RequesterName = empName,
                RequesterNameShort = empName.Length > 7 ? empName.Substring(0, 7) : empName,
                RecCreatedDate = source.RecCreatedDate.ToShortDateString()
            };
            return rfi;
        }
        public static TransferItemStatus CreateForStatus(this WebsiteModels.TIR source)
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
            return new TransferItemStatus
            {
                Id = source.Id,
                ManagerId = source.ManagerId,
                Status = source.Status,
                NotesEn = notesE,
                NotesAr = notesA,
                RecUpdatedBy = source.RecUpdatedBy,
                RecUpdatedDate = source.RecUpdatedDate,
            };
        }
    }
}