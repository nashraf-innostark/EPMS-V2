using System.Collections.Generic;

namespace EPMS.WebModels.ViewModels.InventorySection
{
    public class InventorySectionViewModel
    {
        public WebsiteModels.InventoryDepartment InventoryDepartment { get; set; }
        public IEnumerable<WebsiteModels.InventoryDepartment> InventoryDepartments { get; set; }
        public string RequestFrom { get; set; }
    }
}