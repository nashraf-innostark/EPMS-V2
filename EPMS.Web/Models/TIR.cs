using System;
using System.ComponentModel.DataAnnotations;

namespace EPMS.Web.Models
{
    public class TIR
    {
        public long Id { get; set; }
        public string FormNumber { get; set; }
        [Required(ErrorMessage = "Transfer Reason is required")]
        public string DefectivenessE { get; set; }
        [Required(ErrorMessage = "Transfer Reason arabic is required")]
        public string DefectivenessA { get; set; }
        public string RecCreatedBy { get; set; }
        public DateTime RecCreatedDate { get; set; }
        public string RecUpdatedBy { get; set; }
        public DateTime RecUpdatedDate { get; set; }
        public string NotesA { get; set; }
        public string NotesE { get; set; }
        public int Status { get; set; }
        public string ManagerId { get; set; }
        public string RequesterName { get; set; }
        public string RequesterNameAr { get; set; }
        public string ManagerName { get; set; }
        public string ManagerNameAr { get; set; }
        public string RecCreatedDateString { get; set; }
        public string EmpJobId { get; set; }
    }
}