﻿using System.ComponentModel.DataAnnotations;

namespace EPMS.WebModels.WebsiteModels
{
    public class DIF
    {
        public long Id { get; set; }
        [Required]
        [Display(Name = "Defectiveness")]
        public string DefectivenessE { get; set; }
        [Required]
        [Display(Name = "Defectiveness in arabic")]
        public string DefectivenessA { get; set; }
        public string RecCreatedBy { get; set; }
        public System.DateTime RecCreatedDate { get; set; }
        public string RecCreatedDateString { get; set; }
        public string RecUpdatedBy { get; set; }
        public System.DateTime RecUpdatedDate { get; set; }
        public string NotesA { get; set; }
        public string NotesE { get; set; }
        public int Status { get; set; }
        public string ManagerId { get; set; }
        public string FormNumber { get; set; }
        public string RequesterName { get; set; }
        public string RequesterNameAr { get; set; }
        public string ManagerName { get; set; }
        public string ManagerNameAr { get; set; }
        public string EmpJobId { get; set; }
        public long WarehouseId { get; set; }
    }
}