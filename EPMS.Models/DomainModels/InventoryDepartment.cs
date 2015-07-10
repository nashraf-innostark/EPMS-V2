using System;
using System.Collections.Generic;

namespace EPMS.Models.DomainModels
{
    public class InventoryDepartment
    {
        public long DepartmentId { get; set; }
        public string DepartmentNameEn { get; set; }
        public string DepartmentNameAr { get; set; }
        public string DepartmentColor { get; set; }
        public string DepartmentDesc { get; set; }
        public long? ParentId { get; set; }
        public string RecCreatedBy { get; set; }
        public DateTime RecCreatedDt { get; set; }
        public string RecLastUpdatedBy { get; set; }
        public DateTime RecLastUpdatedDt { get; set; }

        public virtual ICollection<InventoryDepartment> InventoryDepartments { get; set; }
        public virtual InventoryDepartment ParentDepartment { get; set; }
        public virtual ICollection<InventoryItem> InventoryItems { get; set; }
    }
}
