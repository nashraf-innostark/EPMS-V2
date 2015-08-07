using System.Collections.Generic;
using EPMS.Models.DomainModels;

namespace EPMS.Models.ResponseModels
{
    public class ItemVariationForWarehouse
    {
        public IEnumerable<ItemVariationDropDownListItem> ItemVariationDropDownListItems { get; set; }
        public IEnumerable<InventoryDepartment> InventoryDepartments { get; set; }
    }
}
