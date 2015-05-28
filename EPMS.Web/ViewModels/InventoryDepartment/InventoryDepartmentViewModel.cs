using System.Collections.Generic;

namespace EPMS.Web.ViewModels.InventoryDepartment
{
    public class InventoryDepartmentViewModel
    {
        public InventoryDepartmentViewModel()
        {
            InventoryDepartment = new Models.InventoryDepartment();
            InventoryDepartments = new List<Models.InventoryDepartment>();
        }
        public Models.InventoryDepartment InventoryDepartment { get; set; }
        public IEnumerable<Models.InventoryDepartment> InventoryDepartments { get; set; }
        public IEnumerable<Models.InventoryDepartment> Departments { get; set; }
        public IEnumerable<Models.InventoryDepartment> Sections { get; set; }
        public string RequestFrom { get; set; }
    }
}