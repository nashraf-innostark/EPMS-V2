using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EPMS.Web.Models
{
    public class InventoryDepartment
    {
        public InventoryDepartment()
        {
            ParentNodesSections = new List<ParentNodesSection>();
        }
        public long DepartmentId { get; set; }
        [Required]
        public string DepartmentNameEn { get; set; }
        [Required]
        public string DepartmentNameAr { get; set; }
        public string DepartmentColor { get; set; }
        public string DepartmentDesc { get; set; }
        public long? ParentId { get; set; }
        //public string ParentSectionEn { get; set; }
        //public string ParentSectionAr { get; set; }
        public string ParentDepartmentEn { get; set; }
        public string ParentDepartmentAr { get; set; }
        public string Color { get; set; }
        public long NoOfSections { get; set; }
        public long NoOfSubSections { get; set; }
        public string RecCreatedBy { get; set; }
        public DateTime RecCreatedDt { get; set; }
        public string RecLastUpdatedBy { get; set; }
        public DateTime RecLastUpdatedDt { get; set; }

        //public virtual IEnumerable<InventorySections> InventorySectionses { get; set; }
        public virtual IList<InventoryDepartment> InventoryDepartments { get; set; }
        public virtual InventoryDepartment ParentDepartment { get; set; }
        public InventorySections ParentSection { get; set; }
        public IList<ParentNodesSection> ParentNodesSections { get; set; }
    }
}