namespace EPMS.WebModels.WebsiteModels
{
    public class InventorySections
    {
        public long DepartmentId { get; set; }
        public string DepartmentNameEn { get; set; }
        public string DepartmentNameAr { get; set; }
        public string DepartmentColor { get; set; }
        public string DepartmentDesc { get; set; }
        public long? ParentId { get; set; }
        public string ParentDepartmentEn { get; set; }
        public string ParentDepartmentAr { get; set; }
        public long NoOfSections { get; set; }
        public long NoOfSubSections { get; set; }

        public virtual InventorySections ParentSections { get; set; }
    }
}