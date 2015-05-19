using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EPMS.Web.ViewModels.InventoryDepartment
{
    public class InventoryDepartmentViewModel
    {
        public InventoryDepartmentViewModel()
        {
            InventoryDepartment = new Models.InventoryDepartment();
        }
        public Models.InventoryDepartment InventoryDepartment { get; set; }
        public IEnumerable<Models.InventoryDepartment> InventoryDepartments { get; set; }
    }
}