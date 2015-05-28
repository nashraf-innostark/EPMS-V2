using System.Collections.Generic;

namespace EPMS.Web.ViewModels.InventorySection
{
    public class InventorySectionViewModel
    {
        public Models.InventoryDepartment InventoryDepartment { get; set; }
        public IEnumerable<Models.InventoryDepartment> InventoryDepartments { get; set; }
        public string RequestFrom { get; set; }
    }
}