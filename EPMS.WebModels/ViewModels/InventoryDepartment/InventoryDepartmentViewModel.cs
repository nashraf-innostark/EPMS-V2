using System.Collections.Generic;

namespace EPMS.WebModels.ViewModels.InventoryDepartment
{
    public class InventoryDepartmentViewModel
    {
        public InventoryDepartmentViewModel()
        {
            InventoryDepartment = new WebsiteModels.InventoryDepartment();
            InventoryDepartments = new List<WebsiteModels.InventoryDepartment>();
        }
        public WebsiteModels.InventoryDepartment InventoryDepartment { get; set; }
        public IEnumerable<WebsiteModels.InventoryDepartment> InventoryDepartments { get; set; }
        public IEnumerable<WebsiteModels.InventoryDepartment> Departments { get; set; }
        public IEnumerable<WebsiteModels.InventoryDepartment> Sections { get; set; }
        public string RequestFrom { get; set; }
    }
}